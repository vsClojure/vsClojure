using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Debugger.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;

namespace ClojureExtension.Debugger
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  public sealed class RegisterExpressionEvaluatorAttribute : RegistrationAttribute
  {
    private const string EXPRESSION_EVALUATOR_REGISTRY_PATH = @"AD7Metrics\ExpressionEvaluator";
    private static readonly Guid COM_PLUS_ONLY_ENG_GUID = new Guid("449EC4CC-30D2-4032-9256-EE18EB41B62B");
    private static readonly Guid COM_PLUS_NATIVE_ENG_GUID = new Guid("92EF0900-2251-11D2-B72E-0000F87572EF");

    private readonly Guid _vendorGuid;
    private readonly Guid _languageGuid;
    private readonly Type _classType;
    private readonly string _languageName;

    public RegisterExpressionEvaluatorAttribute(Type classType, string languageName, string languageGuid, string vendorGuid)
    {
      _classType = classType;
      _languageName = languageName;
      _languageGuid = new Guid(languageGuid);
      _vendorGuid = new Guid(vendorGuid);
    }

    public override void Register(RegistrationContext context)
    {
      if (context == null)
      {
        return;
      }

      using (Key rk = context.CreateKey(String.Format("{0}\\{1:B}\\{2:B}", EXPRESSION_EVALUATOR_REGISTRY_PATH, _languageGuid, _vendorGuid)))
      {
        rk.SetValue("CLSID", _classType.GUID.ToString("B"));
        rk.SetValue("Language", _languageName);
        rk.SetValue("Name", _languageName);
        using (Key rk2 = rk.CreateSubkey("Engine"))
        {
          rk2.SetValue("0", COM_PLUS_ONLY_ENG_GUID.ToString("B"));
          rk2.SetValue("1", COM_PLUS_NATIVE_ENG_GUID.ToString("B"));
        }
      }
    }

    public override void Unregister(RegistrationContext context)
    {
      if (context == null)
      {
        return;
      }

      context.RemoveKey(String.Format("{0}\\{1:B}\\{2:B}", EXPRESSION_EVALUATOR_REGISTRY_PATH, _languageGuid, _vendorGuid));
      context.RemoveKeyIfEmpty(String.Format("{0}\\{1:B}", EXPRESSION_EVALUATOR_REGISTRY_PATH, _languageGuid));
    }
  }
}