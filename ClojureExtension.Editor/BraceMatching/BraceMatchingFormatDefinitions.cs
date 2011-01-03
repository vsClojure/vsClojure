using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.ClojureExtension.Editor.BraceMatching
{
    [Export(typeof (EditorFormatDefinition))]
    [Name("ClojureBraceNotFound")]
    [UserVisible(true)]
    internal class BraceNotFoundFormatDefinition : MarkerFormatDefinition
    {
        public BraceNotFoundFormatDefinition()
        {
            BackgroundColor = Colors.MistyRose;
            DisplayName = "Clojure - Brace Not Found";
            ZOrder = 5;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [Name("ClojureBraceFound")]
    [UserVisible(true)]
    internal class BraceFoundFormatDefinition : MarkerFormatDefinition
    {
        public BraceFoundFormatDefinition()
        {
            BackgroundColor = Colors.LightBlue;
            DisplayName = "Clojure - Brace Found";
            ZOrder = 5;
        }
    }
}