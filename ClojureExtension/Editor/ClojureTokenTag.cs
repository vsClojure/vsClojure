using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor
{
    public class ClojureTokenTag : ITag
    {
        public ClojureTokenTypes Type { get; private set; }

        public ClojureTokenTag(ClojureTokenTypes type)
        {
            Type = type;
        }
    }
}