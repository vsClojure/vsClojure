using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplStorageProvider : IProvider<ReplStorage>
    {
        public static ReplStorage Storage { get; set; }

        public ReplStorage Get()
        {
            return Storage;
        }
    }
}