using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor.Classification
{
    internal sealed class ClojureClassifier : ITagger<ClassificationTag>
    {
        private ITextBuffer _buffer;
        private readonly ITagAggregator<ClojureTokenTag> _aggregator;
        private readonly IDictionary<int, IClassificationType> _clojureTypes;

        internal ClojureClassifier(ITextBuffer buffer,
                                   ITagAggregator<ClojureTokenTag> clojureTagAggregator,
                                   IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = clojureTagAggregator;
            _clojureTypes = new Dictionary<int, IClassificationType>();
            _clojureTypes[ClojureLexer.SYMBOL] = typeService.GetClassificationType("ClojureSymbol");
            _clojureTypes[ClojureLexer.STRING] = typeService.GetClassificationType("String");
            _clojureTypes[ClojureLexer.NUMBER] = typeService.GetClassificationType("Number");
            _clojureTypes[ClojureLexer.COMMENT] = typeService.GetClassificationType("Comment");
            _clojureTypes[ClojureLexer.KEYWORD] = typeService.GetClassificationType("ClojureKeyword");
            _clojureTypes[ClojureLexer.CHARACTER] = typeService.GetClassificationType("Character");
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