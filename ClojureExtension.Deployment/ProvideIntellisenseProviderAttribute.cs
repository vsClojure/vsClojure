using System;
using System.Globalization;
using Microsoft.VisualStudio.Shell;

namespace ClojureExtension.Deployment
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  public sealed class ProvideIntellisenseProviderAttribute : RegistrationAttribute
  {
    public ProvideIntellisenseProviderAttribute(Type provider, string providerName, string addItemLanguageName, string defaultExtension, string shortLanguageName, string templateFolderName)
    {
      Provider = provider;
      ProviderName = providerName;
      AddItemLanguageName = addItemLanguageName;
      DefaultExtension = defaultExtension;
      ShortLanguageName = shortLanguageName;
      TemplateFolderName = templateFolderName;
    }

    public Type Provider { get; private set; }
    public string ProviderName { get; private set; }
    public string AddItemLanguageName { get; private set; }
    public string DefaultExtension { get; private set; }
    public string ShortLanguageName { get; private set; }
    public string TemplateFolderName { get; private set; }
    //public string AdditionalExtensions { get; private set; }

    private string ProviderRegKey
    {
      get { return string.Format(CultureInfo.InvariantCulture, @"Languages\IntellisenseProviders\{0}", ProviderName); }
    }

    public override void Register(RegistrationContext context)
    {
      if (context == null)
      {
        return;
      }

      using (Key childKey = context.CreateKey(ProviderRegKey))
      {
        childKey.SetValue("GUID", Provider.GUID.ToString("B"));
        childKey.SetValue("AddItemLanguageName", AddItemLanguageName);
        childKey.SetValue("DefaultExtension", DefaultExtension);
        childKey.SetValue("ShortLanguageName", ShortLanguageName);
        childKey.SetValue("TemplateFolderName", TemplateFolderName);
/*
        if (!string.IsNullOrEmpty(AdditionalExtensions))
        {
          childKey.SetValue("AdditionalExtensions", AdditionalExtensions);
        }
*/
      }
    }

    public override void Unregister(RegistrationContext context)
    {
      if (context != null)
      {
        context.RemoveKey(ProviderRegKey);
      }
    }
  }
}