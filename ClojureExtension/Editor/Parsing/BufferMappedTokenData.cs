using System.Collections.Generic;

namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public class BufferMappedTokenData
	{
		private readonly int _startIndex;
		private readonly Token _token;
		private readonly LinkedListNode<Token> _node;

		public BufferMappedTokenData(int startIndex, Token token, LinkedListNode<Token> node)
		{
			_startIndex = startIndex;
			_token = token;
			_node = node;
		}

		public LinkedListNode<Token> Node
		{
			get { return _node; }
		}

		public Token Token
		{
			get { return _token; }
		}

		public int StartIndex
		{
			get { return _startIndex; }
		}
	}
}