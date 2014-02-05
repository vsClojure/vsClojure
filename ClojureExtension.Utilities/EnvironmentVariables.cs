// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

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
      set
      {
        //Set the environment variable on the current process to avoid the need to restart. Set it for the User to allow it to persist for the next visual studio load.
        Environment.SetEnvironmentVariable(VSCLOJURE_RUNTIMES_DIR, value, EnvironmentVariableTarget.User);
        Environment.SetEnvironmentVariable(VSCLOJURE_RUNTIMES_DIR, value, EnvironmentVariableTarget.Process);
      }
    }
  }
}