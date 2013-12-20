// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

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
