using System.Runtime.InteropServices;
using ClojureExtension.Project;
using Microsoft.VisualStudio.Project.Automation;

namespace Microsoft.ClojureExtension.Project
{
    [ComVisible(true)]
    public class OAClojureProject : OAProject
    {
        public OAClojureProject(ClojureProjectNode project)
            : base(project)
        {
        }
    }
}