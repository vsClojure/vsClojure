using System.Windows.Input;

namespace ClojureExtension.Repl
{
	public class KeyboardExaminer
	{
		public bool IsShiftDown()
		{
			return Keyboard.IsKeyDown(Key.LeftShift) | Keyboard.IsKeyDown(Key.RightShift);
		}

		public bool ControlIsDown()
		{
			return Keyboard.IsKeyDown(Key.LeftCtrl) | Keyboard.IsKeyDown(Key.RightCtrl);
		}

		public bool IsArrowKey(Key key)
		{
			return key == Key.Up || key == Key.Down || key == Key.Left || key == Key.Right;
		}
	}
}