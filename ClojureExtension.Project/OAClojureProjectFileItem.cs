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