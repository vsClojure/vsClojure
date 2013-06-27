using System;
using System.Globalization;
using Microsoft.VisualStudio.Shell;

namespace ClojureExtension.Deployment
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  public sealed class RegisterSnippetsAttribute : RegistrationAttribute
  {
    public RegisterSnippetsAttribute(string languageGuid, bool showRoots, short displayName, string languageStringId, string indexPath, string forceCreateDirectories, string paths)
    {
      Language = new Guid(languageGuid);
      ShowRoots = showRoots;
      DisplayName = displayName;
      LanguageStringId = languageStringId;
      IndexPath = indexPath;
      ForceCreateDirectories = forceCreateDirectories;
      Paths = paths;
    }

    public Guid Language { get; private set; }
    public bool ShowRoots { get; private set; }
    public short DisplayName { get; private set; }
    public string LanguageStringId { get; private set; }
    public string IndexPath { get; private set; }
    public string ForceCreateDirectories { get; private set; }
    public string Paths { get; private set; }

    private string LanguageName()
    {
      return string.Format(CultureInfo.InvariantCulture, "Languages\\CodeExpansions\\{0}", LanguageStringId);
    }

    public override void Register(RegistrationContext context)
    {
      if (context == null)
      {
        return;
      }

      using (Key childKey = context.CreateKey(LanguageName()))
      {
        childKey.SetValue("", Language.ToString("B"));

        string snippetIndexPath = context.ComponentPath;
        snippetIndexPath = System.IO.Path.Combine(snippetIndexPath, IndexPath);
        snippetIndexPath = context.EscapePath(System.IO.Path.GetFullPath(snippetIndexPath));

        childKey.SetValue("DisplayName", DisplayName.ToString(CultureInfo.InvariantCulture));
        childKey.SetValue("IndexPath", snippetIndexPath);
        childKey.SetValue("LangStringId", LanguageStringId.ToLowerInvariant());
        childKey.SetValue("Package", context.ComponentType.GUID.ToString("B"));
        childKey.SetValue("ShowRoots", ShowRoots ? 1 : 0);

        string snippetPaths = context.ComponentPath;
        snippetPaths = System.IO.Path.Combine(snippetPaths, ForceCreateDirectories);
        snippetPaths = context.EscapePath(System.IO.Path.GetFullPath(snippetPaths));

        //The following enables VS to look into a user directory for more user-created snippets
        string myDocumentsPath = @";%MyDocs%\Code Snippets\" + ClojureLanguage.CLOJURE_LANGUAGE_NAME + @"\My Code Snippets\";
        using (Key forceSubKey = childKey.CreateSubkey("ForceCreateDirs"))
        {
          forceSubKey.SetValue(LanguageStringId, snippetPaths + myDocumentsPath);
        }

        using (Key pathsSubKey = childKey.CreateSubkey("Paths"))
        {
          pathsSubKey.SetValue(LanguageStringId, snippetPaths + myDocumentsPath);
        }
      }
    }

    public override void Unregister(RegistrationContext context)
    {
      if (context != null)
      {
        context.RemoveKey(LanguageName());
      }
    }
  }
}