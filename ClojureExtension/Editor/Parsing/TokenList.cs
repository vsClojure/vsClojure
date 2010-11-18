using System.Collections.Generic;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public class TokenList
	{
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;

		public TokenList(Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_tokenizedBuffer = tokenizedBuffer;
		}

		public LinkedList<BufferMappedTokenData> Intersection(int startIndex, int length)
		{
			int currentIndex = 0;
			int endPosition = startIndex + length - 1;
			LinkedListNode<Token> currentToken = _tokenizedBuffer.CurrentState.First;
			LinkedList<BufferMappedTokenData> intersection = new LinkedList<BufferMappedTokenData>();

			while (currentToken != null && currentIndex + currentToken.Value.Length <= startIndex)
			{
				currentIndex += currentToken.Value.Length;
				currentToken = currentToken.Next;
			}

			while (currentToken != null && currentIndex <= endPosition)
			{
				intersection.AddLast(new BufferMappedTokenData(currentIndex, currentToken.Value, currentToken));
				currentIndex += currentToken.Value.Length;
				currentToken = currentToken.Next;
			}

			return intersection;
		}
	}
}