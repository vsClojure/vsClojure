using System.Collections.Generic;
using System.IO;
using ClojureExtension.Editor.TextBuffer;
using ClojureExtension.Parsing;

namespace ClojureExtension.Editor.Commenting
{
	public class BlockUncomment
	{
		private readonly ITextBufferAdapter _textBuffer;

		public BlockUncomment(ITextBufferAdapter textBuffer)
		{
			_textBuffer = textBuffer;
		}

		public void Execute()
		{
			List<string> lines = _textBuffer.GetSelectedLines();
			List<string> uncommentedLines = new List<string>();

			foreach (string line in lines)
			{
				Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader(line)));
				Token currentToken = lexer.Next();
				while (currentToken != null && currentToken.Type == TokenType.Whitespace) currentToken = lexer.Next();
				if (currentToken == null) uncommentedLines.Add(line);
				else if (currentToken.Type != TokenType.Comment) uncommentedLines.Add(line);
				else uncommentedLines.Add(line.Remove(currentToken.StartIndex, 1));
			}

			_textBuffer.ReplaceSelectedLines(uncommentedLines);
		}
	}
}