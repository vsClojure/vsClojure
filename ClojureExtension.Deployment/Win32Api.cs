// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

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