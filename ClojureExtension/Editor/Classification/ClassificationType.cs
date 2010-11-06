using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.Classification
{
    internal static class OrdinaryClassificationDefinition
    {

        [Export(typeof (ClassificationTypeDefinition))]
		[Name("ClojureSymbol")]
		internal static ClassificationTypeDefinition ClojureSymbol = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("ClojureString")]
		internal static ClassificationTypeDefinition ClojureString = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureNumber")]
		internal static ClassificationTypeDefinition ClojureNumber = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureComment")]
		internal static ClassificationTypeDefinition ClojureComment = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureKeyword")]
		internal static ClassificationTypeDefinition ClojureKeyword = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureCharacter")]
		internal static ClassificationTypeDefinition ClojureCharacter = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureBuiltIn")]
		internal static ClassificationTypeDefinition ClojureBuiltIn = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureBoolean")]
		internal static ClassificationTypeDefinition ClojureBoolean = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureList")]
		internal static ClassificationTypeDefinition ClojureList = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureVector")]
		internal static ClassificationTypeDefinition ClojureVector = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureMap")]
		internal static ClassificationTypeDefinition ClojureMap = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("ClojureMetadataTypeHint")]
		internal static ClassificationTypeDefinition ClojureMetadataTypeHint = null;
    }
}