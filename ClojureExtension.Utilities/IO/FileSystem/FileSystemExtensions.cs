// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.IO;

namespace ClojureExtension.Utilities.IO.FileSystem
{
	public class FileSystemExtensions
	{
		public static void ClearDirectory(string directoryPath)
		{
			if (Directory.Exists(directoryPath)) Directory.Delete(directoryPath, true);
			Directory.CreateDirectory(directoryPath);
		}

		public static string GetFileDirectoryNameFromFileName(string fileName)
		{
			var outputDirectoryName = Path.GetFileNameWithoutExtension(fileName);
			var fileDirectory = Path.GetDirectoryName(fileName);
			return Path.Combine(fileDirectory, outputDirectoryName);
		}
	}
}