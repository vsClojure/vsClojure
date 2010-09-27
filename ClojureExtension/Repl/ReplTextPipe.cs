using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplTextPipe
    {
        private readonly TextBox _textBox;
        private readonly Process _process;
        private int _promptPosition;

        public ReplTextPipe(TextBox textBox, Process process)
        {
            _textBox = textBox;
            _process = process;
            _promptPosition = 0;
        }

        public void WriteFromTextBoxToRepl(string data)
        {
            if (data == "\r\n")
            {
                _process.StandardInput.WriteLine(_textBox.Text.Substring(_promptPosition));
            }
        }

        public void SendDirectlyToRepl(string data)
        {
            _process.StandardInput.WriteLine(data);
        }

        public void ReadOutput(StreamReader stream)
        {
            while (!_process.HasExited)
            {
                string output = ((char) stream.Read()).ToString();
                while (stream.Peek() != -1) output += ((char) stream.Read()).ToString();

                _textBox.Dispatcher.Invoke(
                    DispatcherPriority.Normal,
                    new DispatcherOperationCallback(
                        delegate
                        {
                            _textBox.AppendText(output);
                            _textBox.ScrollToEnd();
                            _promptPosition = _textBox.Text.Length;
                            return null;
                        }), null);
            }
        }

        public void StartMarshallingText()
        {
            Thread outputThread = new Thread(() => ReadOutput(_process.StandardOutput));
            Thread errorThread = new Thread(() => ReadOutput(_process.StandardError));
            outputThread.Start();
            errorThread.Start();
        }

        public void SendToTextBox(string s)
        {
            _textBox.AppendText("\r\n");
        }
    }
}