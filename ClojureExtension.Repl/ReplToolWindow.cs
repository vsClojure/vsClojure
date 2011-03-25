using System.Runtime.InteropServices;
using System.Windows.Controls;
using Microsoft.VisualStudio.Shell;

namespace ClojureExtension.Repl
{
    [Guid("8C5C7302-ECC8-435D-AAFE-D0E5A0A02FE9")]
    public class ReplToolWindow : ToolWindowPane
    {
        private readonly TabControl _replManager;

        public TabControl TabControl
        {
            get { return _replManager; }
        }

        public ReplToolWindow()
            : this(ReplUserInterfaceFactory.CreateTabControl())
        {
        }

        public ReplToolWindow(TabControl replManager) :
            base(null)
        {
            _replManager = replManager;
            Caption = "Repl Manager";
            BitmapResourceID = 301;
            BitmapIndex = 1;
            base.Content = _replManager;
        }
    }
}