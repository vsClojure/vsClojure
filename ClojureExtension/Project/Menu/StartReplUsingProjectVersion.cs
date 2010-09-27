using Microsoft.ClojureExtension.Repl;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Project.Menu
{
    public class StartReplUsingProjectVersion
    {
        private readonly ReplToolWindow _replToolWindow;
        private readonly IVsWindowFrame _toolWindowFrame;

        public StartReplUsingProjectVersion(ReplToolWindow replToolWindow, IVsWindowFrame toolWindowFrame)
        {
            _replToolWindow = replToolWindow;
            _toolWindowFrame = toolWindowFrame;
        }

        public void Execute()
        {
            ErrorHandler.ThrowOnFailure(_toolWindowFrame.Show());
            _replToolWindow.CreateNewRepl();
        }
    }
}