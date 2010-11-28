using System;
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
		private LinkedListNode<Token> _currentToken;
		private const int IndentSize = 4;

		public AutoFormatter(ITextBufferAdapter textBuffer, Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_textBuffer = textBuffer;
			_tokenizedBuffer = tokenizedBuffer;
		}

		public void Format()
		{
			StringBuilder output = new StringBuilder();
			_currentToken = _tokenizedBuffer.CurrentState.First;
			_dataStructureStack = new Stack<Token>();

			while (_currentToken != null)
			{
				string tokenText = _currentToken.Value.Text;
				if (_currentToken.Value.Type.IsBraceStart()) _dataStructureStack.Push(_currentToken.Value);
				if (_currentToken.Value.Type.IsBraceEnd() && _dataStructureStack.Count > 0 && _dataStructureStack.Peek().Type.MatchingBraceType() == _currentToken.Value.Type) _dataStructureStack.Pop();

				if (_currentToken.Value.Type == TokenType.Whitespace)
				{
					bool moreThanOneLineBreak = tokenText.Replace(" ", "").Contains("\r\n\r\n");
					bool hasAtLeastOneLineBreak = tokenText.Replace(" ", "").Contains("\r\n");

					if (_currentToken.Previous == null) tokenText = "";
					else if (_currentToken.Next == null) tokenText = "";
					else if (_currentToken.Next.Value.Type == TokenType.Comment && !tokenText.Contains("\r\n")) tokenText = " ";
					else if (_dataStructureStack.Count == 0 && IsPreviousTokenACommentOnTheSameLineAsExpression()) tokenText = "\r\n\r\n";
					else if (_dataStructureStack.Count == 0 && _currentToken.Previous.Value.Type != TokenType.Comment) tokenText = "\r\n\r\n";
					else if (_dataStructureStack.Count == 0) tokenText = moreThanOneLineBreak ? "\r\n\r\n" : hasAtLeastOneLineBreak ? "\r\n" : "";
					else if (_currentToken.Next.Value.Type == TokenType.Comment && !tokenText.EndsWith(" ")) tokenText = "\r\n";
					else if (tokenText.Contains("\r\n")) tokenText = "\r\n" + GetIndent();
					else if (_currentToken.Next.Value.Type.IsBraceEnd()) tokenText = "";
					else if (_currentToken.Previous.Value.Type.IsBraceStart()) tokenText = "";
					else tokenText = " ";
				}
				else if (_currentToken.Next != null && _currentToken.Next.Value.Type == TokenType.Comment) tokenText += " ";

				output.Append(tokenText);
				_currentToken = _currentToken.Next;
			}

			_textBuffer.SetText(output.ToString());
		}

		private bool IsPreviousTokenACommentOnTheSameLineAsExpression()
		{
			if (_currentToken.Previous.Value.Type == TokenType.Comment)
			{
				LinkedListNode<Token> commentToken = _currentToken.Previous;
				if (commentToken.Previous == null) return false;
				if (commentToken.Previous.Value.Type != TokenType.Whitespace) return true;
				return !commentToken.Previous.Value.Text.Contains("\r\n");
			}

			return false;
		}

		private string GetIndent()
		{
			string indent = "";
			int indentAmount = IndentSize*_dataStructureStack.Count;
			if (_dataStructureStack.Count > 0 && _dataStructureStack.Peek().Type != TokenType.ListStart) indentAmount = IndentSize*(_dataStructureStack.Count - 1) + 1;
			for (int i = 0; i < indentAmount; i++) indent += " ";
			return indent;
		}
	}
}