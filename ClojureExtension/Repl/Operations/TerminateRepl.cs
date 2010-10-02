using System.Windows.Controls;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Repl.Operations
{
    public class TerminateRepl
    {
        private readonly ReplData _replData;
        private readonly TabControl _tabControl;
        private readonly ReplTextPipe _replTextPipe;
        private readonly ReplStorage _replStorage;

        public TerminateRepl(ReplData replData, TabControl tabControl, ReplTextPipe replTextPipe, ReplStorage replStorage)
        {
            _replData = replData;
            _tabControl = tabControl;
            _replTextPipe = replTextPipe;
            _replStorage = replStorage;
        }

        public void Execute()
        {
            _replData.ReplProcess.Kill();
            _replStorage.RemoveRepl(_replData);
            _tabControl.Items.Remove(_replData.Tab);
        }
    }
}