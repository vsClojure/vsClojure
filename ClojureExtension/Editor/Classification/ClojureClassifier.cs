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
        private readonly IDictionary<ClojureTokenTypes, IClassificationType> _clojureTypes;

        internal ClojureClassifier(ITextBuffer buffer,
                                   ITagAggregator<ClojureTokenTag> ookTagAggregator,
                                   IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = ookTagAggregator;
            _clojureTypes = new Dictionary<ClojureTokenTypes, IClassificationType>();
            _clojureTypes[ClojureTokenTypes.StartList] = typeService.GetClassificationType("StartList");
            _clojureTypes[ClojureTokenTypes.EndList] = typeService.GetClassificationType("EndList");
            _clojureTypes[ClojureTokenTypes.Symbol] = typeService.GetClassificationType("Symbol");
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
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return new TagSpan<ClassificationTag>(tagSpans[0], new ClassificationTag(_clojureTypes[tagSpan.Tag.Type]));
            }
        }
    }
}