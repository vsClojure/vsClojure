// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.Collections.Generic;
using System.IO;

namespace ClojureExtension.Parsing
{
	public class Tokenizer
	{
		public LinkedList<Token> Tokenize(string input)
		{
			return Tokenize(input, input.Length);
		}

		public LinkedList<Token> Tokenize(string input, int length)
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader(input)));
			Token currentToken = lexer.Next();
			LinkedList<Token> tokenList = new LinkedList<Token>();
			int currentIndex = 0;

			while (currentToken != null && currentIndex < length)
			{
				tokenList.AddLast(currentToken);
				currentIndex += currentToken.Length;
				currentToken = lexer.Next();
			}

			return tokenList;
		}
	}
}