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
        private readonly ReplTabFactory _replTabFactory;
        private readonly ReplLauncher _replLauncher;

        public TabControl ReplManager
        {
            get { return _replManager; }
        }

        public ReplToolWindow()
            : this(new ReplTabControlFactory().CreateTabControl(), new ReplTabFactory(), new ReplLauncher())
        {
        }

        public ReplToolWindow(TabControl replManager, ReplTabFactory replTabFactory, ReplLauncher replLauncher) :
            base(null)
        {
            _replManager = replManager;
            _replTabFactory = replTabFactory;
            _replLauncher = replLauncher;

            Caption = "Repl Manager";
            BitmapResourceID = 301;
            BitmapIndex = 1;
            base.Content = _replManager;
        }

        public void CreateNewRepl()
        {
            Process process = _replLauncher.Execute();
            TextBox interactiveText = _replTabFactory.CreateInteractiveTextBox();
            ReplTextPipe replTextPipe = _replTabFactory.CreateReplTextPipe(process, interactiveText);
            TabItem replTab = _replTabFactory.CreateTab(interactiveText, replTextPipe);
            _replManager.Items.Add(replTab);
            _replManager.SelectedItem = replTab;
            replTextPipe.StartMarshallingText();
        }
    }
}