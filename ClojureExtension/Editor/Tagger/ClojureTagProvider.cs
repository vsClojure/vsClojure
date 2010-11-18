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
    internal sealed class ClojureTagProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
			Entity<LinkedList<Token>> tokenizedBuffer = new Entity<LinkedList<Token>>();
			tokenizedBuffer.CurrentState = new LinkedList<Token>();

			ClojureTokenTagger tagger = new ClojureTokenTagger(
				new Tokenizer(),
				new TokenList(tokenizedBuffer),
				buffer,
				tokenizedBuffer);

			BufferTextChangeHandler textChangeHandler = new BufferTextChangeHandler(
				new TextBufferAdapter(buffer),
				tokenizedBuffer,
				new TokenList(tokenizedBuffer),
				new Tokenizer());

			TextChangeAdapter textChangeAdapter = new TextChangeAdapter(textChangeHandler);
        	buffer.Changed += textChangeAdapter.OnTextChange;
        	textChangeHandler.TokenChanged += tagger.OnTokenChange;
			return tagger as ITagger<T>;
        }
    }
}