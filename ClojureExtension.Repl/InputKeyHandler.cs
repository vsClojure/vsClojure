using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.ClojureExtension.Utilities;

namespace ClojureExtension.Repl
{
	public class InputKeyHandler
	{
		private readonly TextBox _interactiveTextBox;
		private readonly KeyboardExaminer _keyboardExaminer;
		private readonly Entity<ReplState> _replEntity;
		private readonly ReplWriter _replWriter;

		public InputKeyHandler(KeyboardExaminer keyboardExaminer, Entity<ReplState> replEntity, TextBox interactiveTextBox, ReplWriter replWriter)
		{
			_keyboardExaminer = keyboardExaminer;
			_replEntity = replEntity;
			_replWriter = replWriter;
			_interactiveTextBox = interactiveTextBox;
		}

		public void PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (_interactiveTextBox.CaretIndex < _replEntity.CurrentState.PromptPosition) e.Handled = true;
		}

		public void PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter && !_keyboardExaminer.IsShiftDown() && _interactiveTextBox.CaretIndex >= _replEntity.CurrentState.PromptPosition)
			{
				string userInput = _interactiveTextBox.Text.Substring(_replEntity.CurrentState.PromptPosition);
				_replWriter.WriteExpressionToRepl(userInput);
				return;
			}

			if (_interactiveTextBox.CaretIndex > _replEntity.CurrentState.PromptPosition && e.Key == Key.Home && !_keyboardExaminer.IsShiftDown())
			{
				_interactiveTextBox.CaretIndex = _replEntity.CurrentState.PromptPosition;
				e.Handled = true;
				return;
			}

			if (_interactiveTextBox.CaretIndex > _replEntity.CurrentState.PromptPosition && e.Key == Key.Home && _keyboardExaminer.IsShiftDown())
			{
				_interactiveTextBox.Select(_replEntity.CurrentState.PromptPosition, _interactiveTextBox.CaretIndex - _replEntity.CurrentState.PromptPosition + _interactiveTextBox.SelectionLength);
				e.Handled = true;
				return;
			}

			if (_keyboardExaminer.IsArrowKey(e.Key)) return;
			if (e.Key == Key.Home || e.Key == Key.End || e.Key == Key.PageUp || e.Key == Key.PageDown) return;
			if (_interactiveTextBox.CaretIndex < _replEntity.CurrentState.PromptPosition) e.Handled = true;

			if (_interactiveTextBox.CaretIndex == _replEntity.CurrentState.PromptPosition && e.Key == Key.Back)
			{
				_interactiveTextBox.Text = _interactiveTextBox.Text.Remove(_interactiveTextBox.SelectionStart, _interactiveTextBox.SelectionLength);
				_interactiveTextBox.CaretIndex = _replEntity.CurrentState.PromptPosition;
				e.Handled = true;
			}
		}
	}
}