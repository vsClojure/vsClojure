using System;
using System.Runtime.InteropServices;

namespace ClojureExtension.Deployment
{
	public class Win32Api
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);
	}
}