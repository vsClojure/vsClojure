using System.IO;
using System.Windows.Controls;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Repl.Operations
{
	public class StartReplUsingProjectVersion
	{
		private readonly TabControl _replManager;
		private readonly ReplFactory _replFactory;
		private readonly IVsWindowFrame _toolWindowFrame;
		private readonly IProvider<string> _frameworkProvider;
		private readonly IProvider<EnvDTE.Project> _selectedProjectProvider;

		public StartReplUsingProjectVersion(
			TabControl replManager,
			ReplFactory replFactory,
			IVsWindowFrame toolWindowFrame,
			IProvider<string> frameworkProvider,
			IProvider<EnvDTE.Project> selectedProjectProvider)
		{
			_frameworkProvider = frameworkProvider;
			_selectedProjectProvider = selectedProjectProvider;
			_replManager = replManager;
			_replFactory = replFactory;
			_toolWindowFrame = toolWindowFrame;
		}

		public void Execute()
		{
			_replFactory.CreateRepl(_frameworkProvider.Get(), Path.GetDirectoryName(_selectedProjectProvider.Get().FullName), _replManager);
			_toolWindowFrame.Show();
		}
	}
}