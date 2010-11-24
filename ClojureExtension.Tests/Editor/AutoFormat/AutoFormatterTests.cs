using System.Collections.Generic;
using Microsoft.ClojureExtension.Editor.AutoFormat;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ClojureExtension.Tests.Editor.AutoFormat
{
	[TestClass]
	public class AutoFormatterTests
	{
		private ITextBufferAdapter _textBuffer;
		private Entity<LinkedList<Token>> _tokenizedBufferEntity;
		private Tokenizer _tokenizer;
		private AutoFormatter _formatter;

		[TestInitialize]
		public void Initialize()
		{
			_tokenizer = new Tokenizer();
			_tokenizedBufferEntity = new Entity<LinkedList<Token>>();
			_textBuffer = MockRepository.GenerateStub<ITextBufferAdapter>();
			_formatter = new AutoFormatter(_textBuffer, _tokenizedBufferEntity);
		}

		private void CreateTokensAndTextBuffer(string text)
		{
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(text);
			_textBuffer.Stub(b => b.Length).Return(text.Length);
		}

		[TestMethod]
		public void ShouldRemoveTrailingWhiteSpace()
		{
			CreateTokensAndTextBuffer("(println \"test\")  \r\n  ");
			_formatter.Format();
			_textBuffer.AssertWasCalled(t => t.SetText("(println \"test\")"));
		}
	}
}