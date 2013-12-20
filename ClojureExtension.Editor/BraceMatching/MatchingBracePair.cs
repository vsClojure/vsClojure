// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using ClojureExtension.Parsing;

namespace Microsoft.ClojureExtension.Editor.BraceMatching
{
	public class MatchingBracePair
	{
		private readonly IndexToken _start;
		private readonly IndexToken _end;

		public MatchingBracePair(IndexToken start, IndexToken end)
		{
			_start = start;
			_end = end;
		}

		public IndexToken End
		{
			get { return _end; }
		}

		public IndexToken Start
		{
			get { return _start; }
		}
	}
}