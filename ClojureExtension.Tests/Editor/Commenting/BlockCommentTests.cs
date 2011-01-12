using System.Collections.Generic;
using ClojureExtension.Editor.Commenting;
using ClojureExtension.Editor.TextBuffer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ClojureExtension.Tests.Editor.Commenting
{
	[TestClass]
	public class BlockCommentTests
	{
		private List<string> _selectedLines;
		private ITextBufferAdapter _textBufferAdapter;
		private BlockComment _blockComment;

		[TestInitialize]
		public void Initialize()
		{
			_selectedLines = new List<string>();
			_textBufferAdapter = MockRepository.GenerateStub<ITextBufferAdapter>();
			_textBufferAdapter.Stub(t => t.GetSelectedLines()).Return(_selectedLines);
			_blockComment = new BlockComment(_textBufferAdapter);
		}

		[TestMethod]
		public void ShouldPutSemicolonInFrontOfEachLine()
		{
			_selectedLines.Add("line one");
			_selectedLines.Add("line two");
			_selectedLines.Add("line three");
			_blockComment.Execute();

			List<string> expectedResult = new List<string>();

			expectedResult.Add(";line one");
			expectedResult.Add(";line two");
			expectedResult.Add(";line three");

			_textBufferAdapter.AssertWasCalled(t => t.ReplaceSelectedLines(expectedResult));
		}

		[TestMethod]
		public void ShouldPutSemicolonOnBlankLines()
		{
			_selectedLines.Add("line one");
			_selectedLines.Add("");
			_selectedLines.Add("line three");
			_blockComment.Execute();

			List<string> expectedResult = new List<string>();

			expectedResult.Add(";line one");
			expectedResult.Add(";");
			expectedResult.Add(";line three");

			_textBufferAdapter.AssertWasCalled(t => t.ReplaceSelectedLines(expectedResult));
		}

		[TestMethod]
		public void ShouldPutSemicolonOnBlankLineIfItIsTheOnlyLine()
		{
			_selectedLines.Add("");
			_blockComment.Execute();

			List<string> expectedResult = new List<string>();

			expectedResult.Add(";");

			_textBufferAdapter.AssertWasCalled(t => t.ReplaceSelectedLines(expectedResult));
		}

		[TestMethod]
		public void ShouldDoNothingWhenNoLinesSelected()
		{
			_blockComment.Execute();
			List<string> expectedResult = new List<string>();
			_textBufferAdapter.AssertWasCalled(t => t.ReplaceSelectedLines(expectedResult));
		}
	}
}