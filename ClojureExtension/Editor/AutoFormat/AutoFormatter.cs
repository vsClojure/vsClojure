using System.Collections.Generic;
using System.Text;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Editor.AutoFormat
{
	public class AutoFormatter
	{
		private readonly ITextBufferAdapter _textBuffer;
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;
		private const int IndentSize = 4;

		public AutoFormatter(ITextBufferAdapter textBuffer, Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_textBuffer = textBuffer;
			_tokenizedBuffer = tokenizedBuffer;
		}

		public void Format()
		{
			StringBuilder output = new StringBuilder();
			LinkedListNode<Token> currentToken = _tokenizedBuffer.CurrentState.First;
			Stack<Token> dataStructureStack = new Stack<Token>();

			while (currentToken != null)
			{
				LinkedListNode<Token> nextToken = currentToken.Next;
				if (currentToken.Value.Type.IsBraceStart()) dataStructureStack.Push(currentToken.Value);
				if (currentToken.Value.Type.IsBraceEnd() && dataStructureStack.Count > 0 && dataStructureStack.Peek().Type.MatchingBraceType() == currentToken.Value.Type) dataStructureStack.Pop();
				if (currentToken.Value.Type != TokenType.Whitespace) output.Append(currentToken.Value.Text);

				if (nextToken != null && nextToken.Value.Type == TokenType.Whitespace)
				{
					string whitespace = nextToken.Value.Text;
					whitespace = whitespace.Contains("\r\n") ? "\r\n" : !currentToken.Value.Type.IsBraceStart() ? " " : "";

					if (whitespace == "\r\n" && currentToken.Value.Type != TokenType.Comment)
					{
						int indentAmount = IndentSize*dataStructureStack.Count;
						if (dataStructureStack.Count > 0 && dataStructureStack.Peek().Type != TokenType.ListStart) indentAmount = IndentSize*(dataStructureStack.Count - 1) + 1;
						for (int i = 0; i < indentAmount; i++) whitespace += " ";
					}

					if (currentToken.Value.Type == TokenType.Comment && dataStructureStack.Count == 0)
					{
						whitespace = nextToken.Value.Text.Contains("\r\n\r\n") ? "\r\n\r\n" : whitespace;
					}

					if (nextToken.Next == null || nextToken.Next.Value.Type.IsBraceEnd()) whitespace = "";
					if (nextToken.Next != null && dataStructureStack.Count == 0 && currentToken.Value.Type != TokenType.Comment) whitespace = "\r\n\r\n";
					output.Append(whitespace);
				}

				currentToken = nextToken;
				if (currentToken != null && currentToken.Value.Type == TokenType.Whitespace) currentToken = currentToken.Next;
			}

			_textBuffer.SetText(output.ToString());
		}
	}
}