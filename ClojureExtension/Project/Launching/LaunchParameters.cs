using System;

namespace Microsoft.ClojureExtension.Project.Launching
{
    public class LaunchParameters
    {
        private readonly string _runnerPath;
        private readonly string _frameworkPath;
        private readonly string _applicationPath;
        private readonly string _startupNamespace;
        private readonly string _startupFunction;
        private readonly string _startupArguments;
        private readonly string _allArguments;
        private readonly string _remoteDebugMachine;
        private readonly string _enableUnmanagedDebugging;
        private readonly Guid _debugType;

        public LaunchParameters(string runnerPath, string frameworkPath, string applicationPath, string startupNamespace, string startupFunction, string startupArguments, string allArguments, string remoteDebugMachine, string enableUnmanagedDebugging, Guid debugType)
        {
            _runnerPath = runnerPath;
            _frameworkPath = frameworkPath;
            _applicationPath = applicationPath;
            _startupNamespace = startupNamespace;
            _startupFunction = startupFunction;
            _startupArguments = startupArguments;
            _allArguments = allArguments;
            _remoteDebugMachine = remoteDebugMachine;
            _enableUnmanagedDebugging = enableUnmanagedDebugging;
            _debugType = debugType;
        }

        public Guid DebugType
        {
            get { return _debugType; }
        }

        public string AllArguments
        {
            get { return _allArguments; }
        }

        public string EnableUnmanagedDebugging
        {
            get { return _enableUnmanagedDebugging; }
        }

        public string RemoteDebugMachine
        {
            get { return _remoteDebugMachine; }
        }

        public string StartupArguments
        {
            get { return _startupArguments; }
        }

        public string StartupFunction
        {
            get { return _startupFunction; }
        }

        public string StartupNamespace
        {
            get { return _startupNamespace; }
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