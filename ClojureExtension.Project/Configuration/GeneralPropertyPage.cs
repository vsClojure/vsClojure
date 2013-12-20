// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;

namespace ClojureExtension.Project.Configuration
{
    [ComVisible(true)]
    [Guid("EBAF977F-39E1-481D-BFB8-13C960B55D6E")]
    public class GeneralPropertyPage : SettingsPage
    {
        private string _defaultNamespace;
        private string _clojureCompiler;
        private string _clojureVersion;
        private string _outputType;
        private string _targetFile;
        private string _startupArguments;

        public GeneralPropertyPage()
        {
            Name = "General";
        }

        [Description("Clojure Compiler")]
        [DisplayName("Clojure Compiler")]
        [Category("Clojure")]
        public string ClojureCompiler
        {
            get { return _clojureCompiler; }

            set
            {
                _clojureCompiler = value;
                IsDirty = true;
            }
        }

        [Description("Clojure Version")]
        [DisplayName("Clojure Version")]
        [Category("Clojure")]
        public string ClojureVersion
        {
            get { return _clojureVersion; }

            set
            {
                _clojureVersion = value;
                IsDirty = true;
            }
        }

        [Description("OutputType")]
        [DisplayName("OutputType")]
        [Category("Clojure")]
        public string OutputType
        {
            get { return _outputType; }

            set
            {
                _outputType = value;
                IsDirty = true;
            }
        }

        [Category("Running")]
        [DisplayName("Startup File")]
        [Description("Startup File")]
        public string TargetFile
        {
            get { return _targetFile; }
            set
            {
                _targetFile = value;
                IsDirty = true;
            }
        }

        [Category("Running")]
        [DisplayName("Startup Arguments")]
        [Description("Startup Arguments")]
        public string StartupArguments
        {
            get { return _startupArguments; }
            set
            {
                _startupArguments = value;
                IsDirty = true;
            }
        }

        [Category("Project")]
        [DisplayName("Project File")]
        [Description("Project File")]
        public string ProjectFile
        {
            get { return Path.GetFileName(ProjectMgr.ProjectFile); }
        }

        [Category("Project")]
        [DisplayName("Project Folder")]
        [Description("Project Folder")]
        public string ProjectFolder
        {
            get { return Path.GetDirectoryName(ProjectMgr.ProjectFolder); }
        }

        public override string GetClassName()
        {
            return GetType().FullName;
        }

        protected override void BindProperties()
        {
            _defaultNamespace = ProjectMgr.GetProjectProperty("RootNamespace", false);
            _clojureCompiler = ProjectMgr.GetProjectProperty("ClojureCompiler", false);
            _clojureVersion = ProjectMgr.GetProjectProperty("ClojureVersion", false);
            _outputType = ProjectMgr.GetProjectProperty("OutputType", false);
            _targetFile = ProjectMgr.GetProjectProperty("StartupFile", false);
            _startupArguments = ProjectMgr.GetProjectProperty("StartupArguments", false);
        }

        protected override int ApplyChanges()
        {
            ProjectMgr.SetProjectProperty("RootNamespace", _defaultNamespace);
            ProjectMgr.SetProjectProperty("ClojureCompiler", _clojureCompiler);
            ProjectMgr.SetProjectProperty("ClojureVersion", _clojureVersion);
            ProjectMgr.SetProjectProperty("OutputType", _outputType);
            ProjectMgr.SetProjectProperty("StartupFile", _targetFile);
            ProjectMgr.SetProjectProperty("StartupArguments", _startupArguments);
            IsDirty = false;
            return VSConstants.S_OK;
        }
    }
}