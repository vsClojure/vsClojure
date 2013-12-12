using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClojureExtension.Utilities;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace ClojureExtension.Repl.Operations
{
	internal class LoadFilesIntoRepl
	{
		private readonly ReplWriter _writer;
		private readonly IProvider<List<string>> _filesProvider;
		private readonly IVsWindowFrame _replToolWindowFrame;

		public LoadFilesIntoRepl(
				ReplWriter writer,
				IProvider<List<string>> filesProvider,
				IVsWindowFrame replToolWindowFrame)
		{
			_writer = writer;
			_filesProvider = filesProvider;
			_replToolWindowFrame = replToolWindowFrame;
		}

		public void Execute()
		{
			IEnumerable<string> filesToLoad = _filesProvider.Get().Where(p => p.ToLower().EndsWith(".clj") || p.ToLower().EndsWith(".cljs"));

			if (filesToLoad.Count() == 0) throw new Exception("No files to load.");

			StringBuilder loadFileExpression = new StringBuilder("(map load-file '(");
			filesToLoad.ToList().ForEach(path => loadFileExpression.Append(" \"").Append(path.Replace("\\", "\\\\")).Append("\""));
			loadFileExpression.Append("))");

			_writer.WriteBehindTheSceneExpressionToRepl(loadFileExpression.ToString());
			ErrorHandler.ThrowOnFailure(_replToolWindowFrame.ShowNoActivate());
		}
	}
}
