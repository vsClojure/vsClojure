using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public class BufferTextChangeHandler
	{
		private readonly ITextBufferAdapter _textBuffer;
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;
		private readonly TokenList _tokenList;
		private readonly Tokenizer _tokenizer;
		public event EventHandler<TokenChangedEventArgs> TokenChanged;

		public BufferTextChangeHandler(
			ITextBufferAdapter textBuffer,
			Entity<LinkedList<Token>> tokenizedBuffer,
			TokenList tokenList,
			Tokenizer tokenizer)
		{
			_textBuffer = textBuffer;
			_tokenizedBuffer = tokenizedBuffer;
			_tokenList = tokenList;
			_tokenizer = tokenizer;
		}

		public void OnTextChanged(List<TextChangeData> changes)
		{
			foreach (var change in changes)
			{
				LinkedList<BufferMappedTokenData> intersectingTokens = _tokenList.Intersection(change.Position - 1, change.Length + 1);
				if (intersectingTokens.Count == 0) continue;

				int startIndex = intersectingTokens.First.Value.StartIndex;
				int intersectionLength = intersectingTokens.Sum(t => t.Token.Length);
				LinkedList<Token> newTokens = _tokenizer.Tokenize(_textBuffer.GetText(startIndex), intersectionLength + change.Delta);
				int newTokenListLength = newTokens.Sum(t => t.Length);

				while (intersectionLength + change.Delta != newTokenListLength)
				{
					while (startIndex + intersectionLength + change.Delta < _textBuffer.Length && intersectionLength + change.Delta < newTokenListLength)
					{
						BufferMappedTokenData incorrectToken = NextToken(intersectingTokens);
						intersectingTokens.AddLast(incorrectToken);
						intersectionLength += incorrectToken.Token.Length;
					}

					LinkedList<Token> moreNewTokens = _tokenizer.Tokenize(_textBuffer.GetText(startIndex + newTokenListLength), intersectionLength + change.Delta - newTokenListLength);
					foreach (Token t in moreNewTokens) newTokens.AddLast(t);
					newTokenListLength += moreNewTokens.Sum(t => t.Length);
				}

				foreach (var newToken in newTokens) _tokenizedBuffer.CurrentState.AddBefore(intersectingTokens.First.Value.Node, newToken);
				foreach (var oldToken in intersectingTokens) _tokenizedBuffer.CurrentState.Remove(oldToken.Node);

				int currentLength = 0;

				foreach (var newToken in newTokens)
				{
					TokenChanged(this, new TokenChangedEventArgs(new BufferMappedTokenData(startIndex + currentLength, newToken, null)));
					currentLength += newToken.Length;
				}
			}
		}

		private BufferMappedTokenData NextToken(LinkedList<BufferMappedTokenData> intersectingTokens)
		{
			BufferMappedTokenData lastToken = intersectingTokens.Last.Value;
			int nextTokenIndex = lastToken.StartIndex + lastToken.Token.Length;
			return new BufferMappedTokenData(nextTokenIndex, lastToken.Node.Next.Value, lastToken.Node.Next);
		}
	}
}