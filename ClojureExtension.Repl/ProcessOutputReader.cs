using System.Threading;
using ClojureExtension.Utilities;

namespace ClojureExtension.Repl
{
	public class ProcessOutputReader
	{
		private readonly TextBoxWriter _textBoxWriter;
		private readonly StreamBuffer _outputStreamBuffer;
		private readonly StreamBuffer _errorStreamBuffer;

		public ProcessOutputReader(TextBoxWriter textBoxWriter, StreamBuffer outputStreamBuffer, StreamBuffer errorStreamBuffer)
		{
			_textBoxWriter = textBoxWriter;
			_outputStreamBuffer = outputStreamBuffer;
			_errorStreamBuffer = errorStreamBuffer;
		}

		public void StartMarshallingTextFromReplToTextBox()
		{
			while (true)
			{
				Thread.Sleep(2);

				if (_outputStreamBuffer.HasData && _errorStreamBuffer.HasData) _textBoxWriter.WriteToTextBox(_errorStreamBuffer.GetData());
				if (_outputStreamBuffer.HasData) _textBoxWriter.WriteToTextBox(_outputStreamBuffer.GetData());
				if (_errorStreamBuffer.HasData) _textBoxWriter.WriteToTextBox(_errorStreamBuffer.GetData());
			}
		}
	}
}