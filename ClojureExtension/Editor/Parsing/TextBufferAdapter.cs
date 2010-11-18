using Microsoft.VisualStudio.Text;

namespace Microsoft.ClojureExtension.Editor.Parsing
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

		public int Length
		{
			get { return _buffer.CurrentSnapshot.Length; }
		}
	}
}