// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.Diagnostics;

namespace ClojureExtension.Repl
{
	public class ReplWriter
	{
		private readonly Process _process;
		private readonly TextBoxWriter _textBoxWriter;

		public ReplWriter(Process process, TextBoxWriter textBoxWriter)
		{
			_process = process;
			_textBoxWriter = textBoxWriter;
		}

		public void WriteBehindTheSceneExpressionToRepl(string expression)
		{
			WriteExpressionToRepl(expression);
			_textBoxWriter.WriteToTextBox("\r\n");
		}

		public void WriteExpressionToRepl(string expression)
		{
			_process.StandardInput.WriteLine(expression);
		}
	}
}