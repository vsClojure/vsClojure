using System.Diagnostics;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplLauncher
    {
        public Process Execute(string replPath)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = replPath + "\\Clojure.Main.exe";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.AutoFlush = true;
            return process;
        }
    }
}