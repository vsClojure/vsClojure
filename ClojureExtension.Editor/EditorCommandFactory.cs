using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.ClojureExtension.Editor.AutoFormat;
using Microsoft.ClojureExtension.Editor.Options;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.ClojureExtension.Editor
{
	public class EditorCommandFactory
	{
		private readonly IEditorOptionsFactoryService _editorOptionsFactoryService;

		public EditorCommandFactory(IEditorOptionsFactoryService editorOptionsFactoryService)
		{
			_editorOptionsFactoryService = editorOptionsFactoryService;
		}

		public List<MenuCommand> CreateMenuCommands(ITextView view)
		{
			var editorOptionsBuilder = new EditorOptionsBuilder(_editorOptionsFactoryService.GetOptions(view));
			var tokenizedBuffer = TokenizedBufferBuilder.TokenizedBuffers[view.TextBuffer];
			AutoFormatter formatter = new AutoFormatter(new TextBufferAdapter(view.TextBuffer), tokenizedBuffer);
			MenuCommand formatMenuCommand = new MenuCommand((sender, args) => formatter.Format(editorOptionsBuilder.Get()), new CommandID(Guids.GuidClojureExtensionCmdSet, 15));
			return new List<MenuCommand>() {formatMenuCommand};
		}
	}
}