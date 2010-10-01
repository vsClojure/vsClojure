using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Antlr.Runtime;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.ClojureExtension.Editor.AutoIndent
{
    public class ClojureSmartIndent : ISmartIndent
    {
        private readonly IndentSize _indentSize;

        public ClojureSmartIndent() : this(new IndentSize())
        {
            
        }

        public ClojureSmartIndent(IndentSize indentSize)
        {
            _indentSize = indentSize;
        }

        public void Dispose()
        {
        }

        public int? GetDesiredIndentation(ITextSnapshotLine line)
        {
            string codeBeforeLineBreak = line.Snapshot.GetText(0, line.Start);
            ClojureLexer lexer = new ClojureLexer(new ANTLRStringStream(codeBeforeLineBreak));
            CommonToken token = (CommonToken) lexer.NextToken();
            Stack<CommonToken> openDataStructures = new Stack<CommonToken>();

            while (token.Type != -1)
            {
                if (token.Type == ClojureLexer.OPEN_PAREN) openDataStructures.Push(token);
                else if (token.Type == ClojureLexer.LEFT_CURLY_BRACKET) openDataStructures.Push(token);
                else if (token.Type == ClojureLexer.LEFT_SQUARE_BRACKET) openDataStructures.Push(token);
                else if (openDataStructures.Count > 0)
                {
                    if (token.Type == ClojureLexer.CLOSE_PAREN) openDataStructures.Pop();
                    else if (token.Type == ClojureLexer.RIGHT_CURLY_BRACKET) openDataStructures.Pop();
                    else if (token.Type == ClojureLexer.RIGHT_SQUARE_BRACKET) openDataStructures.Pop();
                }

                token = (CommonToken) lexer.NextToken();
            }

            if (openDataStructures.Count == 0) return null;

            CommonToken lastDataStructureToken = openDataStructures.Peek();
            int beginningOfLine = lastDataStructureToken.StartIndex;
            char indentCharacter = codeBeforeLineBreak[beginningOfLine];

            while (beginningOfLine > 0 && indentCharacter != '\r' && indentCharacter != '\n') indentCharacter = codeBeforeLineBreak[--beginningOfLine];

            string previousLineIndent = codeBeforeLineBreak.Substring(beginningOfLine + 1, lastDataStructureToken.StartIndex - beginningOfLine - 1);
            int actualPreviousLineIndent = 0;

            foreach (char c in previousLineIndent)
                if (c == '\t') actualPreviousLineIndent += _indentSize.Default;
                else actualPreviousLineIndent += 1;

            if (lastDataStructureToken.Type == ClojureLexer.OPEN_PAREN) return actualPreviousLineIndent + _indentSize.Default;
            return actualPreviousLineIndent + 1;
        }
    }
}