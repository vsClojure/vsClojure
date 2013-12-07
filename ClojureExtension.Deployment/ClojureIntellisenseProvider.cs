using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace ClojureExtension.Deployment
{
  [ComVisible(true)]
  [Guid(IntellisenseProviderGuid)]
  public sealed class ClojureIntellisenseProvider : IVsIntellisenseProject, IDisposable
  {
    public const string IntellisenseProviderGuid = "991874E5-8C2F-4EDC-98BA-689B289FBB3D";
    public int Init(IVsIntellisenseProjectHost pHost)
    {
      throw new NotImplementedException();
    }

    public int Close()
    {
      throw new NotImplementedException();
    }

    public int AddFile(string bstrAbsPath, uint itemid)
    {
      throw new NotImplementedException();
    }

    public int RemoveFile(string bstrAbsPath, uint itemid)
    {
      throw new NotImplementedException();
    }

    public int RenameFile(string bstrAbsPath, string bstrNewAbsPath, uint itemid)
    {
      throw new NotImplementedException();
    }

    public int IsCompilableFile(string bstrFileName)
    {
      throw new NotImplementedException();
    }

    public int GetContainedLanguageFactory(out IVsContainedLanguageFactory ppContainedLanguageFactory)
    {
      throw new NotImplementedException();
    }

    public int GetCompilerReference(out object ppCompilerReference)
    {
      throw new NotImplementedException();
    }

    public int GetFileCodeModel(object pProj, object pProjectItem, out object ppCodeModel)
    {
      throw new NotImplementedException();
    }

    public int GetProjectCodeModel(object pProj, out object ppCodeModel)
    {
      throw new NotImplementedException();
    }

    public int RefreshCompilerOptions()
    {
      throw new NotImplementedException();
    }

    public int GetCodeDomProviderName(out string pbstrProvider)
    {
      throw new NotImplementedException();
    }

    public int IsWebFileRequiredByProject(out int pbReq)
    {
      throw new NotImplementedException();
    }

    public int AddAssemblyReference(string bstrAbsPath)
    {
      throw new NotImplementedException();
    }

    public int RemoveAssemblyReference(string bstrAbsPath)
    {
      throw new NotImplementedException();
    }

    public int AddP2PReference(object pUnk)
    {
      throw new NotImplementedException();
    }

    public int RemoveP2PReference(object pUnk)
    {
      throw new NotImplementedException();
    }

    public int StopIntellisenseEngine()
    {
      throw new NotImplementedException();
    }

    public int StartIntellisenseEngine()
    {
      throw new NotImplementedException();
    }

    public int IsSupportedP2PReference(object pUnk)
    {
      throw new NotImplementedException();
    }

    public int WaitForIntellisenseReady()
    {
      throw new NotImplementedException();
    }

    public int GetExternalErrorReporter(out IVsReportExternalErrors ppErrorReporter)
    {
      throw new NotImplementedException();
    }

    public int SuspendPostedNotifications()
    {
      throw new NotImplementedException();
    }

    public int ResumePostedNotifications()
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}