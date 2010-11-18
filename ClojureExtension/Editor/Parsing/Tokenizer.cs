using System.Collections.Generic;
using System.IO;

namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public class Tokenizer
	{
		public LinkedList<Token> Tokenize(string input)
		{
			return TokenizeUpTo(input, input.Length);
		}

		public LinkedList<Token> TokenizeUpTo(string input, int length)
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