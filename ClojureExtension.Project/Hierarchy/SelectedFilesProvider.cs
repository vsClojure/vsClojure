// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

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