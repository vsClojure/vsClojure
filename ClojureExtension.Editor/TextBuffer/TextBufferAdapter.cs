using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace ClojureExtension.Editor.TextBuffer
{
	public class TextBufferAdapter : ITextBufferAdapter
	{
		private readonly ITextView _textView;

		public TextBufferAdapter(ITextView textView)
		{
			_textView = textView;
		}

		public string GetText(int startPosition)
		{
			return _textView.TextBuffer.CurrentSnapshot.GetText().Substring(startPosition);
		}

		public void SetText(string text)
		{
			_textView.TextBuffer.Replace(new Span(0, _textView.TextBuffer.CurrentSnapshot.Length), text);
		}

		public int Length
		{
			get { return _textView.TextBuffer.CurrentSnapshot.Length; }
		}

		public List<string> GetSelectedLines()
		{
			int startPosition = _textView.Selection.Start.Position.GetContainingLine().Start.Position;
			int endPosition = _textView.Selection.End.Position.GetContainingLine().End.Position;
			string rawLines = _textView.TextBuffer.CurrentSnapshot.GetText(startPosition, endPosition - startPosition);
			return new List<string>(rawLines.Split(new[] {"\r\n"}, StringSplitOptions.None));
		}

		public void ReplaceSelectedLines(List<string> newLines)
		{
			int startPosition = _textView.Selection.Start.Position.GetContainingLine().Start.Position;
			int endPosition = _textView.Selection.End.Position.GetContainingLine().End.Position;
			bool originalSelectedIsReversed = _textView.Selection.IsReversed;
			string replacementText = newLines.Aggregate((first, second) => first + "\r\n" + second);
			_textView.TextBuffer.Replace(new Span(startPosition, endPosition - startPosition), replacementText);
			SnapshotSpan replacementSpan = new SnapshotSpan(new SnapshotPoint(_textView.TextBuffer.CurrentSnapshot, startPosition), replacementText.Length);
			_textView.Selection.Select(replacementSpan, originalSelectedIsReversed);
		}
	}
}