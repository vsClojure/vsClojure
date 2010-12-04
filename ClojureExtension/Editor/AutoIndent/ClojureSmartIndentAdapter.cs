using Microsoft.ClojureExtension.Editor.Options;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Editor.AutoIndent
{
	public class ClojureSmartIndentAdapter
	{
		private readonly ClojureSmartIndent _clojureSmartIndent;
		private readonly IProvider<EditorOptions> _optionsBuilder;

		public ClojureSmartIndentAdapter(ClojureSmartIndent clojureSmartIndent, IProvider<EditorOptions> optionsBuilder)
		{
			_clojureSmartIndent = clojureSmartIndent;
			_optionsBuilder = optionsBuilder;
		}

		public int GetIndent(int position)
		{
			return _clojureSmartIndent.GetDesiredIndentation(position, _optionsBuilder.Get());
		}
	}
}