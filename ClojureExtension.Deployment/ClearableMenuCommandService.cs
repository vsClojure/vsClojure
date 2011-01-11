using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;

namespace ClojureExtension.Deployment
{
	public class ClearableMenuCommandService : OleMenuCommandService
	{
		private readonly LinkedList<MenuCommand> _menuCommands;

		public ClearableMenuCommandService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_menuCommands = new LinkedList<MenuCommand>();
		}

		public void Clear()
		{
			foreach (MenuCommand command in _menuCommands) RemoveCommand(command);
			_menuCommands.Clear();
		}

		public override void AddCommand(MenuCommand command)
		{
			base.AddCommand(command);
			_menuCommands.AddLast(command);
		}
	}
}