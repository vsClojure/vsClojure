using System.Collections.Generic;

namespace ClojureExtension.Parsing
{
	public static class TokenListExtensions
	{
		public static IndexTokenNode FindTokenAtIndex(this LinkedList<Token> list, int index)
		{
			if (list.Count == 0) return null;
			int currentIndex = 0;
			LinkedListNode<Token> currentToken = list.First;

			while (currentToken != null && currentIndex + currentToken.Value.Length <= index)
			{
				currentIndex += currentToken.Value.Length;
				currentToken = currentToken.Next;
			}

			if (currentToken == null && currentIndex > 0)
			{
				int lastTokenIndex = currentIndex - list.Last.Value.Length;
				return new IndexTokenNode(new IndexToken(lastTokenIndex, list.Last.Value), list.Last);
			}

			return new IndexTokenNode(new IndexToken(currentIndex, currentToken.Value), currentToken);
		}

		public static IndexTokenNode FindTokenBeforeIndex(this LinkedList<Token> list, int index)
		{
			if (list.Count == 0) return null;
			int currentIndex = 0;
			LinkedListNode<Token> currentToken = list.First;

			while (currentToken != null && currentIndex + currentToken.Value.Length <= index)
			{
				currentIndex += currentToken.Value.Length;
				currentToken = currentToken.Next;
			}

			if (currentToken == null && currentIndex > 0)
			{
				int lastTokenIndex = currentIndex - list.Last.Value.Length;
				return new IndexTokenNode(new IndexToken(lastTokenIndex, list.Last.Value), list.Last);
			}

			if (currentIndex == index && currentToken.Previous != null)
			{
				currentIndex -= currentToken.Previous.Value.Length;
				currentToken = currentToken.Previous;
			}

			if (currentToken.Previous != null)
			{
				currentIndex -= currentToken.Previous.Value.Length;
				currentToken = currentToken.Previous;
			}

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