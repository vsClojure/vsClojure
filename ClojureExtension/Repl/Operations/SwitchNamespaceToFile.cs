using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ClojureExtension.Editor.Parsing;
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
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader(File.ReadAllText(activeFilePath))));
            Token token = lexer.Next();

            while (token != null)
            {
                if (token.Text == "ns" || token.Text == "in-ns")
                {
                    Token namespaceToken = lexer.Next();
                    while (namespaceToken.Type == TokenType.Whitespace) namespaceToken = lexer.Next();
                    if (namespaceToken.Type != TokenType.Symbol) throw new Exception("Cannot determine file namespace.");
                    string completeNamespace = namespaceToken.Text;
                    _replWriter.WriteBehindTheSceneExpressionToRepl("(do (require '" + completeNamespace + ") (in-ns '" + completeNamespace + "))");
                    return;
                }

                token = lexer.Next();
            }
        }
    }
}