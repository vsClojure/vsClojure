// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ClojureExtension.Utilities
{
	public class StreamBuffer
	{
		private readonly Queue<char> _buffer;

		public StreamBuffer()
		{
			_buffer = new Queue<char>();
		}

		public void ReadStream(Stream stream)
		{
			while (true)
			{
				var c = stream.ReadByte();

				while (c != -1)
				{
					lock (_buffer) _buffer.Enqueue((char) c);
					c = stream.ReadByte();
				}

				Thread.Sleep(2);
			}
		}

		public bool HasData
		{
			get { return _buffer.Count > 0; }
		}

		public string GetData()
		{
			var output = new StringBuilder();
			lock (_buffer) while (_buffer.Count > 0) output.Append(_buffer.Dequeue());
			return output.ToString();
		}
	}
}