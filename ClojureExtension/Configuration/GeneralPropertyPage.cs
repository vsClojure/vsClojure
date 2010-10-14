/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;

namespace Microsoft.ClojureExtension.Configuration
{
    [ComVisible(true)]
    [Guid("EBAF977F-39E1-481D-BFB8-13C960B55D6E")]
    public class GeneralPropertyPage : SettingsPage
    {
        private string _defaultNamespace;
        private string _clojureFrameworkPath;

        public GeneralPropertyPage()
        {
            Name = "General";
        }

        [Description("Clojure Framework Path")]
        [DisplayName("Clojure Framework Path")]
        [Category("Clojure")]
        public string ClojureFrameworkPath
        {
            get { return _clojureFrameworkPath; }

            set
            {
                _clojureFrameworkPath = value;
                IsDirty = true;
            }
        }

        [Category("Clojure")]
        [DisplayName("Default Namespace")]
        [Description("Default Namespace")]
        public string DefaultNamespace
        {
            get { return _defaultNamespace; }
            set
            {
                _defaultNamespace = value;
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
            _clojureFrameworkPath = ProjectMgr.GetProjectProperty("ClojureFrameworkPath", false);
        }

        protected override int ApplyChanges()
        {
            ProjectMgr.SetProjectProperty("RootNamespace", _defaultNamespace);
            ProjectMgr.SetProjectProperty("ClojureFrameworkPath", _clojureFrameworkPath);
            IsDirty = false;
            return VSConstants.S_OK;
        }
    }
}