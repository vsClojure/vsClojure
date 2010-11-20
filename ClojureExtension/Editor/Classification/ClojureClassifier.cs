using System;
using System.Collections.Generic;
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
		private readonly IDictionary<Parsing.TokenType, IClassificationType> _clojureTypes;
		public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

		public ClojureClassifier(ITextBuffer buffer,
								   ITagAggregator<ClojureTokenTag> clojureTagAggregator,
								   IClassificationTypeRegistryService typeService)
		{
			_buffer = buffer;
			_aggregator = clojureTagAggregator;
			_aggregator.TagsChanged += TokenTagsChanged;
			_clojureTypes = new Dictionary<Parsing.TokenType, IClassificationType>();
			_clojureTypes[Parsing.TokenType.Symbol] = typeService.GetClassificationType("ClojureSymbol");
			_clojureTypes[Parsing.TokenType.String] = typeService.GetClassificationType("ClojureString");
			_clojureTypes[Parsing.TokenType.Number] = typeService.GetClassificationType("ClojureNumber");
			_clojureTypes[Parsing.TokenType.HexNumber] = typeService.GetClassificationType("ClojureNumber");
			_clojureTypes[Parsing.TokenType.Comment] = typeService.GetClassificationType("ClojureComment");
			_clojureTypes[Parsing.TokenType.Keyword] = typeService.GetClassificationType("ClojureKeyword");
			_clojureTypes[Parsing.TokenType.Character] = typeService.GetClassificationType("ClojureCharacter");
			_clojureTypes[Parsing.TokenType.BuiltIn] = typeService.GetClassificationType("ClojureBuiltIn");
			_clojureTypes[Parsing.TokenType.Boolean] = typeService.GetClassificationType("ClojureBoolean");
			_clojureTypes[Parsing.TokenType.ListStart] = typeService.GetClassificationType("ClojureList");
			_clojureTypes[Parsing.TokenType.ListEnd] = typeService.GetClassificationType("ClojureList");
			_clojureTypes[Parsing.TokenType.VectorStart] = typeService.GetClassificationType("ClojureVector");
			_clojureTypes[Parsing.TokenType.VectorEnd] = typeService.GetClassificationType("ClojureVector");
			_clojureTypes[Parsing.TokenType.MapStart] = typeService.GetClassificationType("ClojureMap");
			_clojureTypes[Parsing.TokenType.MapEnd] = typeService.GetClassificationType("ClojureMap");
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