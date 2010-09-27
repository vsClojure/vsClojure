/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.ClojureExtension;
using Microsoft.ClojureExtension.Project.Menu;
using Microsoft.ClojureExtension.Repl;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Project.Samples.CustomProject
{
    [Guid(PackageGuid)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\10.0")]
    [ProvideObject(typeof (GeneralPropertyPage))]
    [ProvideProjectFactory(typeof (ClojureProjectFactory), "Clojure", "Clojure Project Files (*.cljproj);*.cljproj", "cljproj", "cljproj", @"Templates\Projects\Clojure", LanguageVsTemplate = "Clojure", NewProjectRequireNewFolderVsTemplate = false)]
    [ProvideProjectItem(typeof (ClojureProjectFactory), "Clojure Items", @"Templates\ProjectItems\Clojure", 500)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof (ReplToolWindow))]
    public sealed class ClojurePackage : ProjectPackage
    {
        public const string PackageGuid = "40953a10-3425-499c-8162-a90059792d13";

        protected override void Initialize()
        {
            base.Initialize();
            intializeMenuItems();
            RegisterProjectFactory(new ClojureProjectFactory(this));
        }

        private void intializeMenuItems()
        {
            OleMenuCommandService mcs = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
            ReplToolWindow window = (ReplToolWindow) FindToolWindow(typeof (ReplToolWindow), 0, true);
            IVsWindowFrame windowFrame = (IVsWindowFrame) window.Frame;
            DTE2 dte = (DTE2)GetService(typeof(DTE));
            
            mcs.AddCommand(
                new MenuCommand(
                    (sender, args) => new LoadFileIntoActiveRepl(dte.ToolWindows.SolutionExplorer, window.ReplManager, windowFrame).Execute(),
                    new CommandID(Guids.GuidClojureExtensionCmdSet, CommandIds.LoadFileIntoActiveRepl)));

            mcs.AddCommand(
                new MenuCommand(
                    menuItemClick,
                    new CommandID(Guids.GuidClojureExtensionCmdSet, CommandIds.LoadProjectIntoActiveRepl)));

            mcs.AddCommand(
                new MenuCommand(
                    (sender, args) => new StartReplUsingProjectVersion(window, windowFrame).Execute(),
                    new CommandID(Guids.GuidClojureExtensionCmdSet, CommandIds.StartReplUsingProjectVersion)));
        }

        private void menuItemClick(object sender, EventArgs args)
        {
            ReplToolWindow window = (ReplToolWindow) FindToolWindow(typeof (ReplToolWindow), 0, true);
            IVsWindowFrame windowFrame = (IVsWindowFrame) window.Frame;
            ErrorHandler.ThrowOnFailure(windowFrame.Show());
            window.CreateNewRepl();
        }

        public override string ProductUserContext
        {
            get { return "ClojureProj"; }
        }
    }
}