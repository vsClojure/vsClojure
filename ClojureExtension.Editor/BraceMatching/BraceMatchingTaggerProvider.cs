using System.Collections.Generic;
using System.ComponentModel.Composition;
using ClojureExtension.Editor.TextBuffer;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.BraceMatching
{
	[Export(typeof (IViewTaggerProvider))]
	[ContentType("Clojure")]
	[TagType(typeof (TextMarkerTag))]
	public class BraceMatchingTaggerProvider : IViewTaggerProvider
	{
		public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
		{
			if (!TokenizedBufferBuilder.TokenizedBuffers.ContainsKey(buffer))
			{
				return null;
			}

			Entity<LinkedList<Token>> tokenizedBuffer = TokenizedBufferBuilder.TokenizedBuffers[buffer];
			BraceMatchingTagger matchingTagger = new BraceMatchingTagger(textView, new MatchingBraceFinder(new TextBufferAdapter(textView), tokenizedBuffer));
			textView.TextBuffer.Changed += (o, e) => matchingTagger.InvalidateAllTags();
			textView.Caret.PositionChanged += (o, e) => matchingTagger.InvalidateAllTags();
			return matchingTagger as ITagger<T>;
		}
	}
}