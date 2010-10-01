using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.Classification
{
    [Export(typeof (ITaggerProvider))]
    [ContentType("Clojure")]
    [TagType(typeof (ClassificationTag))]
    internal sealed class ClojureClassifierProvider : ITaggerProvider
    {
        [Export] [Name("Clojure")] [BaseDefinition("code")] internal static ContentTypeDefinition ClojureContentType = null;

        [Export] [FileExtension(".clj")] [ContentType("Clojure")] internal static FileExtensionToContentTypeDefinition ClojureFileType = null;

        [Import] internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import] internal IBufferTagAggregatorFactoryService aggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            ITagAggregator<ClojureTokenTag> clojureTagAggregator =
                aggregatorFactory.CreateTagAggregator<ClojureTokenTag>(buffer);

            return new ClojureClassifier(buffer, clojureTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }
    }
}