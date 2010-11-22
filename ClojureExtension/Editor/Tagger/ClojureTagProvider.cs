using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.Tagger
{
    [Export(typeof (ITaggerProvider))]
    [ContentType("Clojure")]
    [TagType(typeof (ClojureTokenTag))]
    public class ClojureTagProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
        	Entity<LinkedList<Token>> tokenizedBuffer = DocumentLoader.TokenizedBuffers[buffer];
			ClojureTokenTagger tagger = new ClojureTokenTagger(buffer, tokenizedBuffer);
        	BufferTextChangeHandler textChangeHandler = new BufferTextChangeHandler(new TextBufferAdapter(buffer), tokenizedBuffer);
			TextChangeAdapter textChangeAdapter = new TextChangeAdapter(textChangeHandler);
        	buffer.Changed += textChangeAdapter.OnTextChange;
			textChangeHandler.TokenChanged += tagger.OnTokenChange;
			return tagger as ITagger<T>;
        }
    }
}