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
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ClojureExtension.Project.Configuration;
using EnvDTE;
using Microsoft.ClojureExtension;
using Microsoft.ClojureExtension.Configuration;
using Microsoft.ClojureExtension.Project;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;
using Microsoft.VisualStudio.Shell;
using VSLangProj;

namespace ClojureExtension.Project
{
    /// <summary>
    ///   This class extends the ProjectNode in order to represent our project 
    ///   within the hierarchy.
    /// </summary>
    [Guid(Guids.ClojureProject)]
    public class ClojureProjectNode : ProjectNode
    {
        #region Enum for image list

        internal enum MyCustomProjectImageName
        {
            Project = 0,
        }

        #endregion

        #region Constants

        internal const string ProjectTypeName = "Clojure";

        #endregion

        #region Fields

		private Package package;
        internal static int imageOffset;
        private static ImageList imageList;
        private VSProject vsProject;

		public Package Package
    	{
			get { return package; }
    	}

        #endregion

        #region Constructors

        /// <summary>
        ///   Initializes the <see cref = "ClojureProjectNode" /> class.
        /// </summary>
        static ClojureProjectNode()
        {
            imageList = Utilities.GetImageList(typeof (ClojureProjectNode).Assembly.GetManifestResourceStream("ClojureExtension.Project.Resources.ClojureImageList.bmp"));
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ClojureProjectNode" /> class.
        /// </summary>
        /// <param name = "package">Value of the project package for initialize internal package field.</param>
		public ClojureProjectNode(Package package)
        {
            this.package = package;

            InitializeImageList();

            CanProjectDeleteItems = true;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets the image list.
        /// </summary>
        /// <value>The image list.</value>
        public static ImageList ImageList
        {
            get { return imageList; }
            set { imageList = value; }
        }

        protected internal VSProject VSProject
        {
            get
            {
                if (vsProject == null)
                {
                    vsProject = new OAVSProject(this);
                }

                return vsProject;
            }
        }

        #endregion

        #region Overriden implementation

        /// <summary>
        ///   Gets the project GUID.
        /// </summary>
        /// <value>The project GUID.</value>
        public override Guid ProjectGuid
        {
            get { return typeof (ClojureProjectFactory).GUID; }
        }

        public override bool IsCodeFile(string fileName)
        {
            return fileName.ToLower().EndsWith(".clj");
        }

        /// <summary>
        ///   Gets the type of the project.
        /// </summary>
        /// <value>The type of the project.</value>
        public override string ProjectType
        {
            get { return ProjectTypeName; }
        }

        /// <summary>
        ///   Return an imageindex
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public override int ImageIndex
        {
            get { return imageOffset + (int) MyCustomProjectImageName.Project; }
        }

        /// <summary>
        ///   Returns an automation object representing this node
        /// </summary>
        /// <returns>The automation object</returns>
        public override object GetAutomationObject()
        {
            return new OAClojureProject(this);
        }

        /// <summary>
        ///   Creates the file node.
        /// </summary>
        /// <param name = "item">The project element item.</param>
        /// <returns></returns>
        public override FileNode CreateFileNode(ProjectElement item)
        {
            ClojureProjectFileNode node = new ClojureProjectFileNode(this, item);

            node.OleServiceProvider.AddService(typeof (EnvDTE.Project), new OleServiceProvider.ServiceCreatorCallback(CreateServices), false);
            node.OleServiceProvider.AddService(typeof (ProjectItem), node.ServiceCreator, false);
            node.OleServiceProvider.AddService(typeof (VSProject), new OleServiceProvider.ServiceCreatorCallback(CreateServices), false);

            return node;
        }

        /// <summary>
        ///   Generate new Guid value and update it with GeneralPropertyPage GUID.
        /// </summary>
        /// <returns>Returns the property pages that are independent of configuration.</returns>
        protected override Guid[] GetConfigurationIndependentPropertyPages()
        {
            Guid[] result = new Guid[1];
            result[0] = typeof (GeneralPropertyPage).GUID;
            return result;
        }

        /// <summary>
        ///   Overriding to provide project general property page.
        /// </summary>
        /// <returns>Returns the GeneralPropertyPage GUID value.</returns>
        protected override Guid[] GetPriorityProjectDesignerPages()
        {
            Guid[] result = new Guid[1];
            result[0] = typeof (GeneralPropertyPage).GUID;
            return result;
        }

        /// <summary>
        ///   Adds the file from template.
        /// </summary>
        /// <param name = "source">The source template.</param>
        /// <param name = "target">The target file.</param>
        public override void AddFileFromTemplate(string source, string target)
        {
            if (!File.Exists(source))
            {
                throw new FileNotFoundException(string.Format("Template file not found: {0}", source));
            }

            string projectPath = Path.GetDirectoryName(GetMkDocument());
            string relativePath = Path.GetDirectoryName(target).Substring(projectPath.Length);

            string fileNamespace = relativePath.Replace("\\", ".");
            fileNamespace = fileNamespace.TrimStart(new[] {'.'});
            if (!string.IsNullOrEmpty(fileNamespace)) fileNamespace += ".";
            fileNamespace += Path.GetFileNameWithoutExtension(target);

            FileTemplateProcessor.AddReplace("%namespace%", fileNamespace);

            try
            {
                FileTemplateProcessor.UntokenFile(source, target);
                FileTemplateProcessor.Reset();
            }
            catch (Exception e)
            {
                throw new FileLoadException("Failed to add template file to project", target, e);
            }
        }

        #endregion

        #region Private implementation

        private void InitializeImageList()
        {
            imageOffset = ImageHandler.ImageList.Images.Count;

            foreach (Image img in ImageList.Images)
            {
                ImageHandler.AddImage(img);
            }
        }

        private object CreateServices(Type serviceType)
        {
            object service = null;
            if (typeof (VSProject) == serviceType)
            {
                service = VSProject;
            }
            else if (typeof (EnvDTE.Project) == serviceType)
            {
                service = GetAutomationObject();
            }
            return service;
        }

        protected override ConfigProvider CreateConfigProvider()
        {
            return new ClojureConfigProvider(this);
        }

        #endregion
    }
}