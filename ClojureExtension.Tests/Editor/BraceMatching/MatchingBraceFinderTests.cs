using System.Collections.Generic;
using ClojureExtension.Editor.InputHandling;
using ClojureExtension.Editor.TextBuffer;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.ClojureExtension.Editor.BraceMatching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ClojureExtension.Tests.Editor.BraceMatching
{
	[TestClass]
	public class MatchingBraceFinderTests
	{
		private Tokenizer _tokenizer;
		private Entity<LinkedList<Token>> _tokenizedBufferEntity;
		private MatchingBraceFinder _finder;
		private ITextBufferAdapter _textBuffer;

		[TestInitialize]
		public void Initialize()
		{
			_tokenizer = new Tokenizer();
			_tokenizedBufferEntity = new Entity<LinkedList<Token>>();
			_textBuffer = MockRepository.GenerateStub<ITextBufferAdapter>();
			_finder = new MatchingBraceFinder(_textBuffer, _tokenizedBufferEntity);
		}

		private string CreateTokensAndTextBuffer(string text)
		{
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(text);
			_textBuffer.Stub(b => b.Length).Return(text.Length);
			return text;
		}

		[TestMethod]
		public void ShouldNotFindAnyMatchingBracesWhenCursorIsNotTouchingAny()
		{
			CreateTokensAndTextBuffer("(declare sym1)");
			MatchingBracePair pair = _finder.FindMatchingBraces(10);
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}

		[TestMethod]
		public void ShouldNotFindAnyMatchingBracesWhenCursorIsInWhitespaceAtEndOfText()
		{
			string bufferText = CreateTokensAndTextBuffer("(declare sym1) ");
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.Length);
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}

		[TestMethod]
		public void ShouldFindMatchingBraceWhenCursorIsRightBeforeStartOfListAtBeginningOfText()
		{
			string bufferText = CreateTokensAndTextBuffer("(declare sym1) ");
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.IndexOf("("));
			Assert.IsNotNull(pair.Start);
			Assert.IsNotNull(pair.End);
			Assert.AreEqual(bufferText.IndexOf("("), pair.Start.StartIndex);
			Assert.AreEqual(bufferText.IndexOf(")"), pair.End.StartIndex);
		}

		[TestMethod]
		public void ShouldFindMatchingBraceWhenCursorIsRightBeforeStartOfListNotAtBeginningOfText()
		{
			string bufferText = CreateTokensAndTextBuffer(" (declare sym1) ");
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.IndexOf("("));
			Assert.IsNotNull(pair.Start);
			Assert.IsNotNull(pair.End);
			Assert.AreEqual(bufferText.IndexOf("("), pair.Start.StartIndex);
			Assert.AreEqual(bufferText.IndexOf(")"), pair.End.StartIndex);
		}

		[TestMethod]
		public void ShouldNotFindMatchingBracesWhenCursorIsJustRightOfStartOfList()
		{
			string bufferText = CreateTokensAndTextBuffer("(declare sym1) ");
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.IndexOf("d"));
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}

		[TestMethod]
		public void ShouldNotFindMatchingBracesWhenCursorIsJustLeftOfEndOfList()
		{
			string bufferText = CreateTokensAndTextBuffer("(declare sym1) ");
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.IndexOf(")"));
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}

		[TestMethod]
		public void ShouldFindMatchingBracesWhenCursorIsJustAfterLastListEnd()
		{
			string bufferText = CreateTokensAndTextBuffer("(declare sym1) ");
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.LastIndexOf(" "));
			Assert.IsNotNull(pair.Start);
			Assert.IsNotNull(pair.End);
		}

		[TestMethod]
		public void ShouldFindMatchingBracesWhenCursorIsJustAfterLastListEndAndAtEndOfText()
		{
			string bufferText = CreateTokensAndTextBuffer("(declare sym1)");
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.LastIndexOf(")") + 1);
			Assert.IsNotNull(pair.Start);
			Assert.IsNotNull(pair.End);
		}

		[TestMethod]
		public void ShouldNotFindMatchingBracesWhenCursorIsAfterLastCharacterNonBraceCharacterInText()
		{
			string bufferText = CreateTokensAndTextBuffer("(declare sym1");
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.LastIndexOf("1") + 1);
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}

		[TestMethod]
		public void ShouldNotFindMatchingBracesWhenTextIsEmpty()
		{
			CreateTokensAndTextBuffer("");
			MatchingBracePair pair = _finder.FindMatchingBraces(0);
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}
	}
}