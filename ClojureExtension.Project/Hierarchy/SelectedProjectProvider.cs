// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;
using ClojureExtension.Utilities;
using EnvDTE;

namespace Microsoft.ClojureExtension.Project.Hierarchy
{
    public class SelectedProjectProvider : IProvider<EnvDTE.Project>
    {
        private readonly Solution _solution;
        private readonly UIHierarchy _solutionExplorer;

        public SelectedProjectProvider(Solution solution, UIHierarchy solutionExplorer)
        {
            _solution = solution;
            _solutionExplorer = solutionExplorer;
        }

        public EnvDTE.Project Get()
        {
            Array selectedItems = (Array) _solutionExplorer.SelectedItems;
            UIHierarchyItem selectedItem = (UIHierarchyItem) selectedItems.GetValue(0);
            EnvDTE.Project selectedProject = selectedItem.Object as EnvDTE.Project;

            if (selectedProject == null)
                return ((ProjectItem) selectedItem.Object).ContainingProject;

            foreach (EnvDTE.Project project in _solution.Projects)
                if (selectedProject.UniqueName == project.UniqueName)
                    return project;

            return null;
        }
    }
}