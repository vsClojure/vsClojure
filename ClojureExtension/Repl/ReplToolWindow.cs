using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ClojureExtension.Repl
{
    [Guid("0c0e8214-0808-42e5-ccea-b965a2126fb9")]
    public class ReplToolWindow : ToolWindowPane
    {
        private readonly TabControl _replManager;

        public TabControl TabControl
        {
            get { return _replManager; }
        }

        public ReplToolWindow()
            : this(new ReplTabControlFactory().CreateTabControl())
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