// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.Collections.Generic;
using ClojureExtension.Utilities;
using EnvDTE80;

namespace ClojureExtension.Repl.Operations
{
    public class ActiveFileProvider : IProvider<List<string>>
    {
        private readonly DTE2 _dte;

        public ActiveFileProvider(DTE2 dte)
        {
            _dte = dte;
        }

        public List<string> Get()
        {
            return new List<string>() { _dte.ActiveDocument.FullName };
        }
    }
}