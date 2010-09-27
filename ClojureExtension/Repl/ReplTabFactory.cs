using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplTabFactory
    {
        public TextBox CreateInteractiveTextBox()
        {
            TextBox interactiveText = new TextBox();
            interactiveText.HorizontalAlignment = HorizontalAlignment.Stretch;
            interactiveText.VerticalAlignment = VerticalAlignment.Stretch;
            interactiveText.FontSize = 12;
            interactiveText.FontFamily = new FontFamily("Courier New");
            interactiveText.Margin = new Thickness(28, 0, 0, 0);
            interactiveText.IsEnabled = true;
            interactiveText.AcceptsReturn = true;
            return interactiveText;
        }

        public ReplTextPipe CreateReplTextPipe(ReplData replData)
        {
            ReplTextPipe textPipe = new ReplTextPipe(replData, new ReplWriter());
            replData.InteractiveTextBox.PreviewKeyDown += (o, e) => textPipe.WriteFromTextBoxToRepl(e.Key == Key.Enter ? "\r\n" : "");
            return textPipe;
        }

        public TabItem CreateTab(TextBox interactiveText)
        {
            Button interruptButton = new Button();
            interruptButton.Width = 26;
            interruptButton.Padding = new Thickness(1);
            interruptButton.Margin = new Thickness(0, 7, 0, 0);
            interruptButton.Content = "I";

            Button terminateButton = new Button();
            terminateButton.Width = 26;
            terminateButton.Padding = new Thickness(1);
            terminateButton.Margin = new Thickness(0, 7, 0, 0);
            terminateButton.Content = "T";

            Button clearButton = new Button();
            clearButton.Width = 26;
            clearButton.Padding = new Thickness(1);
            clearButton.Margin = new Thickness(0, 7, 0, 0);
            clearButton.Content = "C";

            ToolBarPanel toolBarPanel = new ToolBarPanel();
            toolBarPanel.Children.Add(interruptButton);
            toolBarPanel.Children.Add(terminateButton);
            toolBarPanel.Children.Add(clearButton);
            toolBarPanel.VerticalAlignment = VerticalAlignment.Stretch;
            toolBarPanel.HorizontalAlignment = HorizontalAlignment.Left;
            toolBarPanel.Width = 28;
            toolBarPanel.Orientation = Orientation.Vertical;

            Grid grid = new Grid();
            grid.Children.Add(toolBarPanel);
            grid.Children.Add(interactiveText);

            TabItem tabItem = new TabItem();
            tabItem.Header = "Repl";
            tabItem.Content = grid;
            return tabItem;
        }
    }
}