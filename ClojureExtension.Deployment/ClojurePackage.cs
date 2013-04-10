/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ClojureExtension.Deployment.Configuration;
using ClojureExtension.Editor.Commenting;
using ClojureExtension.Editor.TextBuffer;
using ClojureExtension.Parsing;
using ClojureExtension.Project.Launching;
using ClojureExtension.Repl;
using ClojureExtension.Repl.Operations;
using ClojureExtension.Utilities;
using ClojureExtension.Utilities.IO.Compression;
using EnvDTE;
using EnvDTE80;
using Microsoft.ClojureExtension.Editor;
using Microsoft.ClojureExtension.Editor.AutoFormat;
using Microsoft.ClojureExtension.Editor.Options;
using Microsoft.ClojureExtension.Project;
using Microsoft.ClojureExtension.Project.Hierarchy;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.Win32;
using Constants = ClojureExtension.Utilities.Constants;
using Process = System.Diagnostics.Process;
using ClojureExtension.Editor.Intellisense;

namespace ClojureExtension.Deployment
{
	[Guid(PackageGuid)]
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\11.0")]
	[ProvideObject(typeof(GeneralPropertyPageAdapter))]
	[ProvideProjectFactory(typeof(ClojureProjectFactory), "Clojure", "Clojure Project Files (*.cljproj);*.cljproj", "cljproj", "cljproj", @"Templates\Projects", LanguageVsTemplate = "Clojure", NewProjectRequireNewFolderVsTemplate = false)]
	[ProvideProjectItem(typeof(ClojureProjectFactory), "Clojure Items", @"Templates\ProjectItems\Clojure", 500)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideToolWindow(typeof(ReplToolWindow))]
	[ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string)]
	public sealed class ClojurePackage : ProjectPackage
	{
		public const string PackageGuid = "7712178c-977f-45ec-adf6-e38108cc7739";
		private const bool OPTIMIZE_COMPILED_JAVASCRIPT = false;

		private ClearableMenuCommandService _thirdPartyEditorCommands;

		protected override void Initialize()
		{
			base.Initialize();
			var dte = (DTE2)GetService(typeof(DTE));

			dte.Events.DTEEvents.OnStartupComplete +=
				() =>
				{
					AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainAssemblyResolve;
					RegisterProjectFactory(new ClojureProjectFactory(this));
					RegisterCommandMenuService();
					HideAllClojureEditorMenuCommands();
					ShowClojureProjectMenuCommands();
					EnableTokenizationOfNewClojureBuffers();
					SetupNewClojureBuffersWithSpacingOptions();
					EnableMenuCommandsOnNewClojureBuffers();
					UnzipRuntimes();
					EnableSettingOfRuntimePathForNewClojureProjects();
          InitializeSlowLoadingProcesses();
				};
		}

    private void InitializeSlowLoadingProcesses()
    {
      HippieCompletionSource.Initialize();
    }

		private void UnzipRuntimes()
		{
			try
			{
				var runtimeBasePath = Path.Combine(GetDirectoryOfDeployedContents(), "Runtimes");
				Directory.GetFiles(runtimeBasePath, "*.zip").ToList().ForEach(CompressionExtensions.ExtractZipToFreshSubDirectoryAndDelete);
			}
			catch (Exception e)
			{
				var errorMessage = new StringBuilder();
				errorMessage.AppendLine("Failed to extract Clojure runtime(s).  You may need to reinstall vsClojure.");
				errorMessage.AppendLine(e.Message);
				MessageBox.Show(errorMessage.ToString());
			}
		}

		private string GetDirectoryOfDeployedContents()
		{
			string codebaseRegistryLocation = ApplicationRegistryRoot + "\\Packages\\{" + PackageGuid + "}";
			return Path.GetDirectoryName(Registry.GetValue(codebaseRegistryLocation, "CodeBase", "").ToString());
		}

		private void RegisterCommandMenuService()
		{
			IVsRegisterPriorityCommandTarget commandRegistry = GetService(typeof(SVsRegisterPriorityCommandTarget)) as IVsRegisterPriorityCommandTarget;
			_thirdPartyEditorCommands = new ClearableMenuCommandService(this);
			uint cookie = 0;
			commandRegistry.RegisterPriorityCommandTarget(0, _thirdPartyEditorCommands, out cookie);
		}

		private void EnableSettingOfRuntimePathForNewClojureProjects()
		{
			string deployDirectory = GetDirectoryOfDeployedContents();
			string runtimePath = deployDirectory + "\\Runtimes";
			string clrRuntimePath1_5_0 = string.Format("{0}\\{1}-{2}", runtimePath, Constants.CLOJURECLR, Constants.VERSION);

			bool runtimePathIncorrect = EnvironmentVariables.VsClojureRuntimesDir != runtimePath;
			if (runtimePathIncorrect)
			{
        EnvironmentVariables.VsClojureRuntimesDir = runtimePath;
			}

			string extensionsDirectory = Directory.GetParent(deployDirectory).FullName;

      string clojureLoadPath = EnvironmentVariables.ClojureLoadPath ?? "";
			List<string> loadPaths = clojureLoadPath.Split(new[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries).Where(x => !x.Contains(extensionsDirectory)).ToList();
			loadPaths.Insert(0, clrRuntimePath1_5_0);
			string newClojureLoadPath = loadPaths.Aggregate((x, y) => x + Path.PathSeparator + y);

      bool clojureLoadPathIncorrect = EnvironmentVariables.ClojureLoadPath != newClojureLoadPath;
			if (clojureLoadPathIncorrect)
			{
        EnvironmentVariables.ClojureLoadPath = newClojureLoadPath;
			}

      if (runtimePathIncorrect || clojureLoadPathIncorrect)
      {
        if (MessageBox.Show("Would you like to view the vsClojure ReadMe.txt", "vsClojure Readme.txt", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          string pathToReadme = deployDirectory + "\\ReadMe.txt";
          Process.Start("notepad.exe", pathToReadme);
        }

        MessageBox.Show("Setup of vsClojure complete.  Please restart Visual Studio.", "vsClojure Setup");
      }
		}

		private void HideAllClojureEditorMenuCommands()
		{
            List<CommandID> allCommandIds = new List<CommandID>() { CommandIDs.LoadProjectIntoActiveRepl, CommandIDs.LoadFileIntoActiveRepl, CommandIDs.LoadActiveDocumentIntoRepl, CommandIDs.SwitchReplNamespaceToActiveDocument, CommandIDs.LoadSelectedTextIntoRepl };
			DTE2 dte = (DTE2)GetService(typeof(DTE));
			OleMenuCommandService menuCommandService = (OleMenuCommandService)GetService(typeof(IMenuCommandService));
			List<MenuCommand> menuCommands = new List<MenuCommand>();
			foreach (CommandID commandId in allCommandIds) menuCommands.Add(new MenuCommand((o, s) => { }, commandId));
			MenuCommandListHider hider = new MenuCommandListHider(menuCommandService, menuCommands);
			dte.Events.WindowEvents.WindowActivated += (o, e) => hider.HideMenuCommands();
		}

		private void EnableMenuCommandsOnNewClojureBuffers()
		{
			var componentModel = (IComponentModel)GetService(typeof(SComponentModel));
			ITextEditorFactoryService editorFactoryService = componentModel.GetService<ITextEditorFactoryService>();

			editorFactoryService.TextViewCreated += (o, e) => e.TextView.GotAggregateFocus +=
				(sender, args) =>
				{
					_thirdPartyEditorCommands.Clear();
					if (e.TextView.TextSnapshot.ContentType.TypeName.ToLower() != "clojure") return;

					var editorOptionsBuilder = new EditorOptionsBuilder(componentModel.GetService<IEditorOptionsFactoryService>().GetOptions(e.TextView));
					var tokenizedBuffer = TokenizedBufferBuilder.TokenizedBuffers[e.TextView.TextBuffer];
					var formatter = new AutoFormatter(new TextBufferAdapter(e.TextView), tokenizedBuffer);
					var blockComment = new BlockComment(new TextBufferAdapter(e.TextView));
					var blockUncomment = new BlockUncomment(new TextBufferAdapter(e.TextView));
					_thirdPartyEditorCommands.AddCommand(new MenuCommand((commandSender, commandArgs) => formatter.Format(editorOptionsBuilder.Get()), CommandIDs.FormatDocument));
					_thirdPartyEditorCommands.AddCommand(new MenuCommand((commandSender, commandArgs) => blockComment.Execute(), CommandIDs.BlockComment));
					_thirdPartyEditorCommands.AddCommand(new MenuCommand((commandSender, commandArgs) => blockUncomment.Execute(), CommandIDs.BlockUncomment));
					_thirdPartyEditorCommands.AddCommand(new MenuCommand((commandSender, commandArgs) => { }, CommandIDs.GotoDefinition));
				};
		}

		private void SetupNewClojureBuffersWithSpacingOptions()
		{
			var componentModel = (IComponentModel)GetService(typeof(SComponentModel));
			ITextEditorFactoryService editorFactoryService = componentModel.GetService<ITextEditorFactoryService>();

			editorFactoryService.TextViewCreated +=
				(o, e) =>
				{
					if (e.TextView.TextSnapshot.ContentType.TypeName.ToLower() != "clojure") return;
					IEditorOptions editorOptions = componentModel.GetService<IEditorOptionsFactoryService>().GetOptions(e.TextView);
					editorOptions.SetOptionValue(new ConvertTabsToSpaces().Key, true);
					editorOptions.SetOptionValue(new IndentSize().Key, 2);
				};
		}

		private Dictionary<string, Process> filesBeingCompiled = new Dictionary<string, Process>();
		private object filesBeingCompiledLock = new object();
		private void CompileClojureScript(string filePath, string inputFileContents, Action<string> outputResult)
		{
			new System.Threading.Thread(() =>
			{
				outputResult("/* compiling ... */");

        string runtimeDir = string.Format("{0}\\{1}-{2}", EnvironmentVariables.VsClojureRuntimesDir, Constants.CLOJURESCRIPT, Constants.VERSION);
				List<string> paths = Directory.GetFiles(string.Format("{0}\\lib\\", runtimeDir), "*.jar", SearchOption.AllDirectories).ToList();
				paths.Add(string.Format("{0}\\src\\clj", runtimeDir));
				paths.Add(string.Format("{0}\\src\\cljs", runtimeDir));
				paths.Add(string.Format("{0}\\lib", runtimeDir));

				string classPath = paths.Aggregate((x, y) => x + ";" + y);
				string compilerPath = String.Format("{0}\\bin\\cljsc.clj", runtimeDir);

				string inputFileName = Path.GetTempFileName();
				using (StreamWriter outfile = new StreamWriter(inputFileName))
				{
					outfile.Write(inputFileContents);
				}

				string workingDirectory = GetTempDirectory();

				Process newProcess = new Process();
				newProcess.StartInfo.UseShellExecute = false;
				newProcess.StartInfo.RedirectStandardOutput = true;
				newProcess.StartInfo.RedirectStandardError = true;
				newProcess.StartInfo.CreateNoWindow = true;
				newProcess.StartInfo.FileName = "java";
				const string optimizations = OPTIMIZE_COMPILED_JAVASCRIPT ? "{:optimizations :advanced}" : "";
				string arguments = string.Format("-server -cp \"{0}\" clojure.main \"{1}\" \"{2}\" {3}", classPath, compilerPath, inputFileName, optimizations);
				newProcess.StartInfo.Arguments = arguments;
				newProcess.StartInfo.WorkingDirectory = workingDirectory;

				string standardOutput = "";
				string standardError = "";
				lock (filesBeingCompiledLock)
				{
					if (filesBeingCompiled.ContainsKey(filePath))
					{
						Process oldProcess = filesBeingCompiled[filePath];
						try
						{
							oldProcess.Kill();
						}
						catch { }
					}

					filesBeingCompiled[filePath] = newProcess;

					IntPtr oldWow64Redirection = new IntPtr();
					Win32Api.Wow64DisableWow64FsRedirection(ref oldWow64Redirection);

					try
					{
						newProcess.Start();
					}
					catch (Exception e)
					{
						standardError = e.Message + Environment.NewLine + "Ensure you have the latest version of Java and the JDK installed from Oracle.com" + Environment.NewLine + "Ensure the directory containing java is on the path environment variable (usually C:\\Program Files (x86)\\Java\\jre7\\bin)";
					}

					Win32Api.Wow64RevertWow64FsRedirection(oldWow64Redirection);
				}

				if (string.IsNullOrWhiteSpace(standardError))
				{
					standardOutput = newProcess.StandardOutput.ReadToEnd();
					standardError = newProcess.StandardError.ReadToEnd();

					newProcess.WaitForExit();
				}

				if (!string.IsNullOrWhiteSpace(standardError))
				{
					standardError = string.Format("/*{0}{1}{0}*/{0}", Environment.NewLine, standardError);
				}

				if (!OPTIMIZE_COMPILED_JAVASCRIPT && !string.IsNullOrWhiteSpace(standardOutput))
				{
					string outDirectory = workingDirectory + "\\out";
					if (Directory.Exists(outDirectory))
					{
						string outputFile = Directory.GetFiles(outDirectory, "*.js", SearchOption.TopDirectoryOnly).FirstOrDefault();
						string outputFileContent = !string.IsNullOrWhiteSpace(outputFile) ? File.ReadAllText(outputFile) : "";
						standardOutput = string.Format("/*{0}{1}{0}*/{0}{2}", Environment.NewLine, standardOutput, outputFileContent);
					}
					else
					{
						standardOutput = string.Format("/*{0}{1}{0}*/{0}", Environment.NewLine, standardOutput);
					}
				}

				if (!string.IsNullOrWhiteSpace(standardError) || !string.IsNullOrWhiteSpace(standardOutput))
				{
					outputResult(string.Format("{0}{1}", standardError, standardOutput));
				}
			}).Start();
		}

		private string GetTempDirectory()
		{
			string tempFile = Path.GetTempFileName();
			File.Delete(tempFile);
			string result = Path.ChangeExtension(tempFile, "");
			result = result.Substring(0, result.Length - 1); // remove final .
			Directory.CreateDirectory(result);
			return result;
		}

		private void EnableTokenizationOfNewClojureBuffers()
		{
			var componentModel = (IComponentModel)GetService(typeof(SComponentModel));
			TokenizedBufferBuilder tokenizedBufferBuilder = new TokenizedBufferBuilder(new Tokenizer());
			ITextDocumentFactoryService documentFactoryService = componentModel.GetService<ITextDocumentFactoryService>();

			documentFactoryService.TextDocumentDisposed +=
				(o, e) => tokenizedBufferBuilder.RemoveTokenizedBuffer(e.TextDocument.TextBuffer);

			documentFactoryService.TextDocumentCreated +=
					(o, e) =>
					{
						if (e.TextDocument.FilePath.EndsWith(".clj"))
						{
							tokenizedBufferBuilder.CreateTokenizedBuffer(e.TextDocument.TextBuffer);
						}
						else if (e.TextDocument.FilePath.EndsWith(".cljs"))
						{
							tokenizedBufferBuilder.CreateTokenizedBuffer(e.TextDocument.TextBuffer);

							e.TextDocument.FileActionOccurred += (sender, fileActionEvent) =>
							{
								if (fileActionEvent.FileActionType == FileActionTypes.ContentSavedToDisk)
								{
									RequestCompile(e.TextDocument);
								}
							};
						}
					};
		}

		private void RequestCompile(ITextDocument textDocument)
		{
			string filePath = textDocument.FilePath;
			DTE2 dte = (DTE2)GetService(typeof(DTE));
			ProjectItem projectItem = dte.Solution.FindProjectItem(filePath);
			if (projectItem == null || projectItem.ContainingProject == null)
			{
				return;
			}

			CompileClojureScript(filePath, textDocument.TextBuffer.CurrentSnapshot.GetText(), compilationResult =>
				{
					string outputFilePath = filePath + ".js";
					if (dte.SourceControl != null && dte.SourceControl.IsItemUnderSCC(outputFilePath) &&
							!dte.SourceControl.IsItemCheckedOut(outputFilePath))
					{
						dte.SourceControl.CheckOutItem(outputFilePath);
					}

					File.WriteAllText(outputFilePath, compilationResult);

					if (projectItem.ProjectItems != null &&
							!projectItem.ProjectItems.Cast<ProjectItem>().Any(x => x.FileNames[0] == outputFilePath))
					{
						projectItem.ProjectItems.AddFromFile(outputFilePath);
					}
					else
					{
						projectItem.ContainingProject.ProjectItems.AddFromFile(outputFilePath);
						ProjectItem newProjectItem = dte.Solution.FindProjectItem(outputFilePath);
					}
				});
		}

		private void ShowClojureProjectMenuCommands()
		{
			OleMenuCommandService menuCommandService = (OleMenuCommandService)GetService(typeof(IMenuCommandService));
			ReplToolWindow replToolWindow = (ReplToolWindow)FindToolWindow(typeof(ReplToolWindow), 0, true);
			IVsWindowFrame replToolWindowFrame = (IVsWindowFrame)replToolWindow.Frame;
			DTE2 dte = (DTE2)GetService(typeof(DTE));
			IProvider<EnvDTE.Project> projectProvider = new SelectedProjectProvider(dte.Solution, dte.ToolWindows.SolutionExplorer);

			menuCommandService.AddCommand(
				new MenuCommand(
					(sender, args) =>
						new StartReplUsingProjectVersion(
							new ReplFactory(replToolWindow.TabControl, replToolWindowFrame, this),
							replToolWindowFrame,
							() => new LaunchParametersBuilder((ProjectNode)projectProvider.Get().Object).Get().FrameworkPath,
							new SelectedProjectProvider(dte.Solution, dte.ToolWindows.SolutionExplorer)).Execute(),
                    CommandIDs.StartReplUsingProjectVersion));
		}

		public override string ProductUserContext
		{
			get { return "ClojureProj"; }
		}

		private static Assembly CurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
		{
			return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.FullName == args.Name);
		}
	}
}