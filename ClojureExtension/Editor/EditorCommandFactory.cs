using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Microsoft.ClojureExtension.Editor.AutoFormat;
using Microsoft.ClojureExtension.Editor.AutoIndent;
using Microsoft.ClojureExtension.Editor.Options;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.ClojureExtension.Editor
{
	public class EditorCommandFactory
	{
		private readonly OleMenuCommandService _menuCommandService;
		private readonly IEditorOptionsFactoryService _editorOptionsFactoryService;
		private static readonly List<int> EditorCommandIds = new List<int>() {15};

		public EditorCommandFactory(OleMenuCommandService menuCommandService, IEditorOptionsFactoryService editorOptionsFactoryService)
		{
			_menuCommandService = menuCommandService;
			_editorOptionsFactoryService = editorOptionsFactoryService;
		}

		public void WireCommandsTo(ITextView view)
		{
			var editorOptionsBuilder = new EditorOptionsBuilder(_editorOptionsFactoryService.GetOptions(view));
			var tokenizedBuffer = TokenizedBufferBuilder.TokenizedBuffers[view.TextBuffer];
			AutoFormatter formatter = new AutoFormatter(new TextBufferAdapter(view.TextBuffer), tokenizedBuffer);
			_menuCommandService.AddCommand(new MenuCommand((sender, args) => formatter.Format(editorOptionsBuilder.Get()), new CommandID(Guids.GuidClojureExtensionCmdSet, 15)));
		}

		public void UnwireEditorCommands()
		{
			EditorCommandIds.ForEach(commandId =>
			{
				MenuCommand menuCommand = _menuCommandService.FindCommand(new CommandID(Guids.GuidClojureExtensionCmdSet, commandId));
				if (menuCommand != null) _menuCommandService.RemoveCommand(menuCommand);
			});
		}
	}
}
