/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System.IO;
using System.Runtime.InteropServices;
using Microsoft.ClojureExtension;

namespace Microsoft.VisualStudio.Project.Samples.CustomProject
{
    [ComVisible(true)]
    [Guid("5F9F1697-2E61-4c10-9AD2-94FA2A9BAAE8")]
    public class GeneralPropertyPage : SettingsPage
    {
        private string _defaultNamespace;
        private string _clojureFrameworkPath;

        public GeneralPropertyPage()
        {
            Name = Resources.GetString(Resources.GeneralCaption);
        }

        [ResourcesCategoryAttribute(Resources.ClojureFrameworkPath)]
        [LocDisplayName(Resources.ClojureFrameworkPath)]
        [ResourcesDescriptionAttribute(Resources.ClojureFrameworkPathDescription)]
        public string ClojureFrameworkPath
        {
            get { return _clojureFrameworkPath; }

            set
            {
                _clojureFrameworkPath = value;
                IsDirty = true;
            }
        }

        [ResourcesCategoryAttribute(Resources.Application)]
        [LocDisplayName(Resources.DefaultNamespace)]
        [ResourcesDescriptionAttribute(Resources.DefaultNamespaceDescription)]
        public string DefaultNamespace
        {
            get { return _defaultNamespace; }
            set
            {
                _defaultNamespace = value;
                IsDirty = true;
            }
        }

        [ResourcesCategoryAttribute(Resources.Project)]
        [LocDisplayName(Resources.ProjectFile)]
        [ResourcesDescriptionAttribute(Resources.ProjectFileDescription)]
        public string ProjectFile
        {
            get { return Path.GetFileName(ProjectMgr.ProjectFile); }
        }

        [ResourcesCategoryAttribute(Resources.Project)]
        [LocDisplayName(Resources.ProjectFolder)]
        [ResourcesDescriptionAttribute(Resources.ProjectFolderDescription)]
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
            if (ProjectMgr == null) return VSConstants.E_INVALIDARG;
            ProjectMgr.SetProjectProperty("RootNamespace", _defaultNamespace);
            ProjectMgr.SetProjectProperty("ClojureFrameworkPath", _clojureFrameworkPath);
            IsDirty = false;
            return VSConstants.S_OK;
        }
    }
}