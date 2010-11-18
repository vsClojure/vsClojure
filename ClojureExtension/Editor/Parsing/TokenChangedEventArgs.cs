using System;

namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public class TokenChangedEventArgs : EventArgs
	{
		private readonly BufferMappedTokenData _bufferMappedTokenData;

		public TokenChangedEventArgs(BufferMappedTokenData bufferMappedTokenData)
		{
			_bufferMappedTokenData = bufferMappedTokenData;
		}

		public BufferMappedTokenData BufferMappedTokenData
		{
			get { return _bufferMappedTokenData; }
		}
	}
}