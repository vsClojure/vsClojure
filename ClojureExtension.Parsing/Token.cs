namespace ClojureExtension.Parsing
{
	public class Token
	{
		private readonly TokenType _type;
		private readonly string _text;
		private readonly int _startIndex;
		private readonly int _length;

		public Token(TokenType type, string text, int startIndex, int length)
		{
			_type = type;
			_text = text;
			_startIndex = startIndex;
			_length = length;
		}

		public int Length
		{
			get { return _length; }
		}

		public int StartIndex
		{
			get { return _startIndex; }
		}

		public string Text
		{
			get { return _text; }
		}

		public TokenType Type
		{
			get { return _type; }
		}

		public bool Equals(Token other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._type, _type) && Equals(other._text, _text) && other._startIndex == _startIndex && other._length == _length;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Token)) return false;
			return Equals((Token) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = _type.GetHashCode();
				result = (result*397) ^ (_text != null ? _text.GetHashCode() : 0);
				result = (result*397) ^ _startIndex;
				result = (result*397) ^ _length;
				return result;
			}
		}

		public override string ToString()
		{
			return string.Format("Type: {0}, Text: {1}, StartIndex: {2}, Length: {3}", _type, _text, _startIndex, _length);
		}
	}
}