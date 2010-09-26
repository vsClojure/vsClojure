using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.ClojureExtension.Repl
{
    public class ReplLauncher
    {
        public Process Execute()
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = @"C:\Users\Jon\Desktop\richhickey-clojure-clr-1.2.0-0-g4a08e78\richhickey-clojure-clr-4a08e78\Clojure\Clojure.Main\bin\Debug\Clojure.Main.exe";
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
