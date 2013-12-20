// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.IO;

namespace ClojureExtension.Utilities.IO.FileSystem
{
	public class RelativePathFileSystemWriter : IFileSystemWriter
	{
		private readonly string _rootPath;

		public RelativePathFileSystemWriter(string rootPath)
		{
			_rootPath = rootPath;
		}

		public void CreateFile(Stream contentsStream, string relativePath)
		{
			using (var inflatedFileStream = File.Create(Path.Combine(_rootPath, relativePath)))
			{
				contentsStream.CopyContentsTo(inflatedFileStream);
			}
		}

		public void CreateDirectory(string relativePath)
		{
			Directory.CreateDirectory(Path.Combine(_rootPath, relativePath));
		}
	}
}
