using ClojureExtension.Parsing;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.ClojureExtension.Editor.Tagger
{
    public class ClojureTokenTag : ITag
    {
        private readonly Token _token;

		public Token Token
        {
            get { return _token; }
        }

		public ClojureTokenTag(Token token)
        {
            _token = token;
        }
    }
}