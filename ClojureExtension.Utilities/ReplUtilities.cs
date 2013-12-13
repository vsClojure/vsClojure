using System.Diagnostics;

namespace ClojureExtension.Utilities
{
  public class ReplUtilities
  {
    private static readonly string clojureClrRuntime = string.Format("{0}\\{1}-{2}", EnvironmentVariables.VsClojureRuntimesDir, Constants.CLOJURECLR, Constants.VERSION);
    public static Process CreateReplProcess()
    {
      return CreateReplProcess(clojureClrRuntime, "");
    }

    public static Process CreateReplProcess(string replPath, string projectPath)
    {
      var process = new Process();
      process.EnableRaisingEvents = true;
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = "\"" + replPath + "\\Clojure.Main.exe\"";
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.UseShellExecute = false;
      return process;
    }    
  }
}