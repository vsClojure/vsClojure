using System.Collections.Generic;
using System.IO;

namespace ClojureExtension.Parsing
{
	public class PushBackCharacterStream
	{
		private readonly StringReader _source;
		private readonly Stack<char> _pushedBackChars;
		private int _currentIndex;

		public PushBackCharacterStream(StringReader source)
		{
			_source = source;
			_pushedBackChars = new Stack<char>();
		}

		public char Next()
		{
			_currentIndex = CurrentIndex + 1;

			if (_pushedBackChars.Count > 0)
			{
				return _pushedBackChars.Pop();
			}
			else
			{
				return (char) _source.Read();
			}
		}

		public void Push(char c)
		{
			_currentIndex = CurrentIndex - 1;
			_pushedBackChars.Push(c);
		}

		public void Push(string s)
		{
			for (int i=s.Length-1; i>=0; i--) Push(s[i]);
		}

		public bool HasMore
		{
			get { return _pushedBackChars.Count > 0 || _source.Peek() != -1; }
		}

		public int CurrentIndex
		{
			get { return _currentIndex; }
		}
	}
}
