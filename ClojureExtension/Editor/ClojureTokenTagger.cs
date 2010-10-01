using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor
{
    internal sealed class ClojureTokenTagger : ITagger<ClojureTokenTag>
    {
        private ITextBuffer _buffer;
        private readonly IDictionary<string, ClojureTokenTypes> _clojureTypes;

        internal ClojureTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            _clojureTypes = new Dictionary<string, ClojureTokenTypes>();
            _clojureTypes["("] = ClojureTokenTypes.StartList;
            _clojureTypes[")"] = ClojureTokenTypes.EndList;
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
                int curLoc = containingLine.Start.Position;
                string lineText = containingLine.GetText();
                StringBuilder currentToken = new StringBuilder();
                int tokenStart = curLoc;

                for (int i = 0; i < lineText.Length; i++)
                {
                    if (lineText[i] == '(')
                    {
                        if (currentToken.Length > 0)
                        {
                            var storedTokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(tokenStart, currentToken.Length));
                            currentToken.Clear();
                            tokenSpans.Add(new TagSpan<ClojureTokenTag>(storedTokenSpan, new ClojureTokenTag(ClojureTokenTypes.Symbol)));
                        }

                        var tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, 1));
                        tokenSpans.Add(new TagSpan<ClojureTokenTag>(tokenSpan, new ClojureTokenTag(ClojureTokenTypes.StartList)));
                        tokenStart = curLoc + 1;
                    }
                    else if (lineText[i] == ')')
                    {
                        if (currentToken.Length > 0)
                        {
                            var storedTokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(tokenStart, currentToken.Length));
                            currentToken.Clear();
                            tokenSpans.Add(new TagSpan<ClojureTokenTag>(storedTokenSpan, new ClojureTokenTag(ClojureTokenTypes.Symbol)));
                        }

                        var tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, 1));
                        tokenSpans.Add(new TagSpan<ClojureTokenTag>(tokenSpan, new ClojureTokenTag(ClojureTokenTypes.EndList)));
                        tokenStart = curLoc + 1;
                    }
                    else if (lineText[i] == ' ')
                    {
                        var tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(tokenStart, currentToken.Length));
                        currentToken.Clear();
                        tokenSpans.Add(new TagSpan<ClojureTokenTag>(tokenSpan, new ClojureTokenTag(ClojureTokenTypes.Symbol)));
                        tokenStart = curLoc + 1;
                    }
                    else
                    {
                        currentToken.Append(lineText[i]);
                    }

                    curLoc++;
                }

                if (currentToken.Length > 0)
                {
                    var storedTokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(tokenStart, currentToken.Length));
                    currentToken.Clear();
                    tokenSpans.Add(new TagSpan<ClojureTokenTag>(storedTokenSpan, new ClojureTokenTag(ClojureTokenTypes.Symbol)));
                }
            }

            return tokenSpans;
        }
    }
}