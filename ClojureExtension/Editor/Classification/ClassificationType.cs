using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.Classification
{
    internal static class OrdinaryClassificationDefinition
    {
        [Export(typeof (ClassificationTypeDefinition))] [Name("ClojureSymbol")] internal static ClassificationTypeDefinition clojureSymbol = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("ClojureKeyword")]
        internal static ClassificationTypeDefinition clojureKeyword = null;
    }
}