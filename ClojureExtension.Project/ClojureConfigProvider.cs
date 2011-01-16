using ClojureExtension.Project.Launching;
using Microsoft.ClojureExtension.Project;
using Microsoft.VisualStudio.Project;

namespace ClojureExtension.Project
{
	public class ClojureConfigProvider : ConfigProvider
	{
		private readonly ClojureProjectNode _clojureProjectNode;

		public ClojureConfigProvider(ClojureProjectNode clojureProjectNode) : base(clojureProjectNode)
		{
			_clojureProjectNode = clojureProjectNode;
		}

		protected override ProjectConfig CreateProjectConfiguration(string configName)
		{
			return new ClojureProjectConfig(
				_clojureProjectNode,
				configName,
				new ProjectLauncher(
					_clojureProjectNode,
					new LaunchParametersBuilder(_clojureProjectNode),
					new LaunchParametersValidator()));
		}
	}
}