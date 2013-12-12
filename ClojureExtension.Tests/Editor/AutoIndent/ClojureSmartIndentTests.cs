using System.Collections.Generic;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.ClojureExtension.Editor.AutoIndent;
using Microsoft.ClojureExtension.Editor.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureExtension.Tests.Editor.AutoIndent
{
	[TestClass]
	public class ClojureSmartIndentTests
	{
		private Tokenizer _tokenizer;
		private Entity<LinkedList<Token>> _tokenizedBufferEntity;
		private ClojureSmartIndent _clojureSmartIndent;
		private EditorOptions _defaultOptions;

		[TestInitialize]
		public void Initialize()
		{
			_tokenizer = new Tokenizer();
			_tokenizedBufferEntity = new Entity<LinkedList<Token>>();
			_clojureSmartIndent = new ClojureSmartIndent(_tokenizedBufferEntity);
			_defaultOptions = new EditorOptions(4);
		}

		[TestMethod]
		public void ShouldNotIndentEmptyBuffer()
		{
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize("");
			Assert.AreEqual(0, _clojureSmartIndent.GetDesiredIndentation(0, _defaultOptions));
		}

		[TestMethod]
		public void ShouldIndentOpenListWithNoFollowingTokensByIndentAmount()
		{
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize("(\r\n");
			Assert.AreEqual(4, _clojureSmartIndent.GetDesiredIndentation(3, _defaultOptions));
		}

		[TestMethod]
		public void ShouldIndentOpenListWithFollowingTokensByIndentAmount()
		{
			string input = "(asdf asdf 123\r\n";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(input);
			Assert.AreEqual(4, _clojureSmartIndent.GetDesiredIndentation(input.IndexOf("\n") + 1, _defaultOptions));
		}

		[TestMethod]
		public void ShouldIndentOpenListInsideAnotherListByTheIndentAmountPlusOne()
		{
			string input = "((\r\n";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(input);
			Assert.AreEqual(5, _clojureSmartIndent.GetDesiredIndentation(input.IndexOf("\n") + 1, _defaultOptions));
		}

		[TestMethod]
		public void ShouldIndentByIndentAmountWhenListContainsMultipleElements()
		{
			string input = "(asdf asdf\r\n";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(input);
			Assert.AreEqual(4, _clojureSmartIndent.GetDesiredIndentation(input.IndexOf("\n") + 1, _defaultOptions));
		}

		[TestMethod]
		public void ShouldIndentByIndentAmountWhenListContainsMultipleAndHasIndentElements()
		{
			string input = "(asdf (fdas\r\n))";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(input);
			Assert.AreEqual(input.LastIndexOf("(") + 4, _clojureSmartIndent.GetDesiredIndentation(input.IndexOf("\n") + 1, _defaultOptions));
		}

		[TestMethod]
		public void ShouldIndentByOneAfterTheOpeningBraceForVectors()
		{
			string input = "(asdf [\r\n]";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(input);
			Assert.AreEqual(input.LastIndexOf("[") + 1, _clojureSmartIndent.GetDesiredIndentation(input.IndexOf("\n") + 1, _defaultOptions));
		}

		[TestMethod]
		public void DropsExistingLineDownWhileMaintainingIndentAndIndentsTheCorrectAmountForNewLine()
		{
			string input = "(ns program (:gen-class))\n\n(defn -main [& args] (println \"Hello world\"))";
			_tokenizedBufferEntity.CurrentState = _tokenizer.Tokenize(input);
			Assert.AreEqual(4, _clojureSmartIndent.GetDesiredIndentation(input.IndexOf("(println") - 1, _defaultOptions));
		}
	}
}
