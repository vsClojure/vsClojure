// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

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
      if (!TokenizedBufferBuilder.TokenizedBuffers.ContainsKey(textView.TextBuffer))
      {
        return null;
      }
			return new SmartIndentAdapter(
				new ClojureSmartIndent(TokenizedBufferBuilder.TokenizedBuffers[textView.TextBuffer]),
				new EditorOptionsBuilder(EditorOptionsFactoryService.GetOptions(textView)));
		}
	}
}