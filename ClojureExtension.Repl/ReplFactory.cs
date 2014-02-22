// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Controls;
using ClojureExtension.Parsing;
using ClojureExtension.Repl.Operations;
using ClojureExtension.Utilities;
using EnvDTE;
using EnvDTE80;
using Microsoft.ClojureExtension.Editor;
using Microsoft.ClojureExtension.Project.Hierarchy;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Process = System.Diagnostics.Process;
using Thread = System.Threading.Thread;

namespace ClojureExtension.Repl
{
	public class ReplFactory
	{
    public TabControl ReplManager { get; set; }
    public IVsWindowFrame ReplToolWindow { get; set; }
    private readonly IServiceProvider _serviceProvider;

    public ReplFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public void CreateRepl(Process replProcess)
		{
			var tabItem = new ReplTab();
			var replEntity = new Entity<ReplState> { CurrentState = new ReplState() };

			WireUpTheTextBoxInputToTheReplProcess(tabItem.InteractiveText, replProcess, replEntity);
			WireUpTheOutputOfTheReplProcessToTheTextBox(tabItem.InteractiveText, replProcess, replEntity);
			WireUpTheReplEditorCommandsToTheEditor(tabItem.InteractiveText, replProcess, replEntity, tabItem);

			tabItem.CloseButton.Click +=
			(o, e) =>
			{
				replProcess.Kill();
				ReplManager.Items.Remove(tabItem);
			};

			ReplManager.Items.Add(tabItem);
			ReplManager.SelectedItem = tabItem;
		  ReplToolWindow.Show();
		}

		private void WireUpTheReplEditorCommandsToTheEditor(TextBox replTextBox, Process replProcess, Entity<ReplState> replEntity, TabItem tabItem)
		{
			var dte = (DTE2)_serviceProvider.GetService(typeof(DTE));

			var menuCommandListWirer = new MenuCommandListWirer(
				(OleMenuCommandService)_serviceProvider.GetService(typeof(IMenuCommandService)),
				CreateMenuCommands(replProcess, replTextBox, replEntity),
								() => dte.ActiveDocument != null && (dte.ActiveDocument.FullName.ToLower().EndsWith(".clj") || dte.ActiveDocument.FullName.ToLower().EndsWith(".cljs")) && ReplManager.SelectedItem == tabItem);

			dte.Events.WindowEvents.WindowActivated += (o, e) => menuCommandListWirer.TryToShowMenuCommands();
			ReplManager.SelectionChanged += (sender, eventData) => menuCommandListWirer.TryToShowMenuCommands();
		}

		private static void WireUpTheTextBoxInputToTheReplProcess(TextBox replTextBox, Process replProcess, Entity<ReplState> replEntity)
		{
			var inputKeyHandler = new InputKeyHandler(new KeyboardExaminer(), replEntity, replTextBox, new ReplWriter(replProcess, new TextBoxWriter(replTextBox, replEntity)));
			var history = new History(new KeyboardExaminer(), replEntity, replTextBox);

			replTextBox.PreviewKeyDown += history.PreviewKeyDown;
			replTextBox.PreviewTextInput += inputKeyHandler.PreviewTextInput;
			replTextBox.PreviewKeyDown += inputKeyHandler.PreviewKeyDown;
		}

		private static void WireUpTheOutputOfTheReplProcessToTheTextBox(TextBox replTextBox, Process replProcess, Entity<ReplState> replEntity)
		{
			var standardOutputStream = new StreamBuffer();
			var standardErrorStream = new StreamBuffer();
			var processOutputReader = new ProcessOutputReader(new TextBoxWriter(replTextBox, replEntity), standardOutputStream, standardErrorStream);
			var outputReaderThread = new Thread(processOutputReader.StartMarshallingTextFromReplToTextBox);
			var outputBufferStreamThread = new Thread(() => standardOutputStream.ReadStream(replProcess.StandardOutput.BaseStream));
			var errorBufferStreamThread = new Thread(() => standardOutputStream.ReadStream(replProcess.StandardError.BaseStream));

			replTextBox.Loaded +=
				(o, e) =>
				{
					if (outputReaderThread.IsAlive) return;
					replProcess.Start();
					replProcess.StandardInput.AutoFlush = true;
					outputBufferStreamThread.Start();
					errorBufferStreamThread.Start();
					outputReaderThread.Start();
				};

			replProcess.Exited +=
				(o, e) =>
				{
					outputBufferStreamThread.Abort();
					errorBufferStreamThread.Abort();
					outputReaderThread.Abort();
				};
		}

		private List<MenuCommand> CreateMenuCommands(Process replProcess, TextBox interactiveText, Entity<ReplState> replEntity)
		{
			var dte = (DTE2)_serviceProvider.GetService(typeof(DTE));

			var loadSelectedFilesIntoRepl =
				new LoadFilesIntoRepl(
					new ReplWriter(replProcess, new TextBoxWriter(interactiveText, replEntity)),
					new SelectedFilesProvider(dte.ToolWindows.SolutionExplorer),
					ReplToolWindow);

			var loadSelectedProjectIntoRepl =
				new LoadFilesIntoRepl(
					new ReplWriter(replProcess, new TextBoxWriter(interactiveText, replEntity)),
					new ProjectFilesProvider(
						new SelectedProjectProvider(dte.Solution, dte.ToolWindows.SolutionExplorer)),
					ReplToolWindow);

			var loadActiveFileIntoRepl =
				new LoadFilesIntoRepl(
					new ReplWriter(replProcess, new TextBoxWriter(interactiveText, replEntity)),
					new ActiveFileProvider(dte),
					ReplToolWindow);

			var componentModel = (IComponentModel)_serviceProvider.GetService(typeof(SComponentModel));

			var namespaceParser = new NamespaceParser(NamespaceParser.NamespaceSymbols);

			var activeTextBufferStateProvider =
				new ActiveTextBufferStateProvider(
					componentModel.GetService<IVsEditorAdaptersFactoryService>(),
					(IVsTextManager)_serviceProvider.GetService(typeof(SVsTextManager)));

			var changeReplNamespace =
				new ChangeReplNamespace(new ReplWriter(replProcess, new TextBoxWriter(interactiveText, replEntity)));

			var menuCommands = new List<MenuCommand>();
			menuCommands.Add(new MenuCommand((sender, args) => loadSelectedProjectIntoRepl.Execute(), CommandIDs.LoadProjectIntoActiveRepl));
			menuCommands.Add(new MenuCommand((sender, args) => loadSelectedFilesIntoRepl.Execute(), CommandIDs.LoadFileIntoActiveRepl));
			menuCommands.Add(new MenuCommand((sender, args) => loadActiveFileIntoRepl.Execute(), CommandIDs.LoadActiveDocumentIntoRepl));
			menuCommands.Add(new MenuCommand((sender, args) => changeReplNamespace.Execute(namespaceParser.Execute(activeTextBufferStateProvider.Get())), CommandIDs.SwitchReplNamespaceToActiveDocument));
			menuCommands.Add(new MenuCommand((sender, args) => new ReplWriter(replProcess, new TextBoxWriter(interactiveText, replEntity)).WriteBehindTheSceneExpressionToRepl((string)(dte.ActiveDocument.Selection as dynamic).Text), CommandIDs.LoadSelectedTextIntoRepl));
			return menuCommands;
		}
	}
}
