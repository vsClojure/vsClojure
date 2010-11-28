using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Microsoft.ClojureExtension.Editor.AutoFormat;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.ClojureExtension.Editor
{
	public class EditorCommandFactory
	{
		private readonly OleMenuCommandService _menuCommandService;
		private static readonly List<int> EditorCommandIds = new List<int>() {15};

		public EditorCommandFactory(OleMenuCommandService menuCommandService)
		{
			_menuCommandService = menuCommandService;
		}

		public void WireCommandsTo(ITextView view)
		{
			AutoFormatter formatter = new AutoFormatter(new TextBufferAdapter(view.TextBuffer), TokenizedBufferBuilder.TokenizedBuffers[view.TextBuffer]);
			_menuCommandService.AddCommand(new MenuCommand((sender, args) => formatter.Format(), new CommandID(Guids.GuidClojureExtensionCmdSet, 15)));
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
