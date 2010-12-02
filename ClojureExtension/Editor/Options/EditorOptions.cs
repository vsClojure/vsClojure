namespace Microsoft.ClojureExtension.Editor.Options
{
	public class EditorOptions
	{
		private readonly int _indentSize;
		private readonly bool _useSpaces;

		public EditorOptions(int indentSize, bool useSpaces)
		{
			_indentSize = indentSize;
			_useSpaces = useSpaces;
		}

		public bool UseSpaces
		{
			get { return _useSpaces; }
		}

		public int IndentSize
		{
			get { return _indentSize; }
		}
	}
}