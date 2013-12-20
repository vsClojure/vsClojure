// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

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