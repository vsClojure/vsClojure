using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using EnvDTE;
using Microsoft.ClojureExtension.Repl;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Project.Menu
{
    public class LoadFileIntoActiveRepl
    {
        private readonly UIHierarchy _solutionExplorer;
        private readonly TabControl _replTabControl;
        private readonly IVsWindowFrame _replToolWindowFrame;

        public LoadFileIntoActiveRepl(UIHierarchy solutionExplorer, TabControl replTabControl, IVsWindowFrame replToolWindowFrame)
        {
            _solutionExplorer = solutionExplorer;
            _replTabControl = replTabControl;
            _replToolWindowFrame = replToolWindowFrame;
        }

        public void Execute()
        {
            Array items = _solutionExplorer.SelectedItems as Array;

            foreach (UIHierarchyItem item in items)
            {
                ProjectItem projectItem = item.Object as ProjectItem;
                string path = projectItem.Properties.Item("FullPath").Value.ToString();
                ReplTextPipe textPipe = (ReplTextPipe) ((TabItem) _replTabControl.SelectedItem).Tag;
                path = path.Replace("\\", "\\\\");
                textPipe.SendDirectlyToRepl("(load-file \"" + path + "\")");
                textPipe.SendToTextBox("\r\n");
            }

            ErrorHandler.ThrowOnFailure(_replToolWindowFrame.Show());
        }
    }
}
