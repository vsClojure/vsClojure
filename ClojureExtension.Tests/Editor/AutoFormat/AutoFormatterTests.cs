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
		private string _textBufferState;

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
			_textBuffer.Stub(b => b.SetText(null)).IgnoreArguments().WhenCalled((callData) => _textBufferState = (string) callData.Arguments[0]);
		}

		private void ValidateFormatting(string beforeText, string afterText)
		{
			CreateTokensAndTextBuffer(beforeText);
			_formatter.Format();
			Assert.AreEqual(afterText, _textBufferState);
		}

		[TestMethod]
		public void ShouldNotChangeProperlyFormattedCode()
		{
			string beforeText = "(println)";
			string afterText = "(println)";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldRemoveTrailingWhitespace()
		{
			string beforeText = "(    \r\n\r\n";
			string afterText = "(";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldRemoveSpaceAfterListOpen()
		{
			string beforeText = "(    println)";
			string afterText = "(println)";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldRemoveExtraLineBreaksAndInsertIndentAfterListOpen()
		{
			string beforeText = "(\r\n\r\nprintln)";
			string afterText = "(\r\n    println)";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldRemoveExtraLineBreaksAndRemoveExtraIndentAfterListOpen()
		{
			string beforeText = "(\r\n\r\n            println)";
			string afterText = "(\r\n    println)";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldIndentMultilineLists()
		{
			string beforeText = "(\r\n\r\nprintln \r\n\r\nprintln\r\n\r\nprintln)";
			string afterText = "(\r\n    println\r\n    println\r\n    println)";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldSeperateElementsBySingleSpaceIfTheyContainNoLineBreaks()
		{
			string beforeText = "println    println";
			string afterText = "println println";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldNotPutSpaceBetweenListStartAndFirstElement()
		{
			string beforeText = "(   println";
			string afterText = "(println";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldNotPutSpaceBetweenListStartAndFirstElementThatIsAnotherListStart()
		{
			string beforeText = "(   (";
			string afterText = "((";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldIndentNoneListDataStructuresOneSpace()
		{
			string beforeText = "[\r\n1";
			string afterText = "[\r\n 1";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldNotDecreaseIndentWhenEncounteringEndBraceWithoutStart()
		{
			string beforeText = "(println ]\r\n\r\n1";
			string afterText = "(println]\r\n    1";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldRemoveWhitespaceAtStartOfText()
		{
			string beforeText = "\r\n\r\n   println";
			string afterText = "println";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldNotPutSpaceBeforeEndBrace()
		{
			string beforeText = "(1 2 )";
			string afterText = "(1 2)";
			ValidateFormatting(beforeText, afterText);
		}

		[TestMethod]
		public void ShouldSpaceTopLevelExpressionWithOneEmptyLine()
		{
			string beforeText = "()  \r\n\r\n\r\n()";
			string afterText = "()\r\n\r\n()";
			ValidateFormatting(beforeText, afterText);
		}

		// Unrelated Note: Got an error message when I tried to edit at the end of a file.
	}
}