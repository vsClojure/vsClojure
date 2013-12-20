// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

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