using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.Classification
{
    internal static class OrdinaryClassificationDefinition
    {
        [Export(typeof (ClassificationTypeDefinition))] [Name("StartList")] internal static ClassificationTypeDefinition clojureStartList = null;

        [Export(typeof (ClassificationTypeDefinition))] [Name("EndList")] internal static ClassificationTypeDefinition clojureEndList = null;

        [Export(typeof (ClassificationTypeDefinition))] [Name("Symbol")] internal static ClassificationTypeDefinition clojureSymbol = null;
    }
}