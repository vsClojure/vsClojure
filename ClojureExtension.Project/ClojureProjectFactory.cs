using System.Runtime.InteropServices;
using ClojureExtension.Project;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Microsoft.ClojureExtension.Project
{
	/// <summary>
	/// Represent the methods for creating projects within the solution.
	/// </summary>
    [Guid("985F20FF-87AE-45F6-86E0-1DBBF0224EB9")]
	public class ClojureProjectFactory : ProjectFactory
	{
		#region Fields
		private Package package;
		#endregion

		#region Constructors
		/// <summary>
		/// Explicit default constructor.
		/// </summary>
		/// <param name="package">Value of the project package for initialize internal package field.</param>
		public ClojureProjectFactory(Package package)
			: base(package)
		{
			this.package = package;
		}
		#endregion

		#region Overriden implementation
		/// <summary>
		/// Creates a new project by cloning an existing template project.
		/// </summary>
		/// <returns></returns>
		protected override ProjectNode CreateProject()
		{
			ClojureProjectNode project = new ClojureProjectNode(this.package);
			project.SetSite((IServiceProvider)((System.IServiceProvider)this.package).GetService(typeof(IServiceProvider)));
			return project;
		}
		#endregion
	}
}