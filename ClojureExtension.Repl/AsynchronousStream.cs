using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ClojureExtension.Repl
{
	public class AsynchronousStream
	{
		private readonly Stream _stream;
		private readonly Queue<char> _buffer;
		private readonly Thread _thread;

		public AsynchronousStream(Stream stream)
		{
			_stream = stream;
			_buffer = new Queue<char>();
			_thread = new Thread(ReadStream);
		}

		public void Start()
		{
			_thread.Start();
		}

		public void Stop()
		{
			_thread.Abort();
		}

		private void ReadStream()
		{
			while (true)
			{
				char c = (char) _stream.ReadByte();

				while (c != -1)
				{
					lock (_buffer) _buffer.Enqueue(c);
					c = (char) _stream.ReadByte();
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