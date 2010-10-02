using System.ComponentModel.Composition;
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
            return new ClojureTokenTagger(buffer) as ITagger<T>;
        }
    }
}