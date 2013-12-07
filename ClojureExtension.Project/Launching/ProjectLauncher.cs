using System.IO;
using System.Runtime.InteropServices;
using ClojureExtension.Utilities;
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

		public void Execute()
		{
			var launchParameters = _launchParametersProvider.Get();
			_validator.Validate(launchParameters);

			var launchInfo = CreateClojureLaunchInfo(launchParameters);
			if (launchParameters.StartupFileType == StartupFileType.Executable) launchInfo = CreateExecutableLaunchInfo(launchParameters);
			VsShellUtilities.LaunchDebugger(_project.Site, launchInfo);
		}

		private static VsDebugTargetInfo CreateClojureLaunchInfo(LaunchParameters launchParameters)
		{
			var info = new VsDebugTargetInfo();
			info.cbSize = (uint)Marshal.SizeOf(info);
			info.dlo = DEBUG_LAUNCH_OPERATION.DLO_CreateProcess;
			info.bstrExe = launchParameters.RunnerPath;
			info.bstrCurDir = launchParameters.ApplicationPath;
			info.fSendStdoutToOutputWindow = 0;
			info.grfLaunch = (uint)__VSDBGLAUNCHFLAGS2.DBGLAUNCH_MergeEnv;
			info.bstrArg = "-i " + launchParameters.StartupFile;
			info.bstrRemoteMachine = launchParameters.RemoteDebugMachine;
			info.clsidCustom = launchParameters.DebugType;
			return info;
		}

		private static VsDebugTargetInfo CreateExecutableLaunchInfo(LaunchParameters launchParameters)
		{
			var info = new VsDebugTargetInfo();
			info.cbSize = (uint)Marshal.SizeOf(info);
			info.dlo = DEBUG_LAUNCH_OPERATION.DLO_CreateProcess;
			info.bstrExe = Path.Combine(launchParameters.ApplicationPath, launchParameters.StartupFile);
			info.bstrCurDir = launchParameters.ApplicationPath;
			info.fSendStdoutToOutputWindow = 0;
			info.grfLaunch = (uint)__VSDBGLAUNCHFLAGS2.DBGLAUNCH_MergeEnv;
			info.bstrArg = launchParameters.StartupArguments;
			info.bstrRemoteMachine = launchParameters.RemoteDebugMachine;
			info.bstrEnv = "clojure_load_path=" + launchParameters.FrameworkPath + "\0";
			info.clsidCustom = launchParameters.DebugType;
			return info;
		}
	}
}