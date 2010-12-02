using System.ComponentModel.Composition;
using Microsoft.ClojureExtension.Editor.Options;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.AutoIndent
{
	[Export(typeof (ISmartIndentProvider))]
	[ContentType("Clojure")]
	public class SmartIndentProvider : ISmartIndentProvider
	{
		[Import]
		public IEditorOptionsFactoryService EditorOptionsFactoryService { get; set; }

		public ISmartIndent CreateSmartIndent(ITextView textView)
		{
			return new SmartIndentAdapter(
				new ClojureSmartIndent(TokenizedBufferBuilder.TokenizedBuffers[textView.TextBuffer]),
				new EditorOptionsBuilder(EditorOptionsFactoryService.GetOptions(textView)));
		}
	}
}