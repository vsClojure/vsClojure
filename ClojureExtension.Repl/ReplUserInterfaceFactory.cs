using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClojureExtension.Repl
{
	public class ReplUserInterfaceFactory
	{
		public static TabControl CreateTabControl()
		{
			TabControl tabControl = new TabControl();
			tabControl.HorizontalAlignment = HorizontalAlignment.Stretch;
			tabControl.VerticalAlignment = VerticalAlignment.Stretch;
			tabControl.Padding = new Thickness(2);
			return tabControl;
		}

		public static TabItem CreateTabItem(WrapPanel headerPanel, Grid textBoxGrid)
		{
			TabItem tabItem = new TabItem();
			tabItem.Header = headerPanel;
			tabItem.Content = textBoxGrid;
			return tabItem;
		}

		public static WrapPanel CreateHeaderPanel(Label replName, Button closeButton)
		{
			WrapPanel headerPanel = new WrapPanel();
			headerPanel.Children.Add(replName);
			headerPanel.Children.Add(closeButton);
			return headerPanel;
		}

		public static Grid CreateTextBoxGrid(TextBox textBox)
		{
			Grid grid = new Grid();
			grid.Children.Add(textBox);
			return grid;
		}

		public static Label CreateTabLabel()
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

		public static Button CreateCloseButton()
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

		public static TextBox CreateInteractiveText()
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