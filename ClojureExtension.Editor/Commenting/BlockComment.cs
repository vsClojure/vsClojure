using System.Collections.Generic;
using ClojureExtension.Editor.TextBuffer;

namespace ClojureExtension.Editor.Commenting
{
	public class BlockComment
	{
		private readonly ITextBufferAdapter _textBuffer;

		public BlockComment(ITextBufferAdapter textBuffer)
		{
			_textBuffer = textBuffer;
		}

		public void Execute()
		{
			List<string> lines = _textBuffer.GetSelectedLines();
			List<string> commentedLines = new List<string>();
			foreach (string line in lines) commentedLines.Add(";" + line);
			_textBuffer.ReplaceSelectedLines(commentedLines);
		}
	}
}