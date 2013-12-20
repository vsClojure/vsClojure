// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

namespace ClojureExtension.Parsing
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
		BuiltIn,
		Character,
		IgnoreReaderMacro
	}
}
