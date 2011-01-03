using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Microsoft.ClojureExtension.Utilities
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
			foreach (MenuCommand menuCommand in _menuCommands)
			{
				MenuCommand existingMenuCommand = _menuCommandService.FindCommand(menuCommand.CommandID);
				if (existingMenuCommand != null) _menuCommandService.RemoveCommand(existingMenuCommand);
				_menuCommandService.AddCommand(menuCommand);
				menuCommand.Visible = false;
			}
		}
	}
}