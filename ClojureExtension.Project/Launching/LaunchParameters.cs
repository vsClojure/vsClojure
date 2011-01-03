using System;

namespace Microsoft.ClojureExtension.Project.Launching
{
	public class LaunchParameters
	{
		private readonly string _runnerPath;
		private readonly string _frameworkPath;
		private readonly string _applicationPath;
		private readonly string _startupFile;
		private readonly string _remoteDebugMachine;
		private readonly string _enableUnmanagedDebugging;
		private readonly Guid _debugType;

		public LaunchParameters(string runnerPath, string frameworkPath, string applicationPath, string startupFile, string remoteDebugMachine, string enableUnmanagedDebugging, Guid debugType)
		{
			_runnerPath = runnerPath;
			_frameworkPath = frameworkPath;
			_applicationPath = applicationPath;
			_startupFile = startupFile;
			_remoteDebugMachine = remoteDebugMachine;
			_enableUnmanagedDebugging = enableUnmanagedDebugging;
			_debugType = debugType;
		}

		public string StartupFile
		{
			get { return _startupFile; }
		}

		public Guid DebugType
		{
			get { return _debugType; }
		}

		public string EnableUnmanagedDebugging
		{
			get { return _enableUnmanagedDebugging; }
		}

		public string RemoteDebugMachine
		{
			get { return _remoteDebugMachine; }
		}

		public string ApplicationPath
		{
			get { return _applicationPath; }
		}

		public string FrameworkPath
		{
			get { return _frameworkPath; }
		}

		public string RunnerPath
		{
			get { return _runnerPath; }
		}
	}
}