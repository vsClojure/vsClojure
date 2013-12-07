using System;

namespace ClojureExtension.Utilities
{
  public class EnvironmentVariables
  {
    private const string VSCLOJURE_RUNTIMES_DIR = "VSCLOJURE_RUNTIMES_DIR";
    private const string CLOJURE_LOAD_PATH = "CLOJURE_LOAD_PATH";

    public static string ClojureLoadPath
    {
      get { return Environment.GetEnvironmentVariable(CLOJURE_LOAD_PATH); }
      set { Environment.SetEnvironmentVariable(CLOJURE_LOAD_PATH, value, EnvironmentVariableTarget.User); }
    }

    public static string VsClojureRuntimesDir
    {
      get { return Environment.GetEnvironmentVariable(VSCLOJURE_RUNTIMES_DIR); }
      set { Environment.SetEnvironmentVariable(VSCLOJURE_RUNTIMES_DIR, value, EnvironmentVariableTarget.User); }
    }
  }
}