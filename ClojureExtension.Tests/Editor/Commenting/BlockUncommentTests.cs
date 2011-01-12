using System.Collections.Generic;
using ClojureExtension.Editor.Commenting;
using ClojureExtension.Editor.TextBuffer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ClojureExtension.Tests.Editor.Commenting
{
	[TestClass]
	public class BlockUncommentTests
	{
		private List<string> _selectedLines;
		private ITextBufferAdapter _textBufferAdapter;
		private BlockUncomment _blockUncomment;

		[TestInitialize]
		public void Initialize()
		{
			_selectedLines = new List<string>();
			_textBufferAdapter = MockRepository.GenerateStub<ITextBufferAdapter>();
			_textBufferAdapter.Stub(t => t.GetSelectedLines()).Return(_selectedLines);
			_blockUncomment = new BlockUncomment(_textBufferAdapter);
		}

		[TestMethod]
		public void ShouldRemoveSemicolonFromEachLine()
		{
			_selectedLines.Add(";line one");
			_selectedLines.Add(";line two");
			_selectedLines.Add(";line three");
			_blockUncomment.Execute();

			List<string> expectedResult = new List<string>();

			expectedResult.Add("line one");
			expectedResult.Add("line two");
			expectedResult.Add("line three");

			_textBufferAdapter.AssertWasCalled(t => t.ReplaceSelectedLines(expectedResult));
		}

		[TestMethod]
		public void ShouldRemoveSemicolonThatHasLeadingWhitespace()
		{
			_selectedLines.Add("    ;   line two");
			_blockUncomment.Execute();

			List<string> expectedResult = new List<string>();
			expectedResult.Add("       line two");
			_textBufferAdapter.AssertWasCalled(t => t.ReplaceSelectedLines(expectedResult));
		}

		[TestMethod]
		public void ShouldRemoveOnlyOneSemicolonIfMoreThanOneIsPresent()
		{
			_selectedLines.Add(";;line two");
			_blockUncomment.Execute();

			List<string> expectedResult = new List<string>();
			expectedResult.Add(";line two");
			_textBufferAdapter.AssertWasCalled(t => t.ReplaceSelectedLines(expectedResult));
		}

		[TestMethod]
		public void ShouldDoNothingToALineThatDoesNotBeginWithASemicolon()
		{
			_selectedLines.Add("line two");
			_blockUncomment.Execute();

			List<string> expectedResult = new List<string>();
			expectedResult.Add("line two");
			_textBufferAdapter.AssertWasCalled(t => t.ReplaceSelectedLines(expectedResult));
		}

		[TestMethod]
		public void ShouldDoNothingToALineThatEndsWithAComment()
		{
			_selectedLines.Add("line two ;asdf");
			_blockUncomment.Execute();

			List<string> expectedResult = new List<string>();
			expectedResult.Add("line two ;asdf");
			_textBufferAdapter.AssertWasCalled(t => t.ReplaceSelectedLines(expectedResult));
		}
	}
}