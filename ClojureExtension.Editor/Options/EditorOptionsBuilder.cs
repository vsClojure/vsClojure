using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClojureExtension.Utilities;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.ClojureExtension.Editor.Options
{
	public class EditorOptionsBuilder : IProvider<EditorOptions>
	{
		private readonly IEditorOptions _editorOptions;

		public EditorOptionsBuilder(IEditorOptions editorOptions)
		{
			_editorOptions = editorOptions;
		}

		public EditorOptions Get()
		{
			return new EditorOptions(_editorOptions.GetOptionValue<int>(new IndentSize().Key));
		}
	}
}
