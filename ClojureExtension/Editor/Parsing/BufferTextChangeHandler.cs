using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public class BufferTextChangeHandler
	{
		private readonly ITextBufferAdapter _textBuffer;
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;
		public event EventHandler<TokenChangedEventArgs> TokenChanged;

		public BufferTextChangeHandler(ITextBufferAdapter textBuffer, Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_textBuffer = textBuffer;
			_tokenizedBuffer = tokenizedBuffer;
		}

		public void OnTextChanged(List<TextChangeData> changes)
		{
			foreach (var change in changes)
			{
				IndexTokenNode firstToken = _tokenizedBuffer.CurrentState.FindTokenAtIndex(change.Position - 1);
				Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader(_textBuffer.GetText(firstToken.IndexToken.StartIndex))));
				int oldBufferStartIndex = firstToken.IndexToken.StartIndex + change.Delta;
				int newBufferStartIndex = firstToken.IndexToken.StartIndex;
				LinkedList<LinkedListNode<Token>> oldTokens = new LinkedList<LinkedListNode<Token>>();
				LinkedList<IndexToken> newTokens = new LinkedList<IndexToken>();
				Token newToken = lexer.Next();
				LinkedListNode<Token> oldToken = firstToken.Node;

				while (newBufferStartIndex + newToken.Length != oldBufferStartIndex + oldToken.Value.Length)
				{
					if (newBufferStartIndex + newToken.Length < oldBufferStartIndex + oldToken.Value.Length)
					{
						newTokens.AddLast(new IndexToken(newBufferStartIndex, newToken));
						newBufferStartIndex += newToken.Length;
						newToken = lexer.Next();
					}
					else
					{
						oldTokens.AddLast(oldToken);
						oldBufferStartIndex += oldToken.Value.Length;
						oldToken = oldToken.Next;
					}
				}

				oldTokens.AddLast(oldToken);
				newTokens.AddLast(new IndexToken(newBufferStartIndex, newToken));
				foreach (var t in newTokens) _tokenizedBuffer.CurrentState.AddBefore(firstToken.Node, t.Token);
				foreach (var t in oldTokens) _tokenizedBuffer.CurrentState.Remove(t);
				foreach (var t in newTokens) TokenChanged(this, new TokenChangedEventArgs(t));
			}
		}
	}
}