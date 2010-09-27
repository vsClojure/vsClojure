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
            Array items = (Array) _solutionExplorer.SelectedItems;
            List<string> filesToLoad = new List<string>();

            foreach (UIHierarchyItem item in items)
            {
                ProjectItem projectItem = (ProjectItem) item.Object;
                string filePath = projectItem.Properties.Item("FullPath").Value.ToString();
                if (!filePath.ToLower().EndsWith(".clj")) continue;
                filesToLoad.Add(filePath);
            }

            StringBuilder loadFileExpression = new StringBuilder("(map load-file '(");
            filesToLoad.ForEach(path => loadFileExpression.Append(" \"").Append(path.Replace("\\", "\\\\")).Append("\""));
            loadFileExpression.Append("))");

            ReplTextPipe textPipe = (ReplTextPipe)((TabItem)_replTabControl.SelectedItem).Tag;
            textPipe.SendDirectlyToRepl(loadFileExpression.ToString());
            textPipe.SendToTextBox("\r\n");

            ErrorHandler.ThrowOnFailure(_replToolWindowFrame.Show());
        }
    }
}
