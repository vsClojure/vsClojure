using System;
using ClojureExtension.Parsing;

namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public class TokenChangedEventArgs : EventArgs
	{
		private readonly IndexToken _indexToken;

		public TokenChangedEventArgs(IndexToken indexToken)
		{
			_indexToken = indexToken;
		}

		public IndexToken IndexToken
		{
			get { return _indexToken; }
		}
	}
}