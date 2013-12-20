// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.Collections.Generic;

namespace ClojureExtension.Parsing
{
	public class IndexTokenNode
	{
		private readonly IndexToken _indexToken;
		private readonly LinkedListNode<Token> _node;

		public IndexTokenNode(IndexToken indexToken, LinkedListNode<Token> node)
		{
			_indexToken = indexToken;
			_node = node;
		}

		public LinkedListNode<Token> Node
		{
			get { return _node; }
		}

		public IndexToken IndexToken
		{
			get { return _indexToken; }
		}

		public IndexTokenNode Next()
		{
			if (Node.Next == null) return null;
			IndexToken nextToken = new IndexToken(IndexToken.StartIndex + IndexToken.Token.Length, Node.Next.Value);
			return new IndexTokenNode(nextToken, Node.Next);
		}

		public IndexTokenNode Previous()
		{
			if (Node.Previous == null) return null;
			IndexToken previousToken = new IndexToken(IndexToken.StartIndex - Node.Previous.Value.Length, Node.Previous.Value);
			return new IndexTokenNode(previousToken, Node.Previous);
		}
	}
}