using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;
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
			Entity<LinkedList<Token>> tokenizedBuffer = DocumentLoader.TokenizedBuffers[buffer];
			BraceMatcher matcher = new BraceMatcher(textView, new MatchingBraceFinder(tokenizedBuffer));
			textView.TextBuffer.Changed += (o, e) => matcher.InvalidateAllTags();
			textView.Caret.PositionChanged += (o, e) => matcher.InvalidateAllTags();
			return matcher as ITagger<T>;
		}
	}
}