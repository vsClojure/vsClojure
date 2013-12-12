using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using Microsoft.Win32;

namespace ClojureExtension.Debugger
{
  [ComVisible(true)]
  [Guid(CLOJURE_DEBUG_EXPRESSION_EVALUATOR_GUID)]
  public class ExpressionEvaluator : IDebugExpressionEvaluator
  {
    public const string CLOJURE_DEBUG_EXPRESSION_EVALUATOR_GUID = "81CBEA43-93CC-4FAE-85C8-1489A5DB5A50";
    public const string MICROSOFT_VENDOR_GUID = "994B45C4-E6E9-11D2-903F-00C04FA302A1";

    public int Parse(string upstrExpression, uint dwFlags, uint nRadix, out string pbstrError, out uint pichError, out IDebugParsedExpression ppParsedExpression)
    {
      pbstrError = null;
      pichError = 0;
      ppParsedExpression = (IDebugParsedExpression)null;
      return VSConstants.S_OK;
    }

    public int GetMethodProperty(IDebugSymbolProvider pSymbolProvider, IDebugAddress pAddress, IDebugBinder pBinder, int fIncludeHiddenLocals, out IDebugProperty2 ppProperty)
    {
      ppProperty = (IDebugProperty2)null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetMethodLocationProperty(string upstrFullyQualifiedMethodPlusOffset, IDebugSymbolProvider pSymbolProvider, IDebugAddress pAddress, IDebugBinder pBinder, out IDebugProperty2 ppProperty)
    {
      ppProperty = (IDebugProperty2)null;
      return VSConstants.E_NOTIMPL;
    }

    public int SetLocale(ushort wLangID)
    {
      return VSConstants.E_NOTIMPL;
    }

    public int SetRegistryRoot(string ustrRegistryRoot)
    {
      return VSConstants.E_NOTIMPL;
    }
  }
}