using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor.Tagger
{
	internal sealed class ClojureTokenTagger : ITagger<ClojureTokenTag>
	{
		private readonly Tokenizer _tokenizer;
		private readonly TokenList _tokenList;
		private readonly ITextBuffer _buffer;
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;
		public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

		internal ClojureTokenTagger(Tokenizer tokenizer, TokenList tokenList, ITextBuffer buffer, Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_tokenizer = tokenizer;
			_tokenList = tokenList;
			_buffer = buffer;
			_tokenizedBuffer = tokenizedBuffer;
		}

		public void OnTokenChange(object sender, TokenChangedEventArgs e)
		{
			TagsChanged(sender, new SnapshotSpanEventArgs(new SnapshotSpan(_buffer.CurrentSnapshot, e.BufferMappedTokenData.StartIndex, e.BufferMappedTokenData.Token.Length)));
		}

		public IEnumerable<ITagSpan<ClojureTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		{
			if (_tokenizedBuffer.CurrentState.Count == 0)
				_tokenizedBuffer.CurrentState = _tokenizer.Tokenize(_buffer.CurrentSnapshot.GetText());

			LinkedList<TagSpan<ClojureTokenTag>> tagSpans = new LinkedList<TagSpan<ClojureTokenTag>>();

			foreach (SnapshotSpan curSpan in spans)
			{
				foreach (BufferMappedTokenData intersectingTokenData in _tokenList.Intersection(curSpan.Start.Position, curSpan.Length))
				{
					var storedTokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(intersectingTokenData.StartIndex, intersectingTokenData.Token.Length));
					tagSpans.AddLast(new TagSpan<ClojureTokenTag>(storedTokenSpan, new ClojureTokenTag(intersectingTokenData.Token)));
				}
			}

			return tagSpans;
		}
	}
}