using System;
using ClojureExtension.Parsing;

namespace ClojureExtension.Editor.InputHandling
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