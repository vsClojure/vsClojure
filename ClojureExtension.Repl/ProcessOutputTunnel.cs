using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Repl
{
	public class ProcessOutputTunnel
	{
		private readonly Process _process;
		private readonly TextBox _interactiveTextBox;
		private readonly Entity<ReplState> _replEntity;

		public ProcessOutputTunnel(Process process, TextBox interactiveTextBox, Entity<ReplState> replEntity)
		{
			_process = process;
			_interactiveTextBox = interactiveTextBox;
			_replEntity = replEntity;
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
							_replEntity.CurrentState = _replEntity.CurrentState.ChangePromptPosition(_interactiveTextBox.Text.Length);
							return null;
						}), null);
			}
		}
	}
}