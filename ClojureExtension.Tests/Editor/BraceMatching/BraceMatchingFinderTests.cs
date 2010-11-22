using System.Collections.Generic;
using Microsoft.ClojureExtension.Editor.BraceMatching;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureExtension.Tests.Editor.BraceMatching
{
	[TestClass]
	public class BraceMatchingFinderTests
	{
		private Tokenizer _tokenizer;
		private Entity<LinkedList<Token>> _tokenizedBufferEntity;
		private MatchingBraceFinder _finder;

		[TestInitialize]
		public void Initialize()
		{
			_tokenizer = new Tokenizer();
			_tokenizedBufferEntity = new Entity<LinkedList<Token>>();
			_finder = new MatchingBraceFinder(_tokenizedBufferEntity);
		}

		[TestMethod]
		public void ShouldNotFindAnyMatchingBracesWhenCursorIsNotTouchingAny()
		{
			string bufferText = "(declare sym1)";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(bufferText);
			MatchingBracePair pair = _finder.FindMatchingBraces(10);
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}

		[TestMethod]
		public void ShouldNotFindAnyMatchingBracesWhenCursorIsInWhitespaceAtEndOfText()
		{
			string bufferText = "(declare sym1) ";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(bufferText);
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.Length);
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}

		[TestMethod]
		public void ShouldFindMatchingBraceWhenCursorIsRightBeforeStartOfListAtBeginningOfText()
		{
			string bufferText = "(declare sym1) ";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(bufferText);
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.IndexOf("("));
			Assert.IsNotNull(pair.Start);
			Assert.IsNotNull(pair.End);
			Assert.AreEqual(bufferText.IndexOf("("), pair.Start.StartIndex);
			Assert.AreEqual(bufferText.IndexOf(")"), pair.End.StartIndex);
		}

		[TestMethod]
		public void ShouldFindMatchingBraceWhenCursorIsRightBeforeStartOfListNotAtBeginningOfText()
		{
			string bufferText = " (declare sym1) ";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(bufferText);
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.IndexOf("("));
			Assert.IsNotNull(pair.Start);
			Assert.IsNotNull(pair.End);
			Assert.AreEqual(bufferText.IndexOf("("), pair.Start.StartIndex);
			Assert.AreEqual(bufferText.IndexOf(")"), pair.End.StartIndex);
		}

		[TestMethod]
		public void ShouldNotFindMatchingBracesWhenCursorIsJustRightOfStartOfList()
		{
			string bufferText = "(declare sym1) ";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(bufferText);
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.IndexOf("d"));
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}

		[TestMethod]
		public void ShouldNotFindMatchingBracesWhenCursorIsJustLeftOfEndOfList()
		{
			string bufferText = "(declare sym1) ";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(bufferText);
			MatchingBracePair pair = _finder.FindMatchingBraces(bufferText.IndexOf(")"));
			Assert.IsNull(pair.Start);
			Assert.IsNull(pair.End);
		}
	}
}