using System;
using System.Collections.Generic;
using Microsoft.ClojureExtension.Configuration;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Project;

namespace Microsoft.ClojureExtension.Project.Hierarchy
{
    public class GetFrameworkFromSelectedProject : IProvider<Framework>
    {
        private readonly IProvider<EnvDTE.Project> _projectProvider;
        private readonly SettingsStore _settingsStore;

        public GetFrameworkFromSelectedProject(IProvider<EnvDTE.Project> projectProvider, SettingsStore settingsStore)
        {
            _projectProvider = projectProvider;
            _settingsStore = settingsStore;
        }

        public Framework Get()
        {
            EnvDTE.Project project = _projectProvider.Get();
            if (project.Kind.ToLower() != typeof(ClojureProjectFactory).GUID.ToString("B").ToLower()) throw new Exception(project.Name + " is not a Clojure project.");
            ProjectNode projectNode = (ProjectNode) project.Object;
            string frameworkName = projectNode.GetProjectProperty("ClojureFrameworkPath");
            List<Framework> frameworks = _settingsStore.Get<List<Framework>>("Frameworks");
            Framework selectedFramework = frameworks.Find(f => f.Name == frameworkName);
            if (string.IsNullOrEmpty(frameworkName)) throw new Exception("Clojure framework not specified for project.");
            if (selectedFramework == null) throw new Exception("Unknown Clojure framework: " + frameworkName);
            return selectedFramework;
        }
    }
}