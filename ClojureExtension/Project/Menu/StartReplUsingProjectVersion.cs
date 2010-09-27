using System.Diagnostics;
using System.Windows.Controls;
using Microsoft.ClojureExtension.Repl;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Project.Menu
{
    public class StartReplUsingProjectVersion
    {
        private readonly ReplStorage _storage;
        private readonly TabControl _replManager;
        private readonly ReplTabFactory _replTabFactory;
        private readonly ReplLauncher _replLauncher;
        private readonly IVsWindowFrame _toolWindowFrame;

        public StartReplUsingProjectVersion(
            ReplStorage storage,
            TabControl replManager,
            ReplTabFactory replTabFactory,
            ReplLauncher replLauncher,
            IVsWindowFrame toolWindowFrame)
        {
            _storage = storage;
            _replManager = replManager;
            _replTabFactory = replTabFactory;
            _replLauncher = replLauncher;
            _toolWindowFrame = toolWindowFrame;
        }

        public void Execute()
        {
            Process process = _replLauncher.Execute();
            TextBox interactiveText = _replTabFactory.CreateInteractiveTextBox();
            TabItem replTab = _replTabFactory.CreateTab(interactiveText);
            ReplData replData = new ReplData(process, interactiveText, replTab);
            _storage.SaveRepl(replData);

            _replManager.Items.Add(replTab);
            _replManager.SelectedItem = replTab;
            ReplTextPipe replTextPipe = _replTabFactory.CreateReplTextPipe(replData);
            replTextPipe.StartMarshallingText();
            ErrorHandler.ThrowOnFailure(_toolWindowFrame.Show());
        }
    }
}