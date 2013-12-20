// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using ClojureExtension.Project.Launching;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;

namespace ClojureExtension.Project
{
    public class ClojureProjectConfig : ProjectConfig
    {
        private readonly ProjectLauncher _launcher;

        public ClojureProjectConfig(ProjectNode project, string configuration, ProjectLauncher launcher) : base(project, configuration)
        {
            _launcher = launcher;
        }

        public override int DebugLaunch(uint grfLaunch)
        {
            _launcher.Execute();
            return VSConstants.S_OK;
        }
    }
}