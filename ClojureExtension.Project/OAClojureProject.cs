// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

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