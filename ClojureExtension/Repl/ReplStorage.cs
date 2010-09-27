using System.Collections.Generic;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplStorage
    {
        private readonly List<ReplData> _replList;

        public ReplStorage()
        {
            _replList = new List<ReplData>();
        }

        public void SaveRepl(ReplData replData)
        {
            if (!_replList.Contains(replData)) _replList.Add(replData);
        }

        public List<ReplData> GetRepls()
        {
            return new List<ReplData>(_replList);
        }
    }
}