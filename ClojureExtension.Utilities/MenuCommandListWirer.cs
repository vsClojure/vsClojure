using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ClojureExtension.Utilities
{
	public class MenuCommandListWirer
	{
		private readonly IMenuCommandService _menuCommandService;
		private readonly List<MenuCommand> _menuCommands;
		private readonly Func<bool> _shouldDisplay;

		public MenuCommandListWirer(IMenuCommandService menuCommandService, List<MenuCommand> menuCommands, Func<bool> shouldDisplay)
		{
			_menuCommandService = menuCommandService;
			_menuCommands = menuCommands;
			_shouldDisplay = shouldDisplay;
		}

		public void TryToShowMenuCommands()
		{
			if (!_shouldDisplay()) return;

			foreach (var menuCommand in _menuCommands)
			{
				var existingMenuCommand = _menuCommandService.FindCommand(menuCommand.CommandID);
				if (existingMenuCommand != null) _menuCommandService.RemoveCommand(existingMenuCommand);
				_menuCommandService.AddCommand(menuCommand);
			}
		}
	}
}