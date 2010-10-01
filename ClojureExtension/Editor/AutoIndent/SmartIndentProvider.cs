using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.AutoIndent
{
    [Export(typeof(ISmartIndentProvider))]
    [ContentType("Clojure")]
    internal class SmartIndentProvider : ISmartIndentProvider
    {
        public ISmartIndent CreateSmartIndent(ITextView textView)
        {
            return new ClojureSmartIndent();
        }
    }
}