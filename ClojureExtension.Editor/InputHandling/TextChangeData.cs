// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;

namespace ClojureExtension.Editor.InputHandling
{
	public class TextChangeData
	{
		private readonly int _position;
		private readonly int _delta;
		private readonly int _lengthOfChangedText;

		public TextChangeData(int position, int delta) : this(position, delta, Math.Abs(delta))
		{
			
		}

		public TextChangeData(int position, int delta, int lengthOfChangedText)
		{
			_position = position;
			_delta = delta;
			_lengthOfChangedText = lengthOfChangedText;
		}

		public int LengthOfChangedText
		{
			get { return _lengthOfChangedText; }
		}

		public int Position
		{
			get { return _position; }
		}

		public int Delta
		{
			get { return _delta; }
		}
	}
}