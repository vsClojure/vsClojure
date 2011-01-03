/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;

namespace Microsoft.ClojureExtension.Project
{
    [ComVisible(true)]
    [Guid("FBFF33D7-CCCD-4BEC-A75E-4ECF6DB4474C")]
    public class OAClojureProjectFileItem : OAFileItem
    {
        public OAClojureProjectFileItem(OAProject project, FileNode node)
            : base(project, node)
        {
        }
    }
}