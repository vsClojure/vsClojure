using System.IO;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureExtension.Tests.Editor.Parsing
{
	[TestClass]
	public class LexerTests
	{
		[TestMethod]
		public void Next_ShouldReturnNullWhenAtEndOfStream()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("")));
			Assert.IsNull(lexer.Next());
		}

		[TestMethod]
		public void Next_ShouldReturnNumberTokenTypeWhenInputIsNumber()
		{
			var stream = new PushBackCharacterStream(new StringReader("123"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("123", token.Text);
			Assert.IsFalse(stream.HasMore);
		}

		[TestMethod]
		public void Next_ShouldReturnNumberTokenTypeWhenInputIsInvalidNumber()
		{
			var stream = new PushBackCharacterStream(new StringReader("123asdf"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("123asdf", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnListStartTokenTypeWhenInputIsAnOpenParen()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("(")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.ListStart, token.Type);
			Assert.AreEqual("(", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnListEndTokenTypeWhenInputIsAClosedParen()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader(")")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.ListEnd, token.Type);
			Assert.AreEqual(")", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnVectorStartTokenTypeWhenInputIsAnOpenBracket()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("[")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.VectorStart, token.Type);
			Assert.AreEqual("[", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnVectorEndTokenTypeWhenInputIsAClosedBracket()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("]")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.VectorEnd, token.Type);
			Assert.AreEqual("]", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnMapStartTokenTypeWhenInputIsAnOpenCurlyBrace()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("{")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.MapStart, token.Type);
			Assert.AreEqual("{", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnMapEndTokenTypeWhenInputIsAClosedCurlyBrace()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("}")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.MapEnd, token.Type);
			Assert.AreEqual("}", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnStringForProperlyTerminatingString()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\"asdf\"")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("\"asdf\"", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnStringForRunOnString()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\"asdfasdf")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("\"asdfasdf", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnStringThatDoesNotTerminateOnBackslashQuote()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\"asdf\\\"asdf\"")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("\"asdf\\\"asdf\"", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnWhitespaceForTabsSpacesCommasAndReturnCharacters()
		{
			string input = "  \t \r\n , ";
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader(input)));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Whitespace, token.Type);
			Assert.AreEqual(input, token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnNumberFollowByWhitespaceAndAString()
		{
			var stream = new PushBackCharacterStream(new StringReader("123 \"asdf\""));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("123", token.Text);
			Assert.AreEqual(0, token.StartIndex);

			token = lexer.Next();
			Assert.AreEqual(TokenType.Whitespace, token.Type);
			Assert.AreEqual(" ", token.Text);
			Assert.AreEqual(3, token.StartIndex);

			token = lexer.Next();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("\"asdf\"", token.Text);
			Assert.AreEqual(4, token.StartIndex);
		}

		[TestMethod]
		public void Next_ShouldReturnRealNumber()
		{
			var stream = new PushBackCharacterStream(new StringReader("123.321"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("123.321", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnHexNumber()
		{
			var stream = new PushBackCharacterStream(new StringReader("0x123A"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.HexNumber, token.Type);
			Assert.AreEqual("0x123A", token.Text);
			Assert.IsFalse(stream.HasMore);
		}

		[TestMethod]
		public void Next_ShouldReturnCommentWithTrailingWhitespace()
		{
			var stream = new PushBackCharacterStream(new StringReader("; test text  \r\n"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Comment, token.Type);
			Assert.AreEqual("; test text  ", token.Text);

			token = lexer.Next();
			Assert.AreEqual(TokenType.Whitespace, token.Type);
			Assert.AreEqual("\r\n", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnCommentThatExtendsToEndOfInput()
		{
			var stream = new PushBackCharacterStream(new StringReader("; test"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Comment, token.Type);
			Assert.AreEqual("; test", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnCommentToEndOfLineOnly()
		{
			var stream = new PushBackCharacterStream(new StringReader("; test\r\n123"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Comment, token.Type);
			Assert.AreEqual("; test", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnSymbol()
		{
			var stream = new PushBackCharacterStream(new StringReader("test"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("test", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnSymbolWhenItHasADot()
		{
			var stream = new PushBackCharacterStream(new StringReader("namespace.test"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("namespace.test", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnSymbolImmediatelyFollowedByComment()
		{
			var stream = new PushBackCharacterStream(new StringReader("test;comment"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("test", token.Text);

			token = lexer.Next();
			Assert.AreEqual(TokenType.Comment, token.Type);
			Assert.AreEqual(";comment", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnTwoSymbolsSeparatedByWhitespace()
		{
			var stream = new PushBackCharacterStream(new StringReader("symbol1 symbol2"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("symbol1", token.Text);

			token = lexer.Next();
			Assert.AreEqual(TokenType.Whitespace, token.Type);

			token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("symbol2", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnKeyword()
		{
			var stream = new PushBackCharacterStream(new StringReader(":asdf"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Keyword, token.Type);
			Assert.AreEqual(":asdf", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnKeywordWithNoName()
		{
			var stream = new PushBackCharacterStream(new StringReader(":"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Keyword, token.Type);
			Assert.AreEqual(":", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnKeywordFollowByListStart()
		{
			var stream = new PushBackCharacterStream(new StringReader(":asdf("));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Keyword, token.Type);
			Assert.AreEqual(":asdf", token.Text);
			Assert.AreEqual(TokenType.ListStart, lexer.Next().Type);
		}

		[TestMethod]
		public void Next_ShouldReturnBooleanWhenTrueIsInput()
		{
			var stream = new PushBackCharacterStream(new StringReader("true"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Boolean, token.Type);
			Assert.AreEqual("true", token.Text);
			Assert.IsFalse(stream.HasMore);
		}

		[TestMethod]
		public void Next_ShouldReturnBooleanWhenFalseIsInput()
		{
			var stream = new PushBackCharacterStream(new StringReader("false"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Boolean, token.Type);
			Assert.AreEqual("false", token.Text);
			Assert.IsFalse(stream.HasMore);
		}

		[TestMethod]
		public void Next_ShouldReturnNil()
		{
			var stream = new PushBackCharacterStream(new StringReader("nil"));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Nil, token.Type);
			Assert.AreEqual("nil", token.Text);
			Assert.IsFalse(stream.HasMore);
		}

		[TestMethod]
		public void Next_ShouldStopParsingSymbolWhenDoubleQuoteFound()
		{
			var stream = new PushBackCharacterStream(new StringReader("asdf\"str\""));
			Lexer lexer = new Lexer(stream);
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("asdf", token.Text);

			token = lexer.Next();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("\"str\"", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReadBackslashNewLineAsCharacter()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\\newline")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\newline", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReadBackslashTabAsCharacter()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\\tab")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\tab", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReadBackslashSpaceAsCharacter()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\\space")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\space", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReadBackslashUFollowedByFourHexDigitsAsCharacter()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\\uF04A")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\uF04A", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReadBackslashUAsChar()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\\u")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\u", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReadBackslashAAsChar()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\\a")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\a", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReadBackslashABackSlashFAsTwoCharacters()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\\a\\f")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\a", token.Text);
			token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\f", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReadBackslashUFollowedByTwoHexDigitsAsSingleUCharacterFollowedByANumber()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\\u19")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\u", token.Text);
			token = lexer.Next();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("19", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReadBackslashUFollowedByThreeHexDigitsAndAZAsSingleUCharacterFollowedByASymbol()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("\\uAF9Z")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\u", token.Text);
			token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("AF9Z", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnSymbolFollowedByCharacter()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("asdf\\s")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("asdf", token.Text);
			token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\s", token.Text);
		}

		[TestMethod]
		public void Next_ShouldReturnNumberFollowedByCharacter()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("123\\s")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("123", token.Text);
			token = lexer.Next();
			Assert.AreEqual(TokenType.Character, token.Type);
			Assert.AreEqual("\\s", token.Text);
		}

		[TestMethod]
		public void ShouldReturnSymbolStartingWithAmpersand()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("&123asdf")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("&123asdf", token.Text);
		}

		[TestMethod]
		public void ShouldReturnSymbolWithOnlyASingleAmpersand()
		{
			Lexer lexer = new Lexer(new PushBackCharacterStream(new StringReader("&")));
			Token token = lexer.Next();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("&", token.Text);
		}
	}
}
