using System.Windows;
using System.Windows.Controls;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplTabControlFactory
    {
        public TabControl CreateTabControl()
        {
            TabControl tabControl = new TabControl();
            tabControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            tabControl.VerticalAlignment = VerticalAlignment.Stretch;
            tabControl.Padding = new Thickness(2);
            return tabControl;
        }
    }
}