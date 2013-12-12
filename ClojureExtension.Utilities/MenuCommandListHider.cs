using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ClojureExtension.Utilities
{
	public class MenuCommandListHider
	{
		private readonly IMenuCommandService _menuCommandService;
		private readonly List<MenuCommand> _menuCommands;

		public MenuCommandListHider(IMenuCommandService menuCommandService, List<MenuCommand> menuCommands)
		{
			_menuCommandService = menuCommandService;
			_menuCommands = menuCommands;
		}

		public void HideMenuCommands()
		{
			foreach (var menuCommand in _menuCommands)
			{
				var existingMenuCommand = _menuCommandService.FindCommand(menuCommand.CommandID);
				if (existingMenuCommand != null) _menuCommandService.RemoveCommand(existingMenuCommand);
				_menuCommandService.AddCommand(menuCommand);
				menuCommand.Visible = false;
			}
		}
	}
}