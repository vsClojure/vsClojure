using Microsoft.VisualStudio.Text;

namespace ClojureExtension.Editor.InputHandling
{
	public class TextBufferAdapter : ITextBufferAdapter
	{
		private readonly ITextBuffer _buffer;

		public TextBufferAdapter(ITextBuffer buffer)
		{
			_buffer = buffer;
		}

		public string GetText(int startPosition)
		{
			return _buffer.CurrentSnapshot.GetText().Substring(startPosition);
		}

		public void SetText(string text)
		{
			_buffer.Replace(new Span(0, _buffer.CurrentSnapshot.Length), text);
		}

		public int Length
		{
			get { return _buffer.CurrentSnapshot.Length; }
		}
	}
}