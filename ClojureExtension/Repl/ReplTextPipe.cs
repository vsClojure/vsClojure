using System.IO;
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
    }
}