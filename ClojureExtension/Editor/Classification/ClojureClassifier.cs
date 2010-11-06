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
		private ITextBuffer _buffer;
		private readonly ITagAggregator<ClojureTokenTag> _aggregator;
		private readonly IDictionary<int, IClassificationType> _clojureTypes;

		public ClojureClassifier(ITextBuffer buffer,
								   ITagAggregator<ClojureTokenTag> clojureTagAggregator,
								   IClassificationTypeRegistryService typeService)
		{
			_buffer = buffer;
			_aggregator = clojureTagAggregator;
			_clojureTypes = new Dictionary<int, IClassificationType>();
			_clojureTypes[ClojureLexer.SYMBOL] = typeService.GetClassificationType("ClojureSymbol");
			_clojureTypes[ClojureLexer.STRING] = typeService.GetClassificationType("ClojureString");
			_clojureTypes[ClojureLexer.NUMBER] = typeService.GetClassificationType("ClojureNumber");
			_clojureTypes[ClojureLexer.HEXDIGIT] = typeService.GetClassificationType("ClojureNumber");
			_clojureTypes[ClojureLexer.COMMENT] = typeService.GetClassificationType("ClojureComment");
			_clojureTypes[ClojureLexer.KEYWORD] = typeService.GetClassificationType("ClojureKeyword");
			_clojureTypes[ClojureLexer.CHARACTER] = typeService.GetClassificationType("ClojureCharacter");
			_clojureTypes[ClojureLexer.SPECIAL_FORM] = typeService.GetClassificationType("ClojureBuiltIn");
			_clojureTypes[ClojureLexer.BOOLEAN] = typeService.GetClassificationType("ClojureBoolean");
			_clojureTypes[ClojureLexer.OPEN_PAREN] = typeService.GetClassificationType("ClojureList");
			_clojureTypes[ClojureLexer.CLOSE_PAREN] = typeService.GetClassificationType("ClojureList");
			_clojureTypes[ClojureLexer.LEFT_SQUARE_BRACKET] = typeService.GetClassificationType("ClojureVector");
			_clojureTypes[ClojureLexer.RIGHT_SQUARE_BRACKET] = typeService.GetClassificationType("ClojureVector");
			_clojureTypes[ClojureLexer.LEFT_CURLY_BRACKET] = typeService.GetClassificationType("ClojureMap");
			_clojureTypes[ClojureLexer.RIGHT_CURLY_BRACKET] = typeService.GetClassificationType("ClojureMap");
			_clojureTypes[ClojureLexer.METADATA_TYPEHINT] = typeService.GetClassificationType("ClojureMetadataTypeHint");
		}

		public event EventHandler<SnapshotSpanEventArgs> TagsChanged
		{
			add { }
			remove { }
		}

		public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		{
			foreach (var tagSpan in _aggregator.GetTags(spans))
			{
				if (!_clojureTypes.ContainsKey(tagSpan.Tag.AntlrToken.Type)) continue;
				var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
				yield return new TagSpan<ClassificationTag>(tagSpans[0], new ClassificationTag(_clojureTypes[tagSpan.Tag.AntlrToken.Type]));
			}
		}
	}
}