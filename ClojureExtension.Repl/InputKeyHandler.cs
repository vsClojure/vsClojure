using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.ClojureExtension.Utilities;

namespace ClojureExtension.Repl
{
	public class InputKeyHandler
	{
		private readonly TextBox _interactiveTextBox;
		private readonly MetaKeyWatcher _metaKeyWatcher;
		private readonly Entity<ReplState> _replEntity;
		private readonly ReplWriter _replWriter;

		public InputKeyHandler(MetaKeyWatcher metaKeyWatcher, Entity<ReplState> replEntity, TextBox interactiveTextBox, ReplWriter replWriter)
		{
			_metaKeyWatcher = metaKeyWatcher;
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
			if (e.Key == Key.Enter && !_metaKeyWatcher.IsShiftDown() && _interactiveTextBox.CaretIndex >= _replEntity.CurrentState.PromptPosition)
			{
				string userInput = _interactiveTextBox.Text.Substring(_replEntity.CurrentState.PromptPosition);
				_replWriter.WriteExpressionToRepl(userInput);
				_replEntity.CurrentState = _replEntity.CurrentState.ChangePromptPosition(_interactiveTextBox.Text.Length);
				_interactiveTextBox.CaretIndex = _interactiveTextBox.Text.Length;
				return;
			}

			if (_interactiveTextBox.CaretIndex > _replEntity.CurrentState.PromptPosition && e.Key == Key.Home && !_metaKeyWatcher.IsShiftDown())
			{
				_interactiveTextBox.CaretIndex = _replEntity.CurrentState.PromptPosition;
				e.Handled = true;
				return;
			}

			if (_interactiveTextBox.CaretIndex > _replEntity.CurrentState.PromptPosition && e.Key == Key.Home && _metaKeyWatcher.IsShiftDown())
			{
				_interactiveTextBox.Select(_replEntity.CurrentState.PromptPosition, _interactiveTextBox.CaretIndex - _replEntity.CurrentState.PromptPosition + _interactiveTextBox.SelectionLength);
				e.Handled = true;
				return;
			}

			if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right) return;
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