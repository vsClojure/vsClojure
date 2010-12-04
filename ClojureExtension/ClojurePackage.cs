/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.ClojureExtension.Configuration;
using Microsoft.ClojureExtension.Editor;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Project;
using Microsoft.ClojureExtension.Project.Hierarchy;
using Microsoft.ClojureExtension.Repl;
using Microsoft.ClojureExtension.Repl.Operations;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.ClojureExtension
{
	[Guid(PackageGuid)]
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\10.0")]
	[ProvideObject(typeof (GeneralPropertyPage))]
	[ProvideProjectFactory(typeof (ClojureProjectFactory), "Clojure", "Clojure Project Files (*.cljproj);*.cljproj", "cljproj", "cljproj", @"Templates\Projects\Clojure", LanguageVsTemplate = "Clojure", NewProjectRequireNewFolderVsTemplate = false)]
	[ProvideProjectItem(typeof (ClojureProjectFactory), "Clojure Items", @"Templates\ProjectItems\Clojure", 500)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideToolWindow(typeof (ReplToolWindow))]
	public sealed class ClojurePackage : ProjectPackage
	{
		public const string PackageGuid = "40953a10-3425-499c-8162-a90059792d13";

		protected override void Initialize()
		{
			base.Initialize();
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

			CreateSettingsStore();
			IntializeMenuItems();
			RegisterProjectFactory(new ClojureProjectFactory(this));

			var componentModel = (IComponentModel) GetService(typeof (SComponentModel));
			TokenizedBufferBuilder tokenizedBufferBuilder = new TokenizedBufferBuilder(new Tokenizer());

			EditorCommandFactory editorCommandFactory = new EditorCommandFactory(
				(OleMenuCommandService) GetService(typeof (IMenuCommandService)),
				componentModel.GetService<IEditorOptionsFactoryService>());

			ITextDocumentFactoryService documentFactoryService = componentModel.GetService<ITextDocumentFactoryService>();

			documentFactoryService.TextDocumentDisposed +=
				(o, e) => tokenizedBufferBuilder.RemoveTokenizedBuffer(e.TextDocument.TextBuffer);

			documentFactoryService.TextDocumentCreated +=
				(o, e) => { if (e.TextDocument.FilePath.EndsWith(".clj")) tokenizedBufferBuilder.CreateTokenizedBuffer(e.TextDocument.TextBuffer); };

			ITextEditorFactoryService editorFactoryService = componentModel.GetService<ITextEditorFactoryService>();

			editorFactoryService.TextViewCreated += (o, e) => e.TextView.GotAggregateFocus +=
				(sender, args) =>
				{
					editorCommandFactory.UnwireEditorCommands();
					if (e.TextView.TextSnapshot.ContentType.TypeName.ToLower() != "clojure") return;
					editorCommandFactory.WireCommandsTo(e.TextView);

					IEditorOptions editorOptions = componentModel.GetService<IEditorOptionsFactoryService>().GetOptions(e.TextView);
					editorOptions.SetOptionValue(new ConvertTabsToSpaces().Key, true);
					editorOptions.SetOptionValue(new IndentSize().Key, 4);
				};
		}

		private void CreateSettingsStore()
		{
			IVsWritableSettingsStore settingsStore;
			var settingsManager = (IVsSettingsManager) GetService(typeof (SVsSettingsManager));
			settingsManager.GetWritableSettingsStore((uint) __VsSettingsScope.SettingsScope_UserSettings, out settingsStore);
			SettingsStoreProvider.Store = new SettingsStore("Clojure", settingsStore);
		}

		private void IntializeMenuItems()
		{
			OleMenuCommandService menuCommandService = (OleMenuCommandService) GetService(typeof (IMenuCommandService));
			ReplToolWindow replToolWindow = (ReplToolWindow) FindToolWindow(typeof (ReplToolWindow), 0, true);
			IVsWindowFrame replToolWindowFrame = (IVsWindowFrame) replToolWindow.Frame;
			DTE2 dte = (DTE2) GetService(typeof (DTE));

			menuCommandService.AddCommand(
				new MenuCommand(
					(sender, args) =>
						new StartReplUsingProjectVersion(
							replToolWindow.TabControl,
							new ReplFactory(dte, replToolWindowFrame, menuCommandService),
							replToolWindowFrame,
							new GetFrameworkFromSelectedProject(new SelectedProjectProvider(dte.Solution, dte.ToolWindows.SolutionExplorer)),
							new SelectedProjectProvider(dte.Solution, dte.ToolWindows.SolutionExplorer)).Execute(),
					new CommandID(Guids.GuidClojureExtensionCmdSet, 10)));
		}

		public override string ProductUserContext
		{
			get { return "ClojureProj"; }
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.FullName == args.Name);
		}
	}
}