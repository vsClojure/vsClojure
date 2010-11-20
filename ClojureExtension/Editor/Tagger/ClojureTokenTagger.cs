using System;
using System.Collections.Generic;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor.Tagger
{
	internal sealed class ClojureTokenTagger : ITagger<ClojureTokenTag>
	{
		private readonly Tokenizer _tokenizer;
		private readonly ITextBuffer _buffer;
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;
		public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

		internal ClojureTokenTagger(Tokenizer tokenizer, ITextBuffer buffer, Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_tokenizer = tokenizer;
			_buffer = buffer;
			_tokenizedBuffer = tokenizedBuffer;
		}

		public void OnTokenChange(object sender, TokenChangedEventArgs e)
		{
			TagsChanged(sender, new SnapshotSpanEventArgs(new SnapshotSpan(_buffer.CurrentSnapshot, e.IndexToken.StartIndex, e.IndexToken.Token.Length)));
		}

		public IEnumerable<ITagSpan<ClojureTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		{
			if (_tokenizedBuffer.CurrentState.Count == 0)
				_tokenizedBuffer.CurrentState = _tokenizer.Tokenize(_buffer.CurrentSnapshot.GetText());

			LinkedList<TagSpan<ClojureTokenTag>> tagSpans = new LinkedList<TagSpan<ClojureTokenTag>>();

			foreach (SnapshotSpan curSpan in spans)
			{
				foreach (IndexToken intersectingTokenData in _tokenizedBuffer.CurrentState.Intersection(curSpan.Start.Position, curSpan.Length))
				{
					var storedTokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(intersectingTokenData.StartIndex, intersectingTokenData.Token.Length));
					tagSpans.AddLast(new TagSpan<ClojureTokenTag>(storedTokenSpan, new ClojureTokenTag(intersectingTokenData.Token)));
				}
			}

			return tagSpans;
		}
	}
}