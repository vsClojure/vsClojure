using Antlr.Runtime;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor
{
    public class ClojureTokenTag : ITag
    {
        private readonly IToken _antlrToken;

        public IToken AntlrToken
        {
            get { return _antlrToken; }
        }

        public ClojureTokenTag(IToken antlrToken)
        {
            _antlrToken = antlrToken;
        }
    }
}