using System;
using System.Collections.Generic;
using ClojureExtension.Editor.InputHandling;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor.Tagger
{
	public class ClojureTokenTagger : ITagger<ClojureTokenTag>
	{
		private readonly ITextBuffer _buffer;
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;
		public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

		public ClojureTokenTagger(ITextBuffer buffer, Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_buffer = buffer;
			_tokenizedBuffer = tokenizedBuffer;
		}

		public void OnTokenChange(object sender, TokenChangedEventArgs e)
		{
			TagsChanged(sender, new SnapshotSpanEventArgs(new SnapshotSpan(_buffer.CurrentSnapshot, e.IndexToken.StartIndex, e.IndexToken.Token.Length)));
		}

		public IEnumerable<ITagSpan<ClojureTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		{
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