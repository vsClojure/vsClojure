using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplTextPipe
    {
    	private readonly TextBox _interactiveTextBox;
    	private readonly Process _process;
    	private readonly ReplWriter _replWriter;
        private int _promptPosition;
		private readonly LinkedList<Key> _downKeys;
    	private readonly LinkedList<string> _history;
    	private LinkedListNode<string> _currentlySelectedHistoryItem;

        public ReplTextPipe(TextBox interactiveTextBox, Process process, ReplWriter replWriter)
        {
        	_process = process;
        	_replWriter = replWriter;
            _promptPosition = 0;
			_interactiveTextBox = interactiveTextBox;
			_downKeys = new LinkedList<Key>();
			_history = new LinkedList<string>();
        }

        public void WriteFromReplToTextBox(StreamReader stream)
        {
			while (!_process.HasExited)
            {
                string output = ((char) stream.Read()).ToString();
                while (stream.Peek() != -1) output += ((char) stream.Read()).ToString();

				_interactiveTextBox.Dispatcher.Invoke(
                    DispatcherPriority.Normal,
                    new DispatcherOperationCallback(
                        delegate
                        {
							_interactiveTextBox.AppendText(output);
							_interactiveTextBox.ScrollToEnd();
							_promptPosition = _interactiveTextBox.Text.Length;
                            return null;
                        }), null);
            }
        }

        public void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
			if (_interactiveTextBox.CaretIndex < _promptPosition) e.Handled = true;
        }

        private bool IsShiftDown()
        {
            return _downKeys.Contains(Key.LeftShift) || _downKeys.Contains(Key.RightShift);
        }

		private bool ControlIsDown()
		{
			return _downKeys.Contains(Key.LeftCtrl) || _downKeys.Contains(Key.RightCtrl);
		}

        public void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            _downKeys.AddLast(e.Key);

			if (e.Key == Key.Enter && !IsShiftDown() && _interactiveTextBox.CaretIndex >= _promptPosition)
            {
            	string userInput = _interactiveTextBox.Text.Substring(_promptPosition);
				
				if (_currentlySelectedHistoryItem != null && _currentlySelectedHistoryItem.Value == userInput)
				{
					_history.Remove(_currentlySelectedHistoryItem.Value);
					_history.AddFirst(_currentlySelectedHistoryItem.Value);
				}
				else
				{
					_history.AddFirst(userInput);
				}

				_currentlySelectedHistoryItem = null;
				_replWriter.WriteExpressionToRepl(userInput);
				_promptPosition = _interactiveTextBox.Text.Length;
            	_interactiveTextBox.CaretIndex = _interactiveTextBox.Text.Length;
            	return;
            }

			if (_interactiveTextBox.CaretIndex > _promptPosition && e.Key == Key.Home && !IsShiftDown())
            {
				_interactiveTextBox.CaretIndex = _promptPosition;
                e.Handled = true;
                return;
            }

			if (_interactiveTextBox.CaretIndex > _promptPosition && e.Key == Key.Home && IsShiftDown())
            {
				_interactiveTextBox.Select(_promptPosition, _interactiveTextBox.CaretIndex - _promptPosition + _interactiveTextBox.SelectionLength);
                e.Handled = true;
                return;
            }

			if (_interactiveTextBox.CaretIndex >= _promptPosition && e.Key == Key.Down && ControlIsDown())
			{
				if (_currentlySelectedHistoryItem != null)
				{
					_interactiveTextBox.Text = _interactiveTextBox.Text.Remove(_promptPosition, _interactiveTextBox.Text.Length - _promptPosition);

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

				e.Handled = true;
				return;
			}

			if (_interactiveTextBox.CaretIndex >= _promptPosition && e.Key == Key.Up && ControlIsDown())
			{
				if (_currentlySelectedHistoryItem == null) _currentlySelectedHistoryItem = _history.First;
				else if (_currentlySelectedHistoryItem.Next != null) _currentlySelectedHistoryItem = _currentlySelectedHistoryItem.Next;

				if (_currentlySelectedHistoryItem != null)
				{
					_interactiveTextBox.Text = _interactiveTextBox.Text.Remove(_promptPosition, _interactiveTextBox.Text.Length - _promptPosition);
					_interactiveTextBox.AppendText(_currentlySelectedHistoryItem.Value);
					_interactiveTextBox.CaretIndex = _interactiveTextBox.Text.Length;
				}
				
				e.Handled = true;
				return;
			}

            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right) return;
            if (e.Key == Key.Home || e.Key == Key.End || e.Key == Key.PageUp || e.Key == Key.PageDown) return;
			if (_interactiveTextBox.CaretIndex < _promptPosition) e.Handled = true;

			if (_interactiveTextBox.CaretIndex == _promptPosition && e.Key == Key.Back)
			{
				_interactiveTextBox.Text = _interactiveTextBox.Text.Remove(_interactiveTextBox.SelectionStart, _interactiveTextBox.SelectionLength);
				_interactiveTextBox.CaretIndex = _promptPosition;
				e.Handled = true;
			}
        }

        public void PreviewKeyUp(object sender, KeyEventArgs e)
        {
            _downKeys.Remove(e.Key);
        }
    }
}