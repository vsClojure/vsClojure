// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.Collections.Generic;
using System.ComponentModel.Composition;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.ClojureExtension.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace ClojureExtension.Editor.Intellisense
{
	[Export(typeof (ICompletionSourceProvider))]
	[ContentType("Clojure")]
	[Name("ClojureCompletion")]
	public class HippieCompletionSourceProvider : ICompletionSourceProvider
	{
		public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
		{
			Entity<LinkedList<Token>> tokenizedBuffer = TokenizedBufferBuilder.TokenizedBuffers[textBuffer];
			return new HippieCompletionSource(tokenizedBuffer);
		}
	}
}