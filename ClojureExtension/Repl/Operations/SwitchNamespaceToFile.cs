using System;
using System.Collections.Generic;
using System.IO;
using Antlr.Runtime;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Repl.Operations
{
    public class SwitchNamespaceToFile
    {
        private readonly IProvider<List<string>> _activeFileProvider;
        private readonly ReplWriter _replWriter;

        public SwitchNamespaceToFile(
            IProvider<List<string>> activeFileProvider,
            ReplWriter replWriter)
        {
            _activeFileProvider = activeFileProvider;
            _replWriter = replWriter;
        }

        public void Execute()
        {
            string activeFilePath = _activeFileProvider.Get()[0];
            ClojureLexer lexer = new ClojureLexer(new ANTLRFileStream(activeFilePath));
            IToken token = lexer.NextToken();

            while (token.Type != -1)
            {
                if (token.Text == "ns" || token.Text == "in-ns")
                {
                    IToken namespaceToken = lexer.NextToken();
                    while (namespaceToken.Type == ClojureLexer.SPACE) namespaceToken = lexer.NextToken();
                    if (namespaceToken.Type != ClojureLexer.SYMBOL) throw new Exception("Cannot determine file namespace.");
                    string completeNamespace = namespaceToken.Text;
                    _replWriter.WriteBehindTheSceneExpressionToRepl("(do (require '" + completeNamespace + ") (in-ns '" + completeNamespace + "))");
                    return;
                }

                token = lexer.NextToken();
            }
        }
    }
}