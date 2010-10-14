using System;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Project;

namespace Microsoft.ClojureExtension.Project.Hierarchy
{
    public class GetFrameworkFromSelectedProject : IProvider<string>
    {
        private readonly IProvider<EnvDTE.Project> _projectProvider;

        public GetFrameworkFromSelectedProject(IProvider<EnvDTE.Project> projectProvider)
        {
            _projectProvider = projectProvider;
        }

        public string Get()
        {
            EnvDTE.Project project = _projectProvider.Get();
            if (project.Kind.ToLower() != typeof (ClojureProjectFactory).GUID.ToString("B").ToLower()) throw new Exception(project.Name + " is not a Clojure project.");
            ProjectNode projectNode = (ProjectNode) project.Object;
            string frameworkPath = projectNode.GetProjectProperty("ClojureFrameworkPath");
            if (string.IsNullOrEmpty(frameworkPath)) throw new Exception("Clojure framework not specified for project.");
            return frameworkPath;
        }
    }
}