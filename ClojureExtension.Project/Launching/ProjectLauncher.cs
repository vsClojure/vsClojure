using System.Runtime.InteropServices;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ClojureExtension.Project.Launching
{
	public class ProjectLauncher
	{
		private readonly ProjectNode _project;
		private readonly IProvider<LaunchParameters> _launchParametersProvider;
		private readonly LaunchParametersValidator _validator;

		public ProjectLauncher(ProjectNode project, IProvider<LaunchParameters> launchParametersProvider, LaunchParametersValidator validator)
		{
			_project = project;
			_launchParametersProvider = launchParametersProvider;
			_validator = validator;
		}

		public void Execute(uint grfLaunch)
		{
			var launchParameters = _launchParametersProvider.Get();
			_validator.Validate(launchParameters);

			var launchInfo = CreateClojureLaunchInfo(launchParameters, grfLaunch);
			if (launchParameters.StartupFileType == StartupFileType.Executable) launchInfo = CreateExecutableLaunchInfo(launchParameters, grfLaunch);
			VsShellUtilities.LaunchDebugger(_project.Site, launchInfo);
		}

		private static VsDebugTargetInfo CreateClojureLaunchInfo(LaunchParameters launchParameters, uint grfLaunch)
		{
			VsDebugTargetInfo info = new VsDebugTargetInfo();
			info.cbSize = (uint)Marshal.SizeOf(info);
			info.dlo = DEBUG_LAUNCH_OPERATION.DLO_CreateProcess;
			info.bstrExe = launchParameters.RunnerPath;
			info.bstrCurDir = launchParameters.ApplicationPath;
			info.fSendStdoutToOutputWindow = 0;
			info.grfLaunch = grfLaunch;
			info.bstrArg = "-i " + launchParameters.StartupFile;
			info.bstrRemoteMachine = launchParameters.RemoteDebugMachine;
			info.clsidCustom = launchParameters.DebugType;
			return info;
		}

		private static VsDebugTargetInfo CreateExecutableLaunchInfo(LaunchParameters launchParameters, uint grfLaunch)
		{
			VsDebugTargetInfo info = new VsDebugTargetInfo();
			info.cbSize = (uint)Marshal.SizeOf(info);
			info.dlo = DEBUG_LAUNCH_OPERATION.DLO_CreateProcess;
			info.bstrExe = launchParameters.ApplicationPath + "\\" + launchParameters.StartupFile;
			info.bstrCurDir = launchParameters.ApplicationPath;
			info.fSendStdoutToOutputWindow = 0;
			info.grfLaunch = grfLaunch;
			info.bstrArg = launchParameters.StartupArguments;
			info.bstrRemoteMachine = launchParameters.RemoteDebugMachine;
			info.bstrEnv = "clojure.load.path=" + launchParameters.FrameworkPath + ";" + launchParameters.ClojureLoadPath + "\0";
			info.clsidCustom = launchParameters.DebugType;
			return info;
		}
	}
}