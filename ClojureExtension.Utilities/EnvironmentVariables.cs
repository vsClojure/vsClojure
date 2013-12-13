using System;
using System.Linq;

namespace ClojureExtension.Utilities
{
  public class EnvironmentVariables
  {
    private const string VSCLOJURE_RUNTIMES_DIR = "VSCLOJURE_RUNTIMES_DIR";

    public static string VsClojureRuntimesDir
    {
      get { return Environment.GetEnvironmentVariable(VSCLOJURE_RUNTIMES_DIR); }
      set { Environment.SetEnvironmentVariable(VSCLOJURE_RUNTIMES_DIR, value, EnvironmentVariableTarget.User); }
    }
  }
}