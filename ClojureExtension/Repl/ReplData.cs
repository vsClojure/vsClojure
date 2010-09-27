using System.Diagnostics;
using System.Windows.Controls;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplData
    {
        private readonly Process _replProcess;
        private readonly TextBox _interactiveTextBox;
        private readonly TabItem _tab;

        public ReplData(Process replProcess, TextBox interactiveTextBox, TabItem tab)
        {
            _replProcess = replProcess;
            _interactiveTextBox = interactiveTextBox;
            _tab = tab;
        }

        public TabItem Tab
        {
            get { return _tab; }
        }

        public TextBox InteractiveTextBox
        {
            get { return _interactiveTextBox; }
        }

        public Process ReplProcess
        {
            get { return _replProcess; }
        }
    }
}