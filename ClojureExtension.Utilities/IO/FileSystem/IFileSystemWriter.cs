// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.IO;

namespace ClojureExtension.Utilities.IO.FileSystem
{
	public interface IFileSystemWriter
	{
		void CreateFile(Stream contentsStream, string relativePath);
		void CreateDirectory(string relativePath);
	}
}