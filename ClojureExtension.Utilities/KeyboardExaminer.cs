// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.Windows.Input;

namespace ClojureExtension.Utilities
{
	public class KeyboardExaminer
	{
		public bool IsShiftDown()
		{
			return Keyboard.IsKeyDown(Key.LeftShift) | Keyboard.IsKeyDown(Key.RightShift);
		}

		public bool IsControlDown()
		{
			return Keyboard.IsKeyDown(Key.LeftCtrl) | Keyboard.IsKeyDown(Key.RightCtrl);
		}

		public bool IsArrowKey(Key key)
		{
			return key == Key.Up || key == Key.Down || key == Key.Left || key == Key.Right;
		}
	}
}