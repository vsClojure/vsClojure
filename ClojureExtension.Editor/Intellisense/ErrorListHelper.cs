using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace ClojureExtension.Editor.Intellisense
{
  public class ErrorListHelper : IServiceProvider
  {
    private const string PROVIDER_GUID = "98679BA5-F492-481C-A2F8-DB4EC8AE7BB6";
    private const string PROVIDER_NAME = "VSCLOJURE Error List Provider";

    public object GetService(Type serviceType)
    {
      return Package.GetGlobalService(serviceType);
    }

    protected ErrorListProvider _errorListProvider;

    public ErrorListHelper()
    {
      _errorListProvider = new ErrorListProvider(this);
      _errorListProvider.ProviderName = PROVIDER_NAME;
      _errorListProvider.ProviderGuid = Guid.Parse(PROVIDER_GUID);
    }

    private void ParseErrorLocation(string error, out int? startLine, out int? startColumn, out int? endLine, out int? endColumn)
    {
      List<string> parsedError = new Regex("^\\(([^,]*),([^,]*),([^,]*),([^)]*)\\): (.*)").Matches(error).Cast<Match>().SelectMany(x => x.Groups.Cast<Group>()).Select(x => x.Value).ToList();

      int parsedInt;
      startLine = parsedError.Count > 2 && int.TryParse(parsedError[1], out parsedInt) ? (int?)parsedInt : null;
      startColumn = parsedError.Count > 3 && int.TryParse(parsedError[2], out parsedInt) ? (int?)parsedInt : null;
      endLine = parsedError.Count > 4 && int.TryParse(parsedError[3], out parsedInt) ? (int?)parsedInt : null;
      endColumn = parsedError.Count > 5 && int.TryParse(parsedError[4], out parsedInt) ? (int?)parsedInt : null;
    }
    
    public void Write(TaskCategory category, TaskErrorCategory errorCategory, string text, string document, int? line = null, int? column = null)
    {
      ErrorTask task = new ErrorTask();
      task.Text = text;
      task.ErrorCategory = errorCategory;

      int? startLine, startColumn, endLine, endColumn;
      ParseErrorLocation(text, out startLine, out startColumn, out endLine, out endColumn);

      task.Line = (line ?? startLine ?? 1) - 1;
      task.Column = (column ?? startColumn ?? 1) - 1;
      task.Document = document;
      task.Category = category;

      if (!string.IsNullOrEmpty(document))
      {
        task.Navigate += NavigateDocument;
      }
      _errorListProvider.Tasks.Add(task);
    }

    public void ClearErrors(string document)
    {
      TaskProvider.TaskCollection taskCollection = _errorListProvider.Tasks;
      foreach (ErrorTask errorToRemove in taskCollection.Cast<ErrorTask>().Where(x => x.Document == document).ToList())
      {
        taskCollection.Remove(errorToRemove);
      }
    }

    private void NavigateDocument(object sender, EventArgs e)
    {
      ErrorTask task = sender as ErrorTask;
      if (task == null)
      {
        throw new ArgumentException("sender");
      }

      int? startLine, startColumn, endLine, endColumn;
      ParseErrorLocation(task.Text, out startLine, out startColumn, out endLine, out endColumn);

      //use the helper class to handle the navigation
      OpenDocumentAndNavigateTo(task.Document, startLine ?? 1, startColumn ?? 1, endLine, endColumn);
    }

    private void OpenDocumentAndNavigateTo(string path, int startLine, int startColumn, int? endLine = null, int? endColumn = null)
    {
      IVsUIShellOpenDocument openDoc = Package.GetGlobalService(typeof(IVsUIShellOpenDocument)) as IVsUIShellOpenDocument;
      if (openDoc == null)
      {
        return;
      }

      Guid logicalView = VSConstants.LOGVIEWID_Code;
      IVsWindowFrame frame;
      try
      {
        Microsoft.VisualStudio.OLE.Interop.IServiceProvider sp;
        IVsUIHierarchy hier;
        uint itemid;
        openDoc.OpenDocumentViaProject(path, ref logicalView, out sp, out hier, out itemid, out frame);
      }
      catch (Exception)
      {
        return;
      }

      object docData;
      frame.GetProperty((int)__VSFPROPID.VSFPROPID_DocData, out docData);

      // Get the VsTextBuffer  
      VsTextBuffer buffer = docData as VsTextBuffer;
      if (buffer == null)
      {
        IVsTextBufferProvider bufferProvider = docData as IVsTextBufferProvider;
        if (bufferProvider != null)
        {
          IVsTextLines lines;
          try
          {
            bufferProvider.GetTextBuffer(out lines);
          }
          catch (Exception)
          {
            return;
          }

          buffer = lines as VsTextBuffer;
          Debug.Assert(buffer != null, "IVsTextLines does not implement IVsTextBuffer");
        }
      }

      IVsTextManager mgr = Package.GetGlobalService(typeof(VsTextManagerClass)) as IVsTextManager;
      if (mgr == null)
      {
        return;
      }

      mgr.NavigateToLineAndColumn(buffer, ref logicalView, startLine - 1, startColumn - 1, (endLine ?? startLine) - 1, (endColumn ?? startColumn) - 1);
    }
  }
}