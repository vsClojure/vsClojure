using System;
using System.Collections.Generic;
using ClojureExtension.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureExtension.Tests.Parsing
{
	[TestClass]
	public class NamespaceParserTests
	{
		private Tokenizer _tokenizer;
		private NamespaceParser _parser;

		[TestInitialize]
		public void Initialize()
		{
			_tokenizer = new Tokenizer();
			_parser = new NamespaceParser(new List<string>() {"ns"});
		}

		[TestMethod]
		[ExpectedException(typeof (Exception))]
		public void ShouldThrowExceptionWhenNoTokensExist()
		{
			_parser.Execute(_tokenizer.Tokenize(""));
		}

		[TestMethod]
		[ExpectedException(typeof (Exception))]
		public void ShouldThrowExceptionWhenNamespaceSymbolNotFound()
		{
			_parser.Execute(_tokenizer.Tokenize("(asdf)"));
		}

		[TestMethod]
		[ExpectedException(typeof (Exception))]
		public void ShouldThrowExceptionWhenNamespaceSymbolFoundButNoNamespace()
		{
			_parser.Execute(_tokenizer.Tokenize("(ns)"));
		}

		[TestMethod]
		public void ShouldReturnNameOfNamespaceWhenItImmediatellyFollowsTheNamespaceSymbol()
		{
			Assert.AreEqual("test", _parser.Execute(_tokenizer.Tokenize("(ns test)")));
		}

		[TestMethod]
		public void ShouldReturnNameOfNamespaceWhenItIsNotImmediatelyFollowingTheNamespaceSymbol()
		{
			Assert.AreEqual("test", _parser.Execute(_tokenizer.Tokenize("(ns ^{:asdf \"asdf\"} test)")));
		}

		[TestMethod]
		public void ShouldReturnNameOfNamespaceWhenANestedDataStructureIsBeforeNamespace()
		{
			Assert.AreEqual("test", _parser.Execute(_tokenizer.Tokenize("(ns ^{:asdf \"asdf\" :asdf2 {1 2}} test)")));
		}

		[TestMethod]
		[ExpectedException(typeof (Exception))]
		public void ShouldThrowExceptionWhenNestedDataStructureTerminatesUnexpectedly()
		{
			Assert.AreEqual("test", _parser.Execute(_tokenizer.Tokenize("(ns ^{:asdf \"asdf\"")));
		}
	}
}