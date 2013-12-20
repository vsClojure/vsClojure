// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;
using System.Diagnostics;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

namespace ClojureExtension.Debugger
{
  [DebuggerDisplay("{FullName}")]
  internal class PropertyBase : IDebugProperty2, IDebugProperty
  {
    public int GetPropertyInfo(uint dwFields, uint dwRadix, uint dwTimeout, IDebugReference2[] rgpArgs, uint dwArgCount, DEBUG_PROPERTY_INFO[] pPropertyInfo)
    {
      return VSConstants.E_NOTIMPL;
    }

    public int SetValueAsString(string pszValue, uint dwRadix, uint dwTimeout)
    {
      return VSConstants.E_NOTIMPL;
    }

    public int SetValueAsReference(IDebugReference2[] rgpArgs, uint dwArgCount, IDebugReference2 pValue, uint dwTimeout)
    {
      return VSConstants.E_NOTIMPL;
    }

    public int EnumChildren(uint dwFields, uint dwRadix, ref Guid guidFilter, ulong dwAttribFilter, string pszNameFilter, uint dwTimeout, out IEnumDebugPropertyInfo2 ppEnum)
    {
      ppEnum = (IEnumDebugPropertyInfo2)null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetParent(out IDebugProperty2 ppParent)
    {
      ppParent = (IDebugProperty2)null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetDerivedMostProperty(out IDebugProperty2 ppDerivedMost)
    {
      ppDerivedMost = (IDebugProperty2)null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetMemoryBytes(out IDebugMemoryBytes2 ppMemoryBytes)
    {
      ppMemoryBytes = (IDebugMemoryBytes2)null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetMemoryContext(out IDebugMemoryContext2 ppMemory)
    {
      ppMemory = (IDebugMemoryContext2)null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetSize(out uint pdwSize)
    {
      pdwSize = 0;
      return VSConstants.E_NOTIMPL;
    }

    public int GetReference(out IDebugReference2 ppReference)
    {
      ppReference = (IDebugReference2)null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetExtendedInfo(ref Guid guidExtendedInfo, out object pExtendedInfo)
    {
      pExtendedInfo = null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetPropertyInfo(uint dwFieldSpec, uint nRadix, DebugPropertyInfo[] pPropertyInfo)
    {
      return VSConstants.E_NOTIMPL;
    }

    public int GetExtendedInfo(uint cInfos, Guid[] rgguidExtendedInfo, object[] rgvar)
    {
      return VSConstants.E_NOTIMPL;
    }

    public int SetValueAsString(string pszValue, uint nRadix)
    {
      return VSConstants.E_NOTIMPL;
    }

    public int EnumMembers(uint dwFieldSpec, uint nRadix, ref Guid refiid, out IEnumDebugPropertyInfo ppepi)
    {
      ppepi = (IEnumDebugPropertyInfo)null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetParent(out IDebugProperty ppDebugProp)
    {
      ppDebugProp = (IDebugProperty)null;
      return VSConstants.E_NOTIMPL;
    }
  }
}