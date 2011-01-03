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
        	ForegroundColor = Color.FromRgb(0, 0, 128);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "ClojureString")]
    [Name("ClojureString")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ClojureString : ClassificationFormatDefinition
    {
        public ClojureString()
        {
            DisplayName = "Clojure - String";
			ForegroundColor = Color.FromRgb(0, 128, 0);
        	IsBold = true;
        }
    }

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "ClojureNumber")]
	[Name("ClojureNumber")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class ClojureNumber : ClassificationFormatDefinition
	{
		public ClojureNumber()
		{
			DisplayName = "Clojure - Number";
			ForegroundColor = Color.FromRgb(0, 0, 255);
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "ClojureComment")]
	[Name("ClojureComment")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class ClojureComment : ClassificationFormatDefinition
	{
		public ClojureComment()
		{
			DisplayName = "Clojure - Comment";
			ForegroundColor = Color.FromRgb(128, 128, 128);
			IsItalic = true;
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
			ForegroundColor = Color.FromRgb(102, 14, 122);
			IsItalic = true;
			IsBold = true;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "ClojureCharacter")]
	[Name("ClojureCharacter")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class ClojureCharacter : ClassificationFormatDefinition
	{
		public ClojureCharacter()
		{
			DisplayName = "Clojure - Character";
			ForegroundColor = Color.FromRgb(0, 128, 0);
			IsBold = true;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "ClojureBuiltIn")]
	[Name("ClojureBuiltIn")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class ClojureBuiltIn : ClassificationFormatDefinition
	{
		public ClojureBuiltIn()
		{
			DisplayName = "Clojure - Built In";
			ForegroundColor = Colors.Orange;
			IsBold = true;
		}
	}
	
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "ClojureBoolean")]
	[Name("ClojureBoolean")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class ClojureBoolean : ClassificationFormatDefinition
	{
		public ClojureBoolean()
		{
			DisplayName = "Clojure - Boolean";
			ForegroundColor = Color.FromRgb(0, 0, 255);
		}
	}
	
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "ClojureList")]
	[Name("ClojureList")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class ClojureList : ClassificationFormatDefinition
	{
		public ClojureList()
		{
			DisplayName = "Clojure - List";
			ForegroundColor = Colors.Black;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "ClojureVector")]
	[Name("ClojureVector")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class ClojureVector : ClassificationFormatDefinition
	{
		public ClojureVector()
		{
			DisplayName = "Clojure - Vector";
			ForegroundColor = Colors.Black;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "ClojureMap")]
	[Name("ClojureMap")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class ClojureMap : ClassificationFormatDefinition
	{
		public ClojureMap()
		{
			DisplayName = "Clojure - Map";
			ForegroundColor = Colors.Black;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "ClojureMetadataTypeHint")]
	[Name("ClojureMetadataTypeHint")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class ClojureMetadataTypeHint : ClassificationFormatDefinition
	{
		public ClojureMetadataTypeHint()
		{
			DisplayName = "Clojure - Type Hint";
			ForegroundColor = Color.FromRgb(53, 145, 175);
		}
	}
}