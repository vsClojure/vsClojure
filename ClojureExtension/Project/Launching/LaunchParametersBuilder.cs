using System;
using System.IO;
using Microsoft.ClojureExtension.Configuration;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.Win32;

namespace Microsoft.ClojureExtension.Project.Launching
{
    public class LaunchParametersBuilder : IProvider<LaunchParameters>
    {
        private readonly ProjectNode _project;
        private readonly string _packageInstallPath;

        public LaunchParametersBuilder(ProjectNode project, string packageInstallPath)
        {
            _project = project;
            _packageInstallPath = packageInstallPath;
        }

        public LaunchParameters Get()
        {
            string frameworkPath = _project.GetProjectProperty("ClojureFrameworkPath", true);
            string applicationPath = _project.GetProjectProperty("ProjectDir", false) + _project.GetProjectProperty("OutputPath", false);
            string startupNamespace = _project.GetProjectProperty("StartupNamespace", false);
            string startupFunction = _project.GetProjectProperty("StartupFunction", false);
            string startupArguments = _project.GetProjectProperty("StartupArguments", false);
            string remoteMachineDebug = _project.GetProjectProperty("RemoteDebugMachine", false);
            string unmanagedDebugging = _project.GetProjectProperty("EnableUnmanagedDebugging", false);
            LaunchType launchType = (LaunchType)Enum.Parse(typeof(LaunchType), _project.GetProjectProperty("LaunchType", false));

            frameworkPath = frameworkPath.TrimEnd(new[] {'\\'});
            applicationPath = applicationPath.TrimEnd(new[] {'\\'});

            string allArguments = "\"" + frameworkPath + "\" \"" + applicationPath + "\" \"" + startupNamespace + "\" \"" + startupFunction + "\"";
            if (!string.IsNullOrEmpty(startupArguments)) allArguments += " " + startupArguments;

            Guid debugType = !string.IsNullOrEmpty(unmanagedDebugging) && unmanagedDebugging.ToLower() == "true"
                ? new Guid("{3B476D35-A401-11D2-AAD4-00C04F990171}")
                : VSConstants.CLSID_ComPlusOnlyDebugEngine;

            string runnerPath = _packageInstallPath + "\\Project\\Launching\\Runners\\";
            if (launchType == LaunchType.ConsoleApplication) runnerPath += "ClojureConsoleRunner.exe";
            else runnerPath += "ClojureWindowsRunner.exe";

            return new LaunchParameters(
                runnerPath,
                frameworkPath,
                applicationPath,
                startupNamespace,
                startupFunction,
                startupArguments,
                allArguments,
                remoteMachineDebug,
                unmanagedDebugging,
                debugType,
                launchType);
        }
    }
}