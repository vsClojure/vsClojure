using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.Classification
{
    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "StartList")]
    [Name("StartList")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class ClojureStartList : ClassificationFormatDefinition
    {
        public ClojureStartList()
        {
            DisplayName = "StartList";
            ForegroundColor = Colors.Black;
        }
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "EndList")]
    [Name("EndList")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class ClojureEndList : ClassificationFormatDefinition
    {
        public ClojureEndList()
        {
            DisplayName = "EndList";
            ForegroundColor = Colors.Black;
        }
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "Symbol")]
    [Name("Symbol")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class ClojureSymbol : ClassificationFormatDefinition
    {
        public ClojureSymbol()
        {
            DisplayName = "Symbol";
            ForegroundColor = Colors.Blue;
        }
    }
}