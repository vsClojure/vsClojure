using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using EnvDTE80;
using Microsoft.ClojureExtension.Project.Hierarchy;
using Microsoft.ClojureExtension.Repl.Operations;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Repl
{
	public class ReplFactory
	{
		private readonly DTE2 _dte;
		private readonly IVsWindowFrame _replToolWindow;
		private readonly IMenuCommandService _menuCommandService;

		public ReplFactory(DTE2 dte, IVsWindowFrame replToolWindow, IMenuCommandService menuCommandService)
		{
			_dte = dte;
			_replToolWindow = replToolWindow;
			_menuCommandService = menuCommandService;
		}

		public void CreateRepl(string replPath, string projectPath, TabControl replManager)
		{
			TextBox interactiveText = ReplUserInterfaceFactory.CreateInteractiveText();
			Button closeButton = ReplUserInterfaceFactory.CreateCloseButton();
			Label name = ReplUserInterfaceFactory.CreateTabLabel();
			Grid grid = ReplUserInterfaceFactory.CreateTextBoxGrid(interactiveText);
			WrapPanel headerPanel = ReplUserInterfaceFactory.CreateHeaderPanel(name, closeButton);
			TabItem tabItem = ReplUserInterfaceFactory.CreateTabItem(headerPanel, grid);
			Process replProcess = CreateReplProcess(replPath, projectPath);

			Entity<ReplState> replEntity = new Entity<ReplState>();
			replEntity.CurrentState = new ReplState(0, new LinkedList<string>(), new LinkedList<Key>());

			ProcessOutputTunnel processOutputTunnel = new ProcessOutputTunnel(replProcess, interactiveText, replEntity);
			Thread outputReaderThread = new Thread(() => processOutputTunnel.WriteFromReplToTextBox(replProcess.StandardOutput));
			Thread errorReaderThread = new Thread(() => processOutputTunnel.WriteFromReplToTextBox(replProcess.StandardError));
			MetaKeyWatcher metaKeyWatcher = new MetaKeyWatcher(replEntity);
			InputKeyHandler inputKeyHandler = new InputKeyHandler(metaKeyWatcher, replEntity, interactiveText, new ReplWriter(replProcess, interactiveText));
			History history = new History(metaKeyWatcher, replEntity, interactiveText);

			interactiveText.PreviewKeyDown += metaKeyWatcher.PreviewKeyDown;
			interactiveText.PreviewKeyUp += metaKeyWatcher.PreviewKeyUp;
			interactiveText.PreviewKeyDown += history.PreviewKeyDown;
			interactiveText.PreviewTextInput += inputKeyHandler.PreviewTextInput;
			interactiveText.PreviewKeyDown += inputKeyHandler.PreviewKeyDown;

			MenuCommandListWirer menuCommandListWirer = new MenuCommandListWirer(
				_menuCommandService,
				CreateMenuCommands(replProcess, interactiveText),
				() => _dte.ActiveDocument != null && _dte.ActiveDocument.FullName.ToLower().EndsWith(".clj") && replManager.SelectedItem == tabItem);

			_dte.Events.WindowEvents.WindowActivated += (o, e) => menuCommandListWirer.TryToShowMenuCommands();

			interactiveText.Loaded +=
				(o, e) =>
				{
					if (outputReaderThread.IsAlive) return;
					replProcess.Start();
					replProcess.StandardInput.AutoFlush = true;
					outputReaderThread.Start();
					errorReaderThread.Start();
				};

			replProcess.Exited +=
				(o, e) =>
				{
					outputReaderThread.Abort();
					errorReaderThread.Abort();
				};

			closeButton.Click +=
				(o, e) =>
				{
					replProcess.Kill();
					replManager.Items.Remove(tabItem);
				};

			replManager.SelectionChanged += (sender, eventData) => menuCommandListWirer.TryToShowMenuCommands();
			replManager.Items.Add(tabItem);
			replManager.SelectedItem = tabItem;
		}

		private List<MenuCommand> CreateMenuCommands(Process replProcess, TextBox interactiveText)
		{
			LoadFilesIntoRepl loadSelectedFilesIntoRepl =
				new LoadFilesIntoRepl(
					new ReplWriter(replProcess, interactiveText),
					new SelectedFilesProvider(_dte.ToolWindows.SolutionExplorer),
					_replToolWindow);
			
			LoadFilesIntoRepl loadSelectedProjectIntoRepl =
				new LoadFilesIntoRepl(
					new ReplWriter(replProcess, interactiveText),
					new ProjectFilesProvider(
						new SelectedProjectProvider(_dte.Solution, _dte.ToolWindows.SolutionExplorer)),
					_replToolWindow);

			LoadFilesIntoRepl loadActiveFileIntoRepl =
				new LoadFilesIntoRepl(
					new ReplWriter(replProcess, interactiveText),
					new ActiveFileProvider(_dte),
					_replToolWindow);

			SwitchNamespaceToFile switchNamespaceToFile =
				new SwitchNamespaceToFile(
					new ActiveFileProvider(_dte),
					new ReplWriter(replProcess, interactiveText));
			
			List<MenuCommand> menuCommands = new List<MenuCommand>();
			menuCommands.Add(new MenuCommand((sender, args) => loadSelectedProjectIntoRepl.Execute(), new CommandID(Guids.GuidClojureExtensionCmdSet, 11)));
			menuCommands.Add(new MenuCommand((sender, args) => loadSelectedFilesIntoRepl.Execute(), new CommandID(Guids.GuidClojureExtensionCmdSet, 12)));
			menuCommands.Add(new MenuCommand((sender, args) => loadActiveFileIntoRepl.Execute(), new CommandID(Guids.GuidClojureExtensionCmdSet, 13)));
			menuCommands.Add(new MenuCommand((sender, args) => switchNamespaceToFile.Execute(), new CommandID(Guids.GuidClojureExtensionCmdSet, 14)));
			return menuCommands;
		}

		private static Process CreateReplProcess(string replPath, string projectPath)
		{
			Process process = new Process();
			process.StartInfo = new ProcessStartInfo();
			process.StartInfo.FileName = replPath + "\\Clojure.Main.exe";
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardInput = true;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.EnvironmentVariables["clojure.load.path"] = projectPath;
			return process;
		}
	}
}