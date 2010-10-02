using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.ClojureExtension.Repl.Operations;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplTabFactory
    {
        public ReplData CreateRepl(Process replProcess, TabControl replManager)
        {
            TextBox interactiveText = new TextBox();
            interactiveText.HorizontalAlignment = HorizontalAlignment.Stretch;
            interactiveText.VerticalAlignment = VerticalAlignment.Stretch;
            interactiveText.FontSize = 12;
            interactiveText.FontFamily = new FontFamily("Courier New");
            interactiveText.Margin = new Thickness(0, 0, 0, 0);
            interactiveText.IsEnabled = true;
            interactiveText.AcceptsReturn = true;

            Grid grid = new Grid();
            grid.Children.Add(interactiveText);

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

            WrapPanel headerPanel = new WrapPanel();
            headerPanel.Children.Add(name);
            headerPanel.Children.Add(closeButton);

            TabItem tabItem = new TabItem();
            tabItem.Header = headerPanel;
            tabItem.Content = grid;

            ReplData replData = new ReplData(replProcess, interactiveText, tabItem);
            ReplTextPipe textPipe = new ReplTextPipe(replData, new ReplWriter());
            Thread outputReaderThread = new Thread(() => textPipe.WriteFromReplToTextBox(replProcess.StandardOutput));
            Thread errorReaderThread = new Thread(() => textPipe.WriteFromReplToTextBox(replProcess.StandardError));

            interactiveText.PreviewKeyDown +=
                (o, e) =>
                textPipe.WriteFromTextBoxToRepl(e.Key == Key.Enter ? "\r\n" : "");
            
            interactiveText.Loaded +=
                (o, e) =>
                {
                    if (outputReaderThread.IsAlive) return;
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
                (obj, sender) =>
                new TerminateRepl(replData, replManager, textPipe, ReplStorageProvider.Storage).Execute();

            return replData;
        }
    }
}