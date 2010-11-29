using System;
using System.Collections.Generic;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;
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
		public void ShouldReplaceTokenThatHasBeenModifiedByAddingOneLetterAtStart()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare asym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldReplaceTokenThatHasBeenModifiedByAddingOneLetterAtTheEnd()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare sym1a)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldReplaceTokenThatHasBeenModifiedByAddingOneLetterAtToTheRightOfATokenOfLengthOne()
		{
			string beforeText = "(declare z sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare ze sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldReplaceTokenThatHasBeenModifiedByAddingOneLetterAtToTheLeftOfATokenOfLengthOne()
		{
			string beforeText = "(declare z sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare ez sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldInsertNewTokenThatHasBeenAddedInWhitespace()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare asdf sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldModifyTokenThatHasHadFirstCharacterDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare ym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldModifyTokenThatHasHadLastCharacterDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare sym)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldModifyTokenThatHasHadMiddleCharactersDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare s1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldCombineTokensThatHaveHadWhitespaceInBetweenDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declaresym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldRemoveTokensThatHaveBeenDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldModifyAllFollowingTokensWhenDoubleQuoteAdded()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			string afterText = "(declare \"sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldModifyAllFollowingTokensWhenDoubleQuoteAdded_RealLifeExample()
		{
			string beforeText = "(def unquote) (def unquote-splicing) (def ^{:arglists '([& items]) :doc \"Creates a new list containing the items.\" :added \"1.0\"} list (. clojure.lang.PersistentList creator))";
			string afterText = "(def \"unquote) (def unquote-splicing) (def ^{:arglists '([& items]) :doc \"Creates a new list containing the items.\" :added \"1.0\"} list (. clojure.lang.PersistentList creator))";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldModifyComment()
		{
			string beforeText = "; ";
			string afterText = "; asdf";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldModifyAllFollowingTokensWhenDoubleQuoteRemoved()
		{
			string beforeText = "(def \"un \"C\" :add \"1.0\"} list";
			string afterText = "(def un \"C\" :add \"1.0\"} list";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldRemoveFirstTokenWhenOneCharacterLongTokenDeletedFromStart()
		{
			string beforeText = "(def";
			string afterText = "def";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldAddTokensWhenNoTokensExist()
		{
			string beforeText = "";
			string afterText = "def asdf asdf";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldRemoveAllTokensWhenChangeDeletesEverything()
		{
			string beforeText = "def asdf asdf";
			string afterText = "";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldConvertUCharacterTokenFollowedByThreeNumbersIntoACharacterWhenAddingAFourthNumber()
		{
			string beforeText = "\\u123 ";
			string afterText = "\\u1234 ";
			ValidateTokenization(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldAllowForChangesToNotStartAtBeginningOfChangePosition()
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