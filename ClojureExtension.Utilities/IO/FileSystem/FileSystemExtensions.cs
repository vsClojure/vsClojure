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