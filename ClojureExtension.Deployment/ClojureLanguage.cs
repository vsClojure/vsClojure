// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;
using Microsoft.VisualStudio.Package;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TextManager.Interop;

namespace ClojureExtension.Deployment
{
  [Guid(CLOJURE_LANGUAGE_GUID)]
  public class ClojureLanguage : LanguageService
  {
    public const string CLOJURE_LANGUAGE_NAME = "Clojure";
    public const string CLOJURE_TEMPLATE_FOLDER_NAME = "Clojure";
    public const string CLOJURE_CODE_PROVIDER = "ClojureCodeProvider";
    public const string CLOJURE_LANGUAGE_GUID = "C1591EC7-00F3-426B-8A62-9B10F3855288";
    public const string CLJ_FILE_EXTENSION = ".clj";
    public const string CLJS_FILE_EXTENSION = ".cljs";

    public override string GetFormatFilterList()
    {
      return "CLJ File (*.clj) *.clj";
    }

    LanguagePreferences preferences = null;
    public override LanguagePreferences GetLanguagePreferences()
    {
      if (preferences == null)
      {
        preferences = new LanguagePreferences(this.Site, typeof(ClojureLanguage).GUID, this.Name);

        if (preferences != null)
        {
          preferences.Init();

          preferences.EnableCodeSense = true;
          preferences.EnableMatchBraces = true;
          preferences.EnableCommenting = true;
          preferences.EnableShowMatchingBrace = true;
          preferences.EnableMatchBracesAtCaret = true;
          preferences.HighlightMatchingBraceFlags = _HighlightMatchingBraceFlags.HMB_USERECTANGLEBRACES;
          preferences.LineNumbers = true;
          preferences.MaxErrorMessages = 100;
          preferences.AutoOutlining = false;
          preferences.MaxRegionTime = 2000;
          preferences.ShowNavigationBar = true;

          preferences.AutoListMembers = true;
          preferences.EnableQuickInfo = true;
          preferences.ParameterInformation = true;
        }
      }

      return preferences;
    }

    public override IScanner GetScanner(Microsoft.VisualStudio.TextManager.Interop.IVsTextLines buffer)
    {
      throw new NotImplementedException();
    }

    public override string Name
    {
      get { return CLOJURE_LANGUAGE_NAME; }
    }

    public override AuthoringScope ParseSource(ParseRequest req)
    {
      throw new NotImplementedException();
    }

    public override void Dispose()
    {
      try
      {
        if (preferences != null)
        {
          preferences.Dispose();
          preferences = null;
        }
      }
      finally
      {
        base.Dispose();
      }
    }
  }
}