using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.Classification
{
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "ClojureSymbol")]
    [Name("ClojureSymbol")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ClojureSymbol : ClassificationFormatDefinition
    {
        public ClojureSymbol()
        {
            DisplayName = "Clojure - Symbol";
            ForegroundColor = Colors.Blue;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "ClojureKeyword")]
    [Name("ClojureKeyword")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ClojureKeyword : ClassificationFormatDefinition
    {
        public ClojureKeyword()
        {
            DisplayName = "Clojure - Keyword";
            ForegroundColor = Colors.DarkMagenta;
        }
    }
}