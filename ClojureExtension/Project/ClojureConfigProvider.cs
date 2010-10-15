using System.IO;
using Microsoft.ClojureExtension.Project.Launching;
using Microsoft.VisualStudio.Project;
using Microsoft.Win32;

namespace Microsoft.ClojureExtension.Project
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
                    new LaunchParametersBuilder(
                        _clojureProjectNode,
                        Path.GetDirectoryName(
                            (string) Registry.GetValue(
                                _clojureProjectNode.Package.ApplicationRegistryRoot + "\\Packages\\{" + typeof(ClojurePackage).GUID + "}",
                                "CodeBase",
                                null))),
                    new LaunchParametersValidator()));
        }
    }
}