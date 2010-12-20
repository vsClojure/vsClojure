using System.Runtime.InteropServices;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Project.Launching
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
            LaunchParameters launchParameters = _launchParametersProvider.Get();
            _validator.Validate(launchParameters);

            VsDebugTargetInfo info = new VsDebugTargetInfo();
            info.cbSize = (uint) Marshal.SizeOf(info);
            info.dlo = DEBUG_LAUNCH_OPERATION.DLO_CreateProcess;
            info.bstrExe = launchParameters.RunnerPath;
            info.bstrCurDir = launchParameters.ApplicationPath;
            info.fSendStdoutToOutputWindow = 0;
            info.grfLaunch = grfLaunch;
        	info.bstrArg = "-i " + launchParameters.TargetFile;
            info.bstrRemoteMachine = launchParameters.RemoteDebugMachine;
            info.clsidCustom = launchParameters.DebugType;

            VsShellUtilities.LaunchDebugger(_project.Site, info);
        }
    }
}