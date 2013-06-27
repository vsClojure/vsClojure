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