using System;
using System.Collections.Generic;
using ClojureExtension.Editor.InputHandling;
using ClojureExtension.Editor.TextBuffer;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ClojureExtension.Tests.Editor.Parsing
{
	[TestClass]
	public class BufferTextChangeHandlerTests
	{
		private Tokenizer _tokenizer;
		private Entity<LinkedList<Token>> _tokenizedBufferEntity;
		private ITextBufferAdapter _textBuffer;
		private BufferTextChangeHandler _bufferTextChangeHandler;

		[TestInitialize]
		public void Initialize()
		{
			_tokenizer = new Tokenizer();
			_tokenizedBufferEntity = new Entity<LinkedList<Token>>();
			_textBuffer = MockRepository.GenerateStub<ITextBufferAdapter>();

			_bufferTextChangeHandler = new BufferTextChangeHandler(
				_textBuffer,
				_tokenizedBufferEntity);

			_bufferTextChangeHandler.TokenChanged += MockRepository.GenerateStub<EventHandler<TokenChangedEventArgs>>();
		}

		[TestMethod]
		public void ShouldDeleteTokenFromFront()
		{
			ValidateTokenization("one two", "two");
		}

		[TestMethod]
		public void ShouldAddTokenToFront()
		{
			ValidateTokenization("one", "two one");
		}

		[TestMethod]
		public void ShouldDeleteLastToken()
		{
			ValidateTokenization("one two", "one");
		}

		[TestMethod]
		public void ShouldDeleteMiddleToken()
		{
			ValidateTokenization("one two three", "one three");
		}

		[TestMethod]
		public void ShouldInsertTokenBetweenTwoTokens()
		{
			ValidateTokenization("one three", "one two three");
		}

		[TestMethod]
		public void ShouldDeleteFirstTokenOfLengthOne()
		{
			ValidateTokenization("(one)", "one)");
		}

		[TestMethod]
		public void ShouldDeleteLastTokenOfLengthOne()
		{
			ValidateTokenization("(one)", "(one");
		}

		[TestMethod]
		public void ShouldDeleteMiddleTokenOfLengthOne()
		{
			ValidateTokenization("(o)", "()");
		}

		[TestMethod]
		public void ShouldInsertCharacterToFrontOfFirstToken()
		{
			ValidateTokenization("one two", "aone two");
		}

		[TestMethod]
		public void ShouldAppendCharacterToFirstToken()
		{
			ValidateTokenization("one two", "onea two");
		}

		[TestMethod]
		public void ShouldInsertCharacterIntoMiddleOfToken()
		{
			ValidateTokenization("one two", "onne two");
		}

		[TestMethod]
		public void ShouldAppendCharacterToTokenOfLengthOne()
		{
			ValidateTokenization("o two", "on two");
		}

		[TestMethod]
		public void ShouldPrependCharacterToTokenOfLengthOne()
		{
			ValidateTokenization("o two", "lo two");
		}

		[TestMethod]
		public void ShouldAppendCharacterToEmptyBuffer()
		{
			ValidateTokenization("", "a");
		}

		[TestMethod]
		public void ShouldRemoveLastCharacterFromBuffer()
		{
			ValidateTokenization("a", "");
		}

		[TestMethod]
		public void ShouldPrependCharacterToLastToken()
		{
			ValidateTokenization("one two", "one atwo");
		}

		[TestMethod]
		public void ShouldAppendCharacterToLastToken()
		{
			ValidateTokenization("one two", "one twoa");
		}

		[TestMethod]
		public void ShouldInsertCharacterIntoMiddleOfLastToken()
		{
			ValidateTokenization("one two", "one tawo");
		}

		[TestMethod]
		public void ShouldAppendCharacterToEndOfTokenThatTouchesAnotherToken()
		{
			ValidateTokenization("one two)", "one twoa)");
		}

		[TestMethod]
		public void ShouldPrependCharacterToBeginningOfTokenThatTouchesAnotherToken()
		{
			ValidateTokenization("one (two", "one (atwo");
		}

		[TestMethod]
		public void ShouldSplitTokenIntoTwo()
		{
			ValidateTokenization("onetwo", "one two");
		}

		[TestMethod]
		public void ShouldCombineTokens()
		{
			ValidateTokenization("one two", "onetwo");
		}

		[TestMethod]
		public void ShouldCascadeStringTokenWhenAddingDoubleQuote()
		{
			ValidateTokenization("one two three four", "one \"two three four");
		}

		[TestMethod]
		public void ShouldCascadeChangeWhenRemovingDoubleQuote()
		{
			ValidateTokenization("one \"two three \"four", "one two three \"four");
		}

		[TestMethod]
		public void ShouldCascadeComment()
		{
			ValidateTokenization("one two three", "one ;two three");
		}

		[TestMethod]
		public void ShouldModifyComment()
		{
			ValidateTokenization("; ", "; one");
		}

		[TestMethod]
		public void ShouldConvertUCharacterTokenFollowedByThreeNumbersIntoACharacterWhenAddingAFourthNumber()
		{
			ValidateTokenization("\\u123 ", "\\u1234 ");
		}

		[TestMethod]
		public void ShouldAllowForChangesToNotStartAtBeginningOfChangeData()
		{
			string beforeText = "(def asdf '(asdf asdf  asdf asdf))";
			string afterText = "(def asdf '(asdf asdf asdf asdf))";
			ValidateTokenization(beforeText, afterText, 0, -1, afterText.Length);
		}

		private void ValidateTokenization(string beforeText, string afterText)
		{
			int changePosition = 0;
			while (changePosition < beforeText.Length && changePosition < afterText.Length && beforeText[changePosition] == afterText[changePosition]) changePosition++;
			int delta = afterText.Length - beforeText.Length;
			int changeLength = Math.Abs(delta);

			ValidateTokenization(beforeText, afterText, changePosition, delta, changeLength);
		}

		private void ValidateTokenization(string beforeText, string afterText, int changePosition, int delta, int changeLength)
		{
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int)t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);
			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData> { new TextChangeData(changePosition, delta, changeLength) });
			AssertTokenListsAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		private static void AssertTokenListsAreEqual(LinkedList<Token> listOne, LinkedList<Token> listTwo)
		{
			LinkedListNode<Token> currentListOneNode = listOne.First;
			LinkedListNode<Token> currentListTwoNode = listTwo.First;

			while (currentListOneNode != null && currentListTwoNode != null)
			{
				AssertTokensAreEqual(currentListOneNode.Value, currentListTwoNode.Value);
				currentListOneNode = currentListOneNode.Next;
				currentListTwoNode = currentListTwoNode.Next;
			}

			Assert.AreEqual(currentListOneNode, currentListTwoNode);
		}

		private static void AssertTokensAreEqual(Token tokenOne, Token tokenTwo)
		{
			Assert.AreEqual(tokenOne.Length, tokenTwo.Length);
			Assert.AreEqual(tokenOne.Text, tokenTwo.Text);
			Assert.AreEqual(tokenOne.Type, tokenTwo.Type);
		}
	}
}