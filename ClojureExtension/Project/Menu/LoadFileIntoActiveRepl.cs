using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ClojureExtension.Repl;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Project.Menu
{
    public class LoadFileIntoActiveRepl
    {
        private readonly ReplWriter _writer;
        private readonly IProvider<ReplData> _replProvider;
        private readonly IProvider<List<string>> _selectedFilesProvider;
        private readonly IVsWindowFrame _replToolWindowFrame;

        public LoadFileIntoActiveRepl(
            ReplWriter writer,
            IProvider<ReplData> replProvider,
            IProvider<List<string>> selectedFilesProvider,
            IVsWindowFrame replToolWindowFrame)
        {
            _writer = writer;
            _replProvider = replProvider;
            _selectedFilesProvider = selectedFilesProvider;
            _replToolWindowFrame = replToolWindowFrame;
        }

        public void Execute()
        {
            IEnumerable<string> filesToLoad = _selectedFilesProvider.Get().Where(p => p.ToLower().EndsWith(".clj"));

            StringBuilder loadFileExpression = new StringBuilder("(map load-file '(");
            filesToLoad.ToList().ForEach(path => loadFileExpression.Append(" \"").Append(path.Replace("\\", "\\\\")).Append("\""));
            loadFileExpression.Append("))");

            _writer.WriteBehindTheSceneExpressionToRepl(_replProvider.Get(), loadFileExpression.ToString());
            ErrorHandler.ThrowOnFailure(_replToolWindowFrame.Show());
        }
    }
}