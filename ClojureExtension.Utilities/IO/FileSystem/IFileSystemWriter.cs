using System.IO;

namespace ClojureExtension.Utilities.IO.FileSystem
{
	public interface IFileSystemWriter
	{
		void CreateFile(Stream contentsStream, string relativePath);
		void CreateDirectory(string relativePath);
	}
}