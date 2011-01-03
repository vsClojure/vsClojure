namespace ClojureExtension.Parsing
{
	public class IndexToken
	{
		private readonly int _startIndex;
		private readonly Token _token;

		public IndexToken(int startIndex, Token token)
		{
			_startIndex = startIndex;
			_token = token;
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