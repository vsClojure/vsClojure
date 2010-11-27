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
		private Stack<Token> _dataStructureStack;
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
			_dataStructureStack = new Stack<Token>();

			while (currentToken != null)
			{
				string tokenText = currentToken.Value.Text;
				if (currentToken.Value.Type.IsBraceStart()) _dataStructureStack.Push(currentToken.Value);
				if (currentToken.Value.Type.IsBraceEnd() && _dataStructureStack.Count > 0 && _dataStructureStack.Peek().Type.MatchingBraceType() == currentToken.Value.Type) _dataStructureStack.Pop();

				if (currentToken.Value.Type == TokenType.Whitespace)
				{
					if (currentToken.Previous == null) tokenText = "";
					else if (currentToken.Next == null) tokenText = "";
					else if (currentToken.Next.Value.Type == TokenType.Comment && !tokenText.Contains("\r\n")) tokenText = tokenText;
					else if (_dataStructureStack.Count == 0 && currentToken.Previous.Value.Type != TokenType.Comment && currentToken.Next.Value.Type == TokenType.Comment) tokenText = "\r\n\r\n";
					else if (_dataStructureStack.Count == 0 && currentToken.Previous.Value.Type == TokenType.Comment && currentToken.Next.Value.Type != TokenType.Comment) tokenText = "\r\n\r\n";
					else if (_dataStructureStack.Count == 0 && currentToken.Previous.Value.Type == TokenType.Comment && currentToken.Next.Value.Type == TokenType.Comment) tokenText = "\r\n";
					else if (_dataStructureStack.Count == 0) tokenText = "\r\n\r\n";
					else if (currentToken.Next.Value.Type.IsBraceEnd()) tokenText = "";
					else if (tokenText.Contains("\r\n")) tokenText = "\r\n" + GetIndent();
					else if (currentToken.Previous.Value.Type.IsBraceStart()) tokenText = "";
					else tokenText = " ";
				}

				output.Append(tokenText);
				currentToken = currentToken.Next;
			}

			_textBuffer.SetText(output.ToString());
		}

		private string GetIndent()
		{
			string indent = "";
			int indentAmount = IndentSize * _dataStructureStack.Count;
			if (_dataStructureStack.Count > 0 && _dataStructureStack.Peek().Type != TokenType.ListStart) indentAmount = IndentSize * (_dataStructureStack.Count - 1) + 1;
			for (int i = 0; i < indentAmount; i++) indent += " ";
			return indent;
		}
	}
}