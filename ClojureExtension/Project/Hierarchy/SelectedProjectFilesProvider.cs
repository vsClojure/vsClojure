using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Project.Samples.CustomProject;

namespace Microsoft.ClojureExtension.Project.Hierarchy
{
    public class SelectedProjectFilesProvider : IProvider<List<string>>
    {
        private readonly Solution _solution;
        private readonly UIHierarchy _solutionExplorer;

        public SelectedProjectFilesProvider(Solution solution, UIHierarchy solutionExplorer)
        {
            _solution = solution;
            _solutionExplorer = solutionExplorer;
        }

        public List<string> Get()
        {
            List<string> files = new List<string>();
            EnvDTE.Project project = getSelectedProject();
            if (project == null) return files;

            Queue<ProjectItem> projectItemsToLookAt = new Queue<ProjectItem>();
            foreach (ProjectItem projectItem in project.ProjectItems) projectItemsToLookAt.Enqueue(projectItem);

            while (projectItemsToLookAt.Count > 0)
            {
                ProjectItem currentProjectItem = projectItemsToLookAt.Dequeue();

                if (currentProjectItem.Object.GetType() == typeof(ClojureProjectFileNode))
                    files.Add(currentProjectItem.Properties.Item("FullPath").Value.ToString());

                if (currentProjectItem.ProjectItems != null && currentProjectItem.ProjectItems.Count > 0)
                    foreach (ProjectItem childItem in currentProjectItem.ProjectItems)
                        projectItemsToLookAt.Enqueue(childItem);
            }

            return files;
        }

        private EnvDTE.Project getSelectedProject()
        {
            Array selectedItems = (Array) _solutionExplorer.SelectedItems;
            if (selectedItems.GetLength(0) != 1) return null;
            UIHierarchyItem selectedItem = (UIHierarchyItem) selectedItems.GetValue(0);
            EnvDTE.Project selectedProject = (EnvDTE.Project) selectedItem.Object;

            foreach (EnvDTE.Project project in _solution.Projects)
                if (selectedProject.UniqueName == project.UniqueName)
                    return project;

            return null;
        }
    }
}