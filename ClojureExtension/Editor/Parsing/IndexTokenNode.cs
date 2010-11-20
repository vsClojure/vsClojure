using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ClojureExtension.Editor.Parsing
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
	}
}
