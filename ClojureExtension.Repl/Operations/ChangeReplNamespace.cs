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
			_replWriter.WriteBehindTheSceneExpressionToRepl("(in-ns '" + ns + ")");
		}
	}
}