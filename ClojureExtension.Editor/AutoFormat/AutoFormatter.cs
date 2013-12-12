using System.Collections.Generic;
using System.Text;
using ClojureExtension.Editor.InputHandling;
using ClojureExtension.Editor.TextBuffer;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.ClojureExtension.Editor.Options;

namespace Microsoft.ClojureExtension.Editor.AutoFormat
{
	public class AutoFormatter
	{
		private readonly ITextBufferAdapter _textBuffer;
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;
		private Stack<Token> _dataStructureStack;
		private LinkedListNode<Token> _currentToken;
		private int _currentLineIndex;
		private Stack<int> _indentStack;

		public AutoFormatter(ITextBufferAdapter textBuffer, Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_textBuffer = textBuffer;
			_tokenizedBuffer = tokenizedBuffer;
		}

		public void Format(EditorOptions editorOptions)
		{
			StringBuilder output = new StringBuilder();
			_currentToken = _tokenizedBuffer.CurrentState.First;
			_dataStructureStack = new Stack<Token>();
			_indentStack = new Stack<int>();

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
					else if (tokenText.Contains("\r\n")) tokenText = "\r\n" + GetIndent(editorOptions);
					else if (_currentToken.Next.Value.Type.IsBraceEnd()) tokenText = "";
					else if (_currentToken.Previous.Value.Type.IsBraceStart()) tokenText = "";
					else tokenText = " ";
				}
				else if (_currentToken.Next != null && _currentToken.Next.Value.Type != TokenType.Whitespace)
				{
					if (_currentToken.Next.Value.Type == TokenType.Comment) tokenText += " ";
					else if (_dataStructureStack.Count == 0) tokenText += "\r\n\r\n";
					else if (!_currentToken.Value.Type.IsBrace() && !_currentToken.Next.Value.Type.IsBrace()) tokenText += " ";	
				}

				if (tokenText.Contains("\r\n")) _currentLineIndex = tokenText.Count(' ');
				else _currentLineIndex += tokenText.Length;

				if (_currentToken.Value.Type.IsBraceStart()) _indentStack.Push(_currentLineIndex - tokenText.Count(' '));
				else if (_indentStack.Count > _dataStructureStack.Count) _indentStack.Pop();

				_currentToken = _currentToken.Next;
				output.Append(tokenText);
			}

			_textBuffer.SetText(output.ToString());
		}

		private string GetIndent(EditorOptions editorOptions)
		{
			if (_dataStructureStack.Count == 0) return "";
			if (_dataStructureStack.Peek().Type == TokenType.ListStart) return " ".Repeat(editorOptions.IndentSize + _indentStack.Peek() - 1);
			return " ".Repeat(_indentStack.Peek());
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
	}
}