// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

namespace ClojureExtension.Debugger
{
  public class ParsedExpression : IDebugParsedExpression
  {
    public int EvaluateSync(uint dwEvalFlags, uint dwTimeout, IDebugSymbolProvider pSymbolProvider, IDebugAddress pAddress, IDebugBinder pBinder, string bstrResultType, out IDebugProperty2 ppResult)
    {
      ppResult = (IDebugProperty2)null;
      return VSConstants.E_NOTIMPL;
    }
  }
}