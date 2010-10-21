using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplTextPipe
    {
        private readonly ReplData _replData;
        private readonly ReplWriter _replWriter;
        private int _promptPosition;

        public ReplTextPipe(ReplData replData, ReplWriter replWriter)
        {
            _replData = replData;
            _replWriter = replWriter;
            _promptPosition = 0;
        }

        public void WriteFromTextBoxToRepl(string data)
        {
            if (data == "\r\n")
            {
                _replWriter.WriteExpressionToRepl(_replData, _replData.InteractiveTextBox.Text.Substring(_promptPosition));
                _promptPosition = _replData.InteractiveTextBox.Text.Length;
            }
        }

        public void WriteFromReplToTextBox(StreamReader stream)
        {
            while (!_replData.ReplProcess.HasExited)
            {
                string output = ((char) stream.Read()).ToString();
                while (stream.Peek() != -1) output += ((char) stream.Read()).ToString();

                _replData.InteractiveTextBox.Dispatcher.Invoke(
                    DispatcherPriority.Normal,
                    new DispatcherOperationCallback(
                        delegate
                        {
                            _replData.InteractiveTextBox.AppendText(output);
                            _replData.InteractiveTextBox.ScrollToEnd();
                            _promptPosition = _replData.InteractiveTextBox.Text.Length;
                            return null;
                        }), null);
            }
        }

        private LinkedList<Key> _downKeys = new LinkedList<Key>();

        public void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (_replData.InteractiveTextBox.CaretIndex < _promptPosition) e.Handled = true;
        }

        private bool isShiftDown()
        {
            return _downKeys.Contains(Key.LeftShift) || _downKeys.Contains(Key.RightShift);
        }

        public void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            _downKeys.AddLast(e.Key);

            if (e.Key == Key.Enter) WriteFromTextBoxToRepl("\r\n");

            if (_replData.InteractiveTextBox.CaretIndex > _promptPosition && e.Key == Key.Home && !isShiftDown())
            {
                _replData.InteractiveTextBox.CaretIndex = _promptPosition;
                e.Handled = true;
                return;
            }

            if (_replData.InteractiveTextBox.CaretIndex > _promptPosition && e.Key == Key.Home && isShiftDown())
            {
                _replData.InteractiveTextBox.Select(_promptPosition, _replData.InteractiveTextBox.CaretIndex - _promptPosition + _replData.InteractiveTextBox.SelectionLength);
                e.Handled = true;
                return;
            }
            
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right) return;
            if (e.Key == Key.Home || e.Key == Key.End || e.Key == Key.PageUp || e.Key == Key.PageDown) return;
            if (_replData.InteractiveTextBox.CaretIndex < _promptPosition) e.Handled = true;
            if (_replData.InteractiveTextBox.CaretIndex == _promptPosition && e.Key == Key.Back) e.Handled = true;
        }

        public void PreviewKeyUp(object sender, KeyEventArgs e)
        {
            _downKeys.Remove(e.Key);
        }
    }
}