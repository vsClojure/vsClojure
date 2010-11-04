using System.ComponentModel.Design;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EnvDTE80;
using Microsoft.ClojureExtension.Project.Hierarchy;
using Microsoft.ClojureExtension.Repl.Operations;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Repl
{
	public class ReplTabFactory
	{
		private readonly DTE2 _dte;
		private readonly IVsWindowFrame _replToolWindow;
		private readonly IMenuCommandService _menuCommandService;

		public ReplTabFactory(DTE2 dte, IVsWindowFrame replToolWindow, IMenuCommandService menuCommandService)
		{
			_dte = dte;
			_replToolWindow = replToolWindow;
			_menuCommandService = menuCommandService;
		}

		public void CreateRepl(string replPath, string projectPath, TabControl replManager)
		{
			TextBox interactiveText = CreateInteractiveText();

			Grid grid = new Grid();
			grid.Children.Add(interactiveText);

			Button closeButton = CreateCloseButton();
			Label name = CreateTabLabel();

			WrapPanel headerPanel = new WrapPanel();
			headerPanel.Children.Add(name);
			headerPanel.Children.Add(closeButton);

			TabItem tabItem = new TabItem();
			tabItem.Header = headerPanel;
			tabItem.Content = grid;

			Process replProcess = CreateReplProcess(replPath, projectPath);

			ReplTextPipe textPipe = new ReplTextPipe(interactiveText, replProcess, new ReplWriter(replProcess, interactiveText));
			Thread outputReaderThread = new Thread(() => textPipe.WriteFromReplToTextBox(replProcess.StandardOutput));
			Thread errorReaderThread = new Thread(() => textPipe.WriteFromReplToTextBox(replProcess.StandardError));

			interactiveText.PreviewTextInput += textPipe.PreviewTextInput;
			interactiveText.PreviewKeyDown += textPipe.PreviewKeyDown;
			interactiveText.PreviewKeyUp += textPipe.PreviewKeyUp;

			interactiveText.Loaded +=
				(o, e) =>
				{
					if (outputReaderThread.IsAlive) return;
					replProcess.Start();
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

			replManager.SelectionChanged +=
				(sender, eventData) => { if (replManager.SelectedItem == tabItem) BuildAndWireMenuCommands(replProcess, interactiveText); };

			replManager.Items.Add(tabItem);
			replManager.SelectedItem = tabItem;
		}

		private void BuildAndWireMenuCommands(Process replProcess, TextBox interactiveText)
		{
			UnwireExistingMenuCommands();

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

			_menuCommandService.AddCommand(new MenuCommand((sender, args) => loadSelectedProjectIntoRepl.Execute(), new CommandID(Guids.GuidClojureExtensionCmdSet, 11)));
			_menuCommandService.AddCommand(new MenuCommand((sender, args) => loadSelectedFilesIntoRepl.Execute(), new CommandID(Guids.GuidClojureExtensionCmdSet, 12)));
			_menuCommandService.AddCommand(new MenuCommand((sender, args) => loadActiveFileIntoRepl.Execute(), new CommandID(Guids.GuidClojureExtensionCmdSet, 13)));
			_menuCommandService.AddCommand(new MenuCommand((sender, args) => switchNamespaceToFile.Execute(), new CommandID(Guids.GuidClojureExtensionCmdSet, 14)));
		}

		private void UnwireExistingMenuCommands()
		{
			for (int i = 11; i < 15; i++)
			{
				MenuCommand menuCommand = _menuCommandService.FindCommand(new CommandID(Guids.GuidClojureExtensionCmdSet, i));
				if (menuCommand == null) continue;
				_menuCommandService.RemoveCommand(menuCommand);
			}
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
			process.Start();
			process.StandardInput.AutoFlush = true;
			return process;
		}

		private static Label CreateTabLabel()
		{
			Label name = new Label();
			name.Content = "Repl";
			name.Height = 19;
			name.HorizontalAlignment = HorizontalAlignment.Left;
			name.VerticalAlignment = VerticalAlignment.Top;
			name.VerticalContentAlignment = VerticalAlignment.Center;
			name.FontFamily = new FontFamily("Courier");
			name.FontSize = 12;
			name.Padding = new Thickness(0);
			name.Margin = new Thickness(0, 1, 0, 0);
			return name;
		}

		private static Button CreateCloseButton()
		{
			Button closeButton = new Button();
			closeButton.Content = "X";
			closeButton.Width = 20;
			closeButton.Height = 19;
			closeButton.FontFamily = new FontFamily("Courier");
			closeButton.FontSize = 12;
			closeButton.FontWeight = (FontWeight) new FontWeightConverter().ConvertFromString("Bold");
			closeButton.HorizontalAlignment = HorizontalAlignment.Right;
			closeButton.VerticalAlignment = VerticalAlignment.Top;
			closeButton.Style = (Style) closeButton.FindResource(ToolBar.ButtonStyleKey);
			closeButton.Margin = new Thickness(3, 0, 0, 0);
			return closeButton;
		}

		private static TextBox CreateInteractiveText()
		{
			TextBox interactiveText = new TextBox();
			interactiveText.HorizontalAlignment = HorizontalAlignment.Stretch;
			interactiveText.VerticalAlignment = VerticalAlignment.Stretch;
			interactiveText.FontSize = 12;
			interactiveText.FontFamily = new FontFamily("Courier New");
			interactiveText.Margin = new Thickness(0, 0, 0, 0);
			interactiveText.IsEnabled = true;
			interactiveText.AcceptsReturn = true;
			interactiveText.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			return interactiveText;
		}
	}
}