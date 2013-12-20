// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

namespace ClojureExtension.Repl.Operations
{
	public class ChangeReplNamespace
	{
		private readonly ReplWriter _replWriter;

		public ChangeReplNamespace(ReplWriter replWriter)
		{
			_replWriter = replWriter;
		}

		public void Execute(string ns)
		{
			_replWriter.WriteBehindTheSceneExpressionToRepl(string.Format("(in-ns '{0})", ns));
		}
	}
}