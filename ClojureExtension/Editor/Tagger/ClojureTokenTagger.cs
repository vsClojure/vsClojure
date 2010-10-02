using System;
using System.Collections.Generic;
using Antlr.Runtime;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor.Tagger
{
    internal sealed class ClojureTokenTagger : ITagger<ClojureTokenTag>
    {
        private ITextBuffer _buffer;

        internal ClojureTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<ClojureTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            List<ITagSpan<ClojureTokenTag>> tokenSpans = new List<ITagSpan<ClojureTokenTag>>();

            foreach (SnapshotSpan curSpan in spans)
            {
                ITextSnapshotLine containingLine = curSpan.Start.GetContainingLine();
                ClojureLexer lexer = new ClojureLexer(new ANTLRStringStream(containingLine.GetText()));
                IToken token = lexer.NextToken();
                int currentIndex = containingLine.Start;

                while (token.Type != -1)
                {
                    var storedTokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(currentIndex, token.Text.Length));
                    tokenSpans.Add(new TagSpan<ClojureTokenTag>(storedTokenSpan, new ClojureTokenTag(token)));
                    currentIndex += token.Text.Length;
                    token = lexer.NextToken();
                }
            }

            return tokenSpans;
        }
    }
}