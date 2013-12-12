using System;
using System.Collections.Generic;
using ClojureExtension.Utilities;
using EnvDTE;

namespace Microsoft.ClojureExtension.Project.Hierarchy
{
    public class SelectedFilesProvider : IProvider<List<string>>
    {
        private readonly UIHierarchy _solutionExplorer;

        public SelectedFilesProvider(UIHierarchy solutionExplorer)
        {
            _solutionExplorer = solutionExplorer;
        }

        public List<string> Get()
        {
            Array items = (Array) _solutionExplorer.SelectedItems;
            List<string> selectedFilePaths = new List<string>();

            foreach (UIHierarchyItem item in items)
            {
                ProjectItem projectItem = (ProjectItem) item.Object;
                string filePath = projectItem.Properties.Item("FullPath").Value.ToString();
                selectedFilePaths.Add(filePath);
            }

            return selectedFilePaths;
        }
    }
}