using System;

namespace ClojureExtension.Project.Launching
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
		private readonly string _startupArguments;
		private readonly StartupFileType _startupFileType;

		public LaunchParameters(string runnerPath, string frameworkPath, string applicationPath, string startupFile, string remoteDebugMachine, string enableUnmanagedDebugging, Guid debugType, string startupArguments, StartupFileType startupFileType)
		{
			_runnerPath = runnerPath;
			_frameworkPath = frameworkPath;
			_applicationPath = applicationPath;
			_startupFile = startupFile;
			_remoteDebugMachine = remoteDebugMachine;
			_enableUnmanagedDebugging = enableUnmanagedDebugging;
			_debugType = debugType;
			_startupArguments = startupArguments;
			_startupFileType = startupFileType;
		}

		public StartupFileType StartupFileType
		{
			get { return _startupFileType; }
		}

		public string StartupArguments
		{
			get { return _startupArguments; }
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