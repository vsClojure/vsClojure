namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public enum TokenType
	{
		Unknown,
		ListStart,
		ListEnd,
		VectorStart,
		VectorEnd,
		MapStart,
		MapEnd,
		Number,
		HexNumber,
		String,
		Whitespace,
		Comment,
		Symbol,
		Keyword,
		Boolean,
		Nil,
		BuiltIn
		//TypeHint
	}
}
