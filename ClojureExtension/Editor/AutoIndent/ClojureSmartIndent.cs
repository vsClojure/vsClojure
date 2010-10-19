/********************************************************************************
*    Copyright (c) ThorTech, L.L.C.. All rights reserved.
*    The use and distribution terms for this software are covered by the
*    GNU General Public License, version 2
*    (http://www.gnu.org/licenses/old-licenses/gpl-2.0.html) with classpath
*    exception (http://www.gnu.org/software/classpath/license.html)
*    which can be found in the file GPL-2.0+ClasspathException.txt at the root
*    of this distribution.
*    By using this software in any fashion, you are agreeing to be bound by
*    the terms of this license.
*    You must not remove this notice, or any other, from this software.
*******************************************************************************
*    Author: Frank Failla
*    Modified By: jmis
*******************************************************************************/

using System.Collections.Generic;
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
            int currentIndexBeforeLastDataStructureToken = lastDataStructureToken.StartIndex - 1;
            if (currentIndexBeforeLastDataStructureToken < 0) currentIndexBeforeLastDataStructureToken = 0;
            char characterBeforeLastDataStructureToken = codeBeforeLineBreak[currentIndexBeforeLastDataStructureToken];
            int previousLineIndent = 0;

            while (currentIndexBeforeLastDataStructureToken > 0 && characterBeforeLastDataStructureToken != '\r' && characterBeforeLastDataStructureToken != '\n')
            {
                previousLineIndent += characterBeforeLastDataStructureToken == '\t' ? _indentSize.Default : 1;
                characterBeforeLastDataStructureToken = codeBeforeLineBreak[--currentIndexBeforeLastDataStructureToken];
            }

            if (lastDataStructureToken.Type == ClojureLexer.OPEN_PAREN) return previousLineIndent + _indentSize.Default;
            return previousLineIndent + 1;
        }
    }
}