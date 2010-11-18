using System;

namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public class TextChangeData
	{
		private readonly int _position;
		private readonly int _delta;

		public TextChangeData(int position, int delta)
		{
			_position = position;
			_delta = delta;
		}

		public int Length
		{
			get { return Math.Abs(Delta); }
		}

		public int Position
		{
			get { return _position; }
		}

		public bool IsDelete
		{
			get { return Delta < 0; }
		}

		public bool IsInsert
		{
			get { return Delta > 0; }
		}

		public int Delta
		{
			get { return _delta; }
		}
	}
}