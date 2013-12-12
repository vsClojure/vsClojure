using System;
using System.Collections.Generic;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.ClojureExtension.Editor.Options;

namespace Microsoft.ClojureExtension.Editor.AutoIndent
{
	public class ClojureSmartIndent
	{
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;

		public ClojureSmartIndent(Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_tokenizedBuffer = tokenizedBuffer;
		}

		public int GetDesiredIndentation(int position, EditorOptions options)
		{
			if (_tokenizedBuffer.CurrentState.Count == 0) return 0;
			IndexTokenNode currentToken = _tokenizedBuffer.CurrentState.FindTokenAtIndex(position);
			currentToken = currentToken.Previous();
			IndexTokenNode firstOpenBrace = null;
			int braceCount = 0;

			while (currentToken != null && firstOpenBrace == null)
			{
				if (currentToken.IndexToken.Token.Type.IsBraceEnd()) braceCount--;
				if (currentToken.IndexToken.Token.Type.IsBraceStart()) braceCount++;
				if (braceCount == 1) firstOpenBrace = currentToken;
				else currentToken = currentToken.Previous();
			}

			if (firstOpenBrace == null) return 0;

			int previousLineLength = 0;
			IndexTokenNode startOfLine = firstOpenBrace.Previous();

			while (startOfLine != null && (startOfLine.IndexToken.Token.Type != TokenType.Whitespace || (startOfLine.IndexToken.Token.Type == TokenType.Whitespace && !startOfLine.IndexToken.Token.Text.Contains("\n"))))
			{
				previousLineLength += startOfLine.IndexToken.Token.Length;
				startOfLine = startOfLine.Previous();
			}

			int previousIndentAmount = 0;

			if (startOfLine != null)
			{
				string startOfLineText = startOfLine.Node.Value.Text;
				string lineWhitespaceWithoutIndent = startOfLineText.TrimEnd(new[] {' '});
				previousIndentAmount = startOfLineText.Length - lineWhitespaceWithoutIndent.Length;
			}

			if (firstOpenBrace.Node.Value.Type == TokenType.ListStart) return previousIndentAmount + previousLineLength + options.IndentSize;
			return previousIndentAmount + previousLineLength + 1;
		}
	}
}
     