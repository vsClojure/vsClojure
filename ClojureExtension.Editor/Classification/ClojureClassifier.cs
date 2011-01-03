using System;
using System.Collections.Generic;
using ClojureExtension.Parsing;
using Microsoft.ClojureExtension.Editor.Tagger;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor.Classification
{
	public class ClojureClassifier : ITagger<ClassificationTag>
	{
		private readonly ITextBuffer _buffer;
		private readonly ITagAggregator<ClojureTokenTag> _aggregator;
		private readonly IDictionary<TokenType, IClassificationType> _clojureTypes;
		public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

		public ClojureClassifier(ITextBuffer buffer,
								   ITagAggregator<ClojureTokenTag> clojureTagAggregator,
								   IClassificationTypeRegistryService typeService)
		{
			_buffer = buffer;
			_aggregator = clojureTagAggregator;
			_aggregator.TagsChanged += TokenTagsChanged;
			_clojureTypes = new Dictionary<TokenType, IClassificationType>();
			_clojureTypes[TokenType.Symbol] = typeService.GetClassificationType("ClojureSymbol");
			_clojureTypes[TokenType.String] = typeService.GetClassificationType("ClojureString");
			_clojureTypes[TokenType.Number] = typeService.GetClassificationType("ClojureNumber");
			_clojureTypes[TokenType.HexNumber] = typeService.GetClassificationType("ClojureNumber");
			_clojureTypes[TokenType.Comment] = typeService.GetClassificationType("ClojureComment");
			_clojureTypes[TokenType.Keyword] = typeService.GetClassificationType("ClojureKeyword");
			_clojureTypes[TokenType.Character] = typeService.GetClassificationType("ClojureCharacter");
			_clojureTypes[TokenType.BuiltIn] = typeService.GetClassificationType("ClojureBuiltIn");
			_clojureTypes[TokenType.Boolean] = typeService.GetClassificationType("ClojureBoolean");
			_clojureTypes[TokenType.ListStart] = typeService.GetClassificationType("ClojureList");
			_clojureTypes[TokenType.ListEnd] = typeService.GetClassificationType("ClojureList");
			_clojureTypes[TokenType.VectorStart] = typeService.GetClassificationType("ClojureVector");
			_clojureTypes[TokenType.VectorEnd] = typeService.GetClassificationType("ClojureVector");
			_clojureTypes[TokenType.MapStart] = typeService.GetClassificationType("ClojureMap");
			_clojureTypes[TokenType.MapEnd] = typeService.GetClassificationType("ClojureMap");
		}

		public void TokenTagsChanged(object sender, TagsChangedEventArgs e)
		{
			foreach (var span in e.Span.GetSpans(_buffer)) TagsChanged(this, new SnapshotSpanEventArgs(span));
		}

		public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		{
			foreach (var tagSpan in _aggregator.GetTags(spans))
			{
				if (!_clojureTypes.ContainsKey(tagSpan.Tag.Token.Type)) continue;
				var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
				yield return new TagSpan<ClassificationTag>(tagSpans[0], new ClassificationTag(_clojureTypes[tagSpan.Tag.Token.Type]));
			}
		}
	}
}