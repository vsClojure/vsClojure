using System.ComponentModel.Composition;
using Microsoft.ClojureExtension.Editor.Tagger;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.Classification
{
	[Export(typeof (IViewTaggerProvider))]
	[ContentType("Clojure")]
	[TagType(typeof (ClassificationTag))]
	internal sealed class ClojureClassifierProvider : IViewTaggerProvider
	{
		[Export] [Name("Clojure")] [BaseDefinition("code")] internal static ContentTypeDefinition ClojureContentType = null;

		[Export] [FileExtension(".clj")] [ContentType("Clojure")] internal static FileExtensionToContentTypeDefinition ClojureFileType = null;

		[Import] internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

		[Import] internal IViewTagAggregatorFactoryService aggregatorFactory = null;

		public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
		{
			ITagAggregator<ClojureTokenTag> clojureTagAggregator =
				aggregatorFactory.CreateTagAggregator<ClojureTokenTag>(textView);

			return new ClojureClassifier(buffer, clojureTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
		}
	}
}