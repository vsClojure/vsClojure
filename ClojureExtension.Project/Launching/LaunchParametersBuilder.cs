using System;
using ClojureExtension.Utilities;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;

namespace ClojureExtension.Project.Launching
{
	public class LaunchParametersBuilder : IProvider<LaunchParameters>
	{
		private readonly ProjectNode _project;

		public LaunchParametersBuilder(ProjectNode project)
		{
			_project = project;
		}

		public LaunchParameters Get()
		{
			string frameworkPath = _project.GetProjectProperty("ClojureRuntimesDirectory");
			string applicationPath = _project.GetProjectProperty("ProjectDir", false) + _project.GetProjectProperty("OutputPath", false);
			string targetFile = _project.GetProjectProperty("StartupFile");
			string remoteMachineDebug = _project.GetProjectProperty("RemoteDebugMachine", false);
			string unmanagedDebugging = _project.GetProjectProperty("EnableUnmanagedDebugging", false);
			string startupArguments = _project.GetProjectProperty("StartupArguments", false);

			frameworkPath = frameworkPath.TrimEnd(new[] {'\\'});
			applicationPath = applicationPath.TrimEnd(new[] {'\\'});

			Guid debugType = !string.IsNullOrEmpty(unmanagedDebugging) && unmanagedDebugging.ToLower() == "true"
				? new Guid("{3B476D35-A401-11D2-AAD4-00C04F990171}")
				: VSConstants.CLSID_ComPlusOnlyDebugEngine;

			string runnerPath = frameworkPath + "\\Clojure.Main.exe";

			var startupFileType = StartupFileType.Unknown;
			if (targetFile.ToLower().EndsWith(".exe")) startupFileType = StartupFileType.Executable;
			if (targetFile.ToLower().EndsWith(".clj")) startupFileType = StartupFileType.ClojureCLR;
			if (targetFile.ToLower().EndsWith(".cljs")) startupFileType = StartupFileType.ClojureScript;

			return new LaunchParameters(
				runnerPath,
				frameworkPath,
				applicationPath,
				targetFile,
				remoteMachineDebug,
				unmanagedDebugging,
				debugType,
				startupArguments,
				startupFileType);
		}
	}
}
