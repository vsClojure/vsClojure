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
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare asym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("sym1"), 1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldReplaceTokenThatHasBeenModifiedByAddingOneLetterAtTheEnd()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare sym1a)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("sym1"), 1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldReplaceTokenThatHasBeenModifiedByAddingOneLetterAtToTheRightOfATokenOfLengthOne()
		{
			string beforeText = "(declare z sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare ze sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf(" s"), 1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldReplaceTokenThatHasBeenModifiedByAddingOneLetterAtToTheLeftOfATokenOfLengthOne()
		{
			string beforeText = "(declare z sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare ez sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("z"), 1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldInsertNewTokenThatHasBeenAddedInWhitespace()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare asdf sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf(" "), 5)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldModifyTokenThatHasHadFirstCharacterDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare ym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("sym1"), -1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldModifyTokenThatHasHadLastCharacterDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare sym)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("sym1"), -1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldModifyTokenThatHasHadMiddleCharactersDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare s1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("ym1"), -2)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldCombineTokensThatHaveHadWhitespaceInBetweenDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declaresym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf(" "), -1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldRemoveTokensThatHaveBeenDeleted()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf(" "), -5)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldModifyAllFollowingTokensWhenDoubleQuoteAdded()
		{
			string beforeText = "(declare sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(declare \"sym1)\r\n(declare sym2)\r\n(println sym1 \"asdf\")\r\n(println \"fdsadf\")";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("sym1"), 1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldModifyAllFollowingTokensWhenDoubleQuoteAdded_RealLifeExample()
		{
			string beforeText = "(def unquote) (def unquote-splicing) (def ^{:arglists '([& items]) :doc \"Creates a new list containing the items.\" :added \"1.0\"} list (. clojure.lang.PersistentList creator))";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(def \"unquote) (def unquote-splicing) (def ^{:arglists '([& items]) :doc \"Creates a new list containing the items.\" :added \"1.0\"} list (. clojure.lang.PersistentList creator))";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("unquote"), 1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldModifyComment()
		{
			string beforeText = "; ";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "; asdf";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(2, 4)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldModifyAllFollowingTokensWhenDoubleQuoteRemoved()
		{
			string beforeText = "(def \"un \"C\" :add \"1.0\"} list";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(def un \"C\" :add \"1.0\"} list";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("\"un"), -1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldRemoveFirstTokenWhenOneCharacterLongTokenDeletedFromStart()
		{
			string beforeText = "(def";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "def";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int) t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() {new TextChangeData(beforeText.IndexOf("("), -1)});
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldAddTokensWhenNoTokensExist()
		{
			string beforeText = "";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "def asdf asdf";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int)t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() { new TextChangeData(0, 1) });
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldRemoveAllTokensWhenChangeDeletesEverything()
		{
			string beforeText = "def asdf asdf";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() { new TextChangeData(0, -beforeText.Length) });
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldConvertUCharacterTokenFollowedByThreeNumbersIntoACharacterWhenAddingAFourthNumber()
		{
			_tokenizedBufferEntity.CurrentState = new LinkedList<Token>();
			_tokenizedBufferEntity.CurrentState.AddLast(new Token(TokenType.Character, "\\u", 0, 2));
			_tokenizedBufferEntity.CurrentState.AddLast(new Token(TokenType.Number, "123", 2, 3));
			_tokenizedBufferEntity.CurrentState.AddLast(new Token(TokenType.Whitespace, " ", 5, 1));

			string afterText = "\\u1234 ";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int)t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() { new TextChangeData(5, 1) });
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		[TestMethod]
		public void ShouldAllowForChangesToNotStartAtBeginningOfChangePosition()
		{
			string beforeText = "(def asdf '(asdf asdf  asdf asdf))";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(beforeText);

			string afterText = "(def asdf '(asdf asdf asdf asdf))";
			_textBuffer.Stub(t => t.GetText(0)).IgnoreArguments().WhenCalled(t => t.ReturnValue = afterText.Substring((int)t.Arguments[0])).Return("");
			_textBuffer.Stub(t => t.Length).Return(afterText.Length);

			_bufferTextChangeHandler.OnTextChanged(new List<TextChangeData>() { new TextChangeData(0, -1, afterText.Length) });
			AssertAreEqual(_tokenizedBufferEntity.CurrentState, _tokenizer.Tokenize(afterText));
		}

		private void AssertAreEqual(LinkedList<Token> listOne, LinkedList<Token> listTwo)
		{
			LinkedListNode<Token> currentListOneNode = listOne.First;
			LinkedListNode<Token> currentListTwoNode = listTwo.First;

			while (currentListOneNode != null && currentListTwoNode != null)
			{
				AreEqual(currentListOneNode.Value, currentListTwoNode.Value);
				currentListOneNode = currentListOneNode.Next;
				currentListTwoNode = currentListTwoNode.Next;
			}

			Assert.AreEqual(currentListOneNode, currentListTwoNode);
		}

		private void AreEqual(Token tokenOne, Token tokenTwo)
		{
			Assert.AreEqual(tokenOne.Length, tokenTwo.Length);
			Assert.AreEqual(tokenOne.Text, tokenTwo.Text);
			Assert.AreEqual(tokenOne.Type, tokenTwo.Type);
		}
	}
}