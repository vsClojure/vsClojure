using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.ClojureExtension.Utilities;

namespace ClojureExtension.Repl
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

		public void WriteFromReplToTextBox()
		{
			var outputReader = new AsynchronousStream(_process.StandardOutput.BaseStream);
			var errorReader = new AsynchronousStream(_process.StandardError.BaseStream);
			outputReader.Start();
			errorReader.Start();

			while (!_process.HasExited)
			{
				Thread.Sleep(2);

				if (outputReader.HasData && errorReader.HasData) WriteToTextBox(errorReader.GetData());
				if (outputReader.HasData) WriteToTextBox(outputReader.GetData());
				if (errorReader.HasData) WriteToTextBox(errorReader.GetData());
			}

			outputReader.Stop();
			errorReader.Stop();
		}

		public void WriteToTextBox(string output)
		{
			_interactiveTextBox.Dispatcher.Invoke(
			                                      DispatcherPriority.Normal,
			                                      new DispatcherOperationCallback(
			                                      	delegate
			                                      	{
			                                      		_interactiveTextBox.AppendText(output);
			                                      		_interactiveTextBox.ScrollToEnd();
			                                      		_replEntity.CurrentState =
			                                      			_replEntity.CurrentState.ChangePromptPosition(
			                                      			                                              _interactiveTextBox.Text.
			                                      			                                              	Length);
			                                      		return null;
			                                      	}), null);
		}
	}
}