using System.Collections.Generic;
using System.Text;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Editor.AutoFormat
{
	public class AutoFormatter
	{
		private readonly ITextBufferAdapter _textBuffer;
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;

		public AutoFormatter(ITextBufferAdapter textBuffer, Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_textBuffer = textBuffer;
			_tokenizedBuffer = tokenizedBuffer;
		}

		public void Format()
		{
			StringBuilder output = new StringBuilder();

			foreach (Token t in _tokenizedBuffer.CurrentState)
			{

			}

			_textBuffer.SetText(output.ToString());
		}
	}
}