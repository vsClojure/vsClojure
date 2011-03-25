using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.ClojureExtension.Utilities;

namespace ClojureExtension.Repl
{
	public class History
	{
		private readonly MetaKeyWatcher _metaKeyWatcher;
		private readonly Entity<ReplState> _replEntity;
		private readonly TextBox _interactiveTextBox;
		private LinkedListNode<string> _currentlySelectedHistoryItem;

		public History(MetaKeyWatcher metaKeyWatcher, Entity<ReplState> replEntity, TextBox interactiveTextBox)
		{
			_metaKeyWatcher = metaKeyWatcher;
			_replEntity = replEntity;
			_interactiveTextBox = interactiveTextBox;
		}

		public void PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter && !_metaKeyWatcher.IsShiftDown() && _interactiveTextBox.CaretIndex >= _replEntity.CurrentState.PromptPosition)
			{
				SubmitInputToHistory();
			}

			if (_interactiveTextBox.CaretIndex >= _replEntity.CurrentState.PromptPosition && e.Key == Key.Down && _metaKeyWatcher.ControlIsDown())
			{
				ShowPreviousHistoryItem();
				e.Handled = true;
				return;
			}

			if (_interactiveTextBox.CaretIndex >= _replEntity.CurrentState.PromptPosition && e.Key == Key.Up && _metaKeyWatcher.ControlIsDown())
			{
				ShowNextItemInHistory();
				e.Handled = true;
				return;
			}
		}

		private void ShowNextItemInHistory()
		{
			if (_currentlySelectedHistoryItem == null) _currentlySelectedHistoryItem = _replEntity.CurrentState.History.First;
			else if (_currentlySelectedHistoryItem.Next != null) _currentlySelectedHistoryItem = _currentlySelectedHistoryItem.Next;

			if (_currentlySelectedHistoryItem != null)
			{
				_interactiveTextBox.Text = _interactiveTextBox.Text.Remove(_replEntity.CurrentState.PromptPosition, _interactiveTextBox.Text.Length - _replEntity.CurrentState.PromptPosition);
				_interactiveTextBox.AppendText(_currentlySelectedHistoryItem.Value);
				_interactiveTextBox.CaretIndex = _interactiveTextBox.Text.Length;
			}
		}

		private void ShowPreviousHistoryItem()
		{
			if (_currentlySelectedHistoryItem != null)
			{
				_interactiveTextBox.Text = _interactiveTextBox.Text.Remove(_replEntity.CurrentState.PromptPosition, _interactiveTextBox.Text.Length - _replEntity.CurrentState.PromptPosition);

				if (_currentlySelectedHistoryItem.Previous == null)
				{
					_currentlySelectedHistoryItem = null;
				}
				else
				{
					_currentlySelectedHistoryItem = _currentlySelectedHistoryItem.Previous;
					_interactiveTextBox.AppendText(_currentlySelectedHistoryItem.Value);
				}

				_interactiveTextBox.CaretIndex = _interactiveTextBox.Text.Length;
			}
		}

		private void SubmitInputToHistory()
		{
			string userInput = _interactiveTextBox.Text.Substring(_replEntity.CurrentState.PromptPosition);
			LinkedList<string> history = _replEntity.CurrentState.History;

			if (_currentlySelectedHistoryItem != null && _currentlySelectedHistoryItem.Value == userInput)
			{
				history.Remove(_currentlySelectedHistoryItem.Value);
				history.AddFirst(_currentlySelectedHistoryItem.Value);
			}
			else if (!string.IsNullOrEmpty(userInput.Trim()))
			{
				history.AddFirst(userInput);
			}

			_replEntity.CurrentState = _replEntity.CurrentState.ChangeHistory(history);
			_currentlySelectedHistoryItem = null;
		}
	}
}
