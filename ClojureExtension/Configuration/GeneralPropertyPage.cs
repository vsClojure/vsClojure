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
		private string _clojureVersion;
		private string _targetFile;

		public GeneralPropertyPage()
		{
			Name = "General";
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
			_clojureVersion = ProjectMgr.GetProjectProperty("ClojureVersion", false);
			_targetFile = ProjectMgr.GetProjectProperty("StartupFile", false);
		}

		protected override int ApplyChanges()
		{
			ProjectMgr.SetProjectProperty("RootNamespace", _defaultNamespace);
			ProjectMgr.SetProjectProperty("ClojureVersion", _clojureVersion);
			ProjectMgr.SetProjectProperty("StartupFile", _targetFile);
			IsDirty = false;
			return VSConstants.S_OK;
		}
	}
}