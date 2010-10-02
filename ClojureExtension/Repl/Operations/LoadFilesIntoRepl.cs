using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Repl.Operations
{
    internal class LoadFilesIntoRepl
    {
        private readonly ReplWriter _writer;
        private readonly IProvider<ReplData> _replProvider;
        private readonly IProvider<List<string>> _filesProvider;
        private readonly IVsWindowFrame _replToolWindowFrame;

        public LoadFilesIntoRepl(
            ReplWriter writer,
            IProvider<ReplData> replProvider,
            IProvider<List<string>> filesProvider,
            IVsWindowFrame replToolWindowFrame)
        {
            _writer = writer;
            _replProvider = replProvider;
            _filesProvider = filesProvider;
            _replToolWindowFrame = replToolWindowFrame;
        }

        public void Execute()
        {
            IEnumerable<string> filesToLoad = _filesProvider.Get().Where(p => p.ToLower().EndsWith(".clj"));

            if (filesToLoad.Count() == 0) throw new Exception("No files to load.");
            if (_replProvider.Get() == null) throw new Exception("No active repl.");

            StringBuilder loadFileExpression = new StringBuilder("(map load-file '(");
            filesToLoad.ToList().ForEach(path => loadFileExpression.Append(" \"").Append(path.Replace("\\", "\\\\")).Append("\""));
            loadFileExpression.Append("))");

            _writer.WriteBehindTheSceneExpressionToRepl(_replProvider.Get(), loadFileExpression.ToString());
            ErrorHandler.ThrowOnFailure(_replToolWindowFrame.Show());
        }
    }
}