using System.IO;
using System.Threading;
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
            }
        }

        public void ReadOutput(StreamReader stream)
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

        public void StartMarshallingText()
        {
            Thread outputThread = new Thread(() => ReadOutput(_replData.ReplProcess.StandardOutput));
            Thread errorThread = new Thread(() => ReadOutput(_replData.ReplProcess.StandardError));
            outputThread.Start();
            errorThread.Start();
        }
    }
}