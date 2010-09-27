using System.Windows.Controls;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Repl
{
    public class SelectedReplProvider : IProvider<ReplData>
    {
        private readonly TabControl _replTabControl;
        private readonly ReplStorage _replStorage;

        public SelectedReplProvider(TabControl replTabControl, ReplStorage replStorage)
        {
            _replTabControl = replTabControl;
            _replStorage = replStorage;
        }

        public ReplData Get()
        {
            return _replStorage.GetRepls().Find(r => r.Tab == _replTabControl.SelectedItem);
        }
    }
}