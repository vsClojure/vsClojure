/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.Drawing;
using ClojureExtension.Project;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;

namespace Microsoft.ClojureExtension.Project
{
	/// <summary>
	/// This class extends the FileNode in order to represent a file 
	/// within the hierarchy.
	/// </summary>
	public class ClojureProjectFileNode : FileNode
	{
		#region Fields
		private OAClojureProjectFileItem automationObject;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ClojureProjectFileNode"/> class.
		/// </summary>
		/// <param name="root">The project node.</param>
		/// <param name="e">The project element node.</param>
		internal ClojureProjectFileNode(ProjectNode root, ProjectElement e)
			: base(root, e)
		{
		}
		#endregion

		#region Overriden implementation
		/// <summary>
		/// Gets the automation object for the file node.
		/// </summary>
		/// <returns></returns>
		public override object GetAutomationObject()
		{
			if(automationObject == null)
			{
				automationObject = new OAClojureProjectFileItem(this.ProjectMgr.GetAutomationObject() as OAProject, this);
			}

			return automationObject;
		}

		public override object GetIconHandle(bool open)
		{
			Bitmap image = (Bitmap) ClojureProjectNode.ImageList.Images[1];
			return image.GetHicon();
		}

		#endregion

		#region Private implementation
		internal OleServiceProvider.ServiceCreatorCallback ServiceCreator
		{
			get { return new OleServiceProvider.ServiceCreatorCallback(this.CreateServices); }
		}

		private object CreateServices(Type serviceType)
		{
			object service = null;
			if(typeof(EnvDTE.ProjectItem) == serviceType)
			{
				service = GetAutomationObject();
			}
			return service;
		}
		#endregion
	}
}