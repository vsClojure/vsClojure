using System;
using System.IO;

namespace Microsoft.ClojureExtension.Project.Launching
{
    public class LaunchParametersValidator
    {
        public void Validate(LaunchParameters launchParameters)
        {
            if (string.IsNullOrEmpty(launchParameters.FrameworkPath))
                throw new Exception("No clojure framework path defined in project properties.");

            if (string.IsNullOrEmpty(launchParameters.StartupNamespace))
                throw new Exception("No startup namespace defined in project properties.");

            if (string.IsNullOrEmpty(launchParameters.StartupFunction))
                throw new Exception("No startup function defined in project properties.");

            if (!File.Exists(launchParameters.RunnerPath))
                throw new Exception("Cannot find Clojure runner at " + launchParameters.RunnerPath);
        }
    }
}