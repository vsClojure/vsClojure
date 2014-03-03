// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClojureExtension.Debugger;
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
using VSLangProj;
using clojure.lang;
using vsClojure;
using Constants = ClojureExtension.Utilities.Constants;
using Process = System.Diagnostics.Process;
using ClojureExtension.Editor.Intellisense;
using Thread = System.Threading.Thread;

namespace ClojureExtension.Deployment
{
	[Guid(PackageGuid)]
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\12.0")]
	[ProvideService(typeof(ClojureLanguage), ServiceName = ClojureLanguage.CLOJURE_LANGUAGE_NAME)]
	[ProvideLanguageService(typeof(ClojureLanguage), ClojureLanguage.CLOJURE_LANGUAGE_NAME, 100, CodeSense = true, DefaultToInsertSpaces = true, EnableCommenting = true, MatchBraces = true, MatchBracesAtCaret = true, ShowCompletion = true, ShowMatchingBrace = true, QuickInfo = true, AutoOutlining = true, DebuggerLanguageExpressionEvaluator = ClojureLanguage.CLOJURE_LANGUAGE_GUID)]
	[ProvideLanguageExtension(typeof(ClojureLanguage), ClojureLanguage.CLJ_FILE_EXTENSION)]
	[ProvideLanguageExtension(typeof(ClojureLanguage), ClojureLanguage.CLJS_FILE_EXTENSION)]
	[RegisterSnippetsAttribute(ClojureLanguage.CLOJURE_LANGUAGE_GUID, false, 131, ClojureLanguage.CLOJURE_LANGUAGE_NAME, @"CodeSnippets\SnippetsIndex.xml", @"CodeSnippets\Snippets\", @"CodeSnippets\Snippets\")]
	[ProvideObject(typeof(GeneralPropertyPageAdapter))]
	[ProvideProjectFactory(typeof(ClojureProjectFactory), "Clojure", "Clojure Project Files (*.cljproj);*.cljproj", "cljproj", "cljproj", @"Templates\Projects", LanguageVsTemplate = "Clojure", NewProjectRequireNewFolderVsTemplate = false)]
	[ProvideProjectItem(typeof(ClojureProjectFactory), "Clojure Items", @"Templates\ProjectItems\Clojure", 500)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideToolWindow(typeof(ReplToolWindow))]
  [RegisterExpressionEvaluator(typeof(ExpressionEvaluator), ClojureLanguage.CLOJURE_LANGUAGE_NAME, ExpressionEvaluator.CLOJURE_DEBUG_EXPRESSION_EVALUATOR_GUID, ExpressionEvaluator.MICROSOFT_VENDOR_GUID)]
  [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string)]
  [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string)]
  public sealed class ClojurePackage : ProjectPackage
	{
		public const string PackageGuid = "7712178c-977f-45ec-adf6-e38108cc7739";
		private const bool OPTIMIZE_COMPILED_JAVASCRIPT = false;
		private ClearableMenuCommandService _thirdPartyEditorCommands;
		private Metadata _metadata;
		private ErrorListHelper _errorListHelper;

		protected override void Initialize()
		{
      base.Initialize();

      AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainAssemblyResolve;

		  try
		  {
		    UnzipRuntimes();
		    RegisterProjectFactory(new ClojureProjectFactory(this));
		    RegisterCommandMenuService();
		    HideAllClojureEditorMenuCommands();
		    EnableTokenizationOfNewClojureBuffers();
		    SetupNewClojureBuffersWithSpacingOptions();
		    EnableMenuCommandsOnNewClojureBuffers();
		    ShowClojureProjectMenuCommands();
		    _errorListHelper = new ErrorListHelper();
      
		    Thread delayedStartup = new Thread(() =>
		    {
		      try
		      {
						EnableSettingOfRuntimePathForNewClojureProjectsOnFirstInstall();

		        HippieCompletionSource.Initialize(this);
		        _metadata = new Metadata(); // SlowLoadingProcess for the 1st time.
		      }
		      catch (Exception e)
		      {
		        MessageBox.Show(string.Format("Unhandled Exception loading vsClojure : {0}{1}Stack Trace: {2}", e.Message, System.Environment.NewLine, e.StackTrace));
		      }
		    });
		    delayedStartup.IsBackground = true;
		    delayedStartup.Start();
		  }
		  catch (Exception e)
		  {
		    MessageBox.Show(string.Format("Unhandled Exception loading vsClojure : {0}{1}Stack Trace: {2}", e.Message, System.Environment.NewLine, e.StackTrace));
		  }
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

		private void EnableSettingOfRuntimePathForNewClojureProjectsOnFirstInstall()
		{
      var deployDirectory = GetDirectoryOfDeployedContents();
      var runtimePath = string.Format(@"{0}\Runtimes", deployDirectory);
      var firstInstall = string.Compare(EnvironmentVariables.VsClojureRuntimesDir, runtimePath, true, CultureInfo.CurrentCulture) != 0;

			if (firstInstall)
      {
        EnvironmentVariables.VsClojureRuntimesDir = runtimePath;

        var pathToReadme = string.Format(@"{0}\ReadMe.txt", deployDirectory);
				
				// Opening a file within Visual Studio
				// Reference: http://stackoverflow.com/a/10724025/170217
				var dte = (DTE2) GetService(typeof(DTE));
	      dte.MainWindow.Activate(); // This may not be needed, need to test without!
	      dte.ItemOperations.OpenFile(pathToReadme, EnvDTE.Constants.vsViewKindTextView);
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

			editorFactoryService.TextViewCreated += (o, e) =>
			{
				e.TextView.GotAggregateFocus +=
				(sender, args) =>
				{
					_thirdPartyEditorCommands.Clear();
					if (e.TextView.TextSnapshot.ContentType.TypeName.ToLower() != "clojure")
					{
						return;
					}

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

				_errorListHelper.ClearErrors(filePath);

				if (!string.IsNullOrWhiteSpace(standardError))
				{
					standardError = string.Format("/*{0}{1}{0}*/{0}", Environment.NewLine, standardError);

					List<string> compilerErrors = new Regex("^Exception in thread \"main\" [^:]*:(.*)compiling:\\(.:[^:]*:([0-9]*):([0-9]*)\\)", RegexOptions.Multiline).Matches(standardError, 0).Cast<Match>().Select(x => string.Format("({0}, {1}, {0}, {1}): {2}", x.Groups[2].Value, x.Groups[3].Value, x.Groups[1].Value)).ToList();
					compilerErrors.AddRange(new Regex("^Caused by:[^:]*:(.*)", RegexOptions.Multiline).Matches(standardError, 0).Cast<Match>().Select(x => x.Groups[1].Value).ToList());

					foreach (string compilerError in compilerErrors)
					{
						_errorListHelper.Write(TaskCategory.BuildCompile, TaskErrorCategory.Error, compilerError, filePath);
					}
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

									e.TextDocument.FileActionOccurred += (sender, fileActionEvent) =>
									{
										if (fileActionEvent.FileActionType == FileActionTypes.ContentSavedToDisk)
										{
											RequestClojureCompile(e.TextDocument);
										}
									};
								}
								else if (e.TextDocument.FilePath.EndsWith(".cljs"))
								{
									tokenizedBufferBuilder.CreateTokenizedBuffer(e.TextDocument.TextBuffer);

									e.TextDocument.FileActionOccurred += (sender, fileActionEvent) =>
									{
										if (fileActionEvent.FileActionType == FileActionTypes.ContentSavedToDisk)
										{
											RequestClojureScriptCompile(e.TextDocument);
										}
									};
								}
							};
		}

		private void RequestClojureCompile(ITextDocument textDocument)
		{
			if (_metadata == null)
			{
				return;
			}

			string filePath = textDocument.FilePath;
			string code = textDocument.TextBuffer.CurrentSnapshot.GetText();

			new System.Threading.Thread(() =>
			{
				Var.pushThreadBindings(RT.map(Compiler.CompilePathVar, Path.GetTempPath()));
				IEnumerable<string> compilerErrors = _metadata.GetCompilerErrors(code);

				_errorListHelper.ClearErrors(filePath);

				foreach (string compilerError in compilerErrors)
				{
					_errorListHelper.Write(TaskCategory.BuildCompile, TaskErrorCategory.Error, compilerError, filePath);
				}
			}).Start();
		}

		private void RequestClojureScriptCompile(ITextDocument textDocument)
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

    ReplToolWindow _replToolWindow = null;
	  private ReplToolWindow ReplToolWindow
	  {
	    get
	    {
	      if (_replToolWindow == null)
	      {
	        _replToolWindow = (ReplToolWindow) FindToolWindow(typeof (ReplToolWindow), 0, true);

	      }

        return _replToolWindow;
	    }
	  }

    IVsWindowFrame _replToolWindowFrame = null;
	  private IVsWindowFrame ReplToolWindowFrame
	  {
	    get
	    {
	      if (_replToolWindowFrame == null)
	      {
	        _replToolWindowFrame = (IVsWindowFrame) ReplToolWindow.Frame;
	      }

	      return _replToolWindowFrame;
	    }
	  }

    private void ShowClojureProjectMenuCommands()
		{
			OleMenuCommandService menuCommandService = (OleMenuCommandService)GetService(typeof(IMenuCommandService));
			ReplFactory replFactory = new ReplFactory(this);
      StartReplUsingProjectVersion replStartFunction = new StartReplUsingProjectVersion(replFactory,
        () =>
        {
          DTE2 dte = (DTE2)GetService(typeof(DTE));
          IProvider<EnvDTE.Project> projectProvider = new SelectedProjectProvider(dte.Solution, dte.ToolWindows.SolutionExplorer);
         
          string frameworkPath = Path.Combine(EnvironmentVariables.VsClojureRuntimesDir, "ClojureCLR-1.5.0");

          try
          {
            frameworkPath = new LaunchParametersBuilder((ProjectNode)projectProvider.Get().Object).Get().FrameworkPath;
          }
          catch { }

          SelectedProjectProvider selectedProjectProvider = new SelectedProjectProvider(dte.Solution, dte.ToolWindows.SolutionExplorer);
          return ReplUtilities.CreateReplProcess(frameworkPath, Path.GetDirectoryName(selectedProjectProvider.Get().FullName));
        });

			menuCommandService.AddCommand(new MenuCommand((sender, args) =>
			{
			  replFactory.ReplManager = ReplToolWindow.TabControl;
			  replFactory.ReplToolWindow = ReplToolWindowFrame;
			  replStartFunction.Execute();
			}, CommandIDs.StartReplUsingProjectVersion));
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