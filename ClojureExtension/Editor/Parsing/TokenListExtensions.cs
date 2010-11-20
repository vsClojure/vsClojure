using System;
using System.Collections.Generic;

namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public static class TokenListExtensions
	{
		public static IndexTokenNode FindTokenAtIndex(this LinkedList<Token> list, int index)
		{
			int currentIndex = 0;
			LinkedListNode<Token> currentToken = list.First;

			while (currentToken != null && currentIndex + currentToken.Value.Length <= index)
			{
				currentIndex += currentToken.Value.Length;
				currentToken = currentToken.Next;
			}

			if (currentToken == null) throw new Exception("Could not find node at index: " + index);
			return new IndexTokenNode(new IndexToken(currentIndex, currentToken.Value), currentToken);
		}

		public static LinkedList<IndexToken> Intersection(this LinkedList<Token> list, int startIndex, int length)
		{
			int currentIndex = 0;
			int endPosition = startIndex + length - 1;
			LinkedListNode<Token> currentToken = list.First;
			LinkedList<IndexToken> intersection = new LinkedList<IndexToken>();

			while (currentToken != null && currentIndex + currentToken.Value.Length <= startIndex)
			{
				currentIndex += currentToken.Value.Length;
				currentToken = currentToken.Next;
			}

			while (currentToken != null && currentIndex <= endPosition)
			{
				intersection.AddLast(new IndexToken(currentIndex, currentToken.Value));
				currentIndex += currentToken.Value.Length;
				currentToken = currentToken.Next;
			}

			return intersection;
		}
	}
}