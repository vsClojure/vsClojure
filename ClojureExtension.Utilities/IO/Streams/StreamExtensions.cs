using System.IO;

namespace ClojureExtension.Utilities.IO
{
	public static class StreamExtensions
	{
		public static void CopyContentsTo(this Stream source, Stream destination)
		{
			var dataBlock = new byte[2048];

			while (true)
			{
				int bytesRead = source.Read(dataBlock, 0, dataBlock.Length);
				if (bytesRead > 0) destination.Write(dataBlock, 0, bytesRead);
				else break;
			}
		}
	}
}
