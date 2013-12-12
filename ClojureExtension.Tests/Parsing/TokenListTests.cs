using System.Collections.Generic;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureExtension.Tests.Parsing
{
	[TestClass]
	public class TokenListTests
	{
		[TestMethod]
		public void ShouldReturnTokensThatIntersectThroughTheCenterOfTargetRange()
		{
			Entity<LinkedList<Token>> tokenizedBuffer = new Entity<LinkedList<Token>>();
			tokenizedBuffer.CurrentState = new LinkedList<Token>();
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym1", 0, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym2", 4, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym3", 8, 4));

			LinkedList<IndexToken> intersectingTokens = tokenizedBuffer.CurrentState.Intersection(5, 7);

			Assert.AreEqual(2, intersectingTokens.Count);
			Assert.AreEqual("sym2", intersectingTokens.First.Value.Token.Text);
			Assert.AreEqual("sym3", intersectingTokens.Last.Value.Token.Text);
		}

		[TestMethod]
		public void ShouldReturnTokensThatIntersectAtEndOfTargetRange()
		{
			Entity<LinkedList<Token>> tokenizedBuffer = new Entity<LinkedList<Token>>();
			tokenizedBuffer.CurrentState = new LinkedList<Token>();
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym1", 0, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym2", 4, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym3", 8, 4));

			LinkedList<IndexToken> intersectingTokens = tokenizedBuffer.CurrentState.Intersection(3, 1);

			Assert.AreEqual(1, intersectingTokens.Count);
			Assert.AreEqual("sym1", intersectingTokens.First.Value.Token.Text);
		}

		[TestMethod]
		public void ShouldReturnTokensThatIntersectAtStartOfTargetRange()
		{
			Entity<LinkedList<Token>> tokenizedBuffer = new Entity<LinkedList<Token>>();
			tokenizedBuffer.CurrentState = new LinkedList<Token>();
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym1", 0, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym2", 4, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym3", 8, 4));

			LinkedList<IndexToken> intersectingTokens = tokenizedBuffer.CurrentState.Intersection(4, 1);

			Assert.AreEqual(1, intersectingTokens.Count);
			Assert.AreEqual("sym2", intersectingTokens.First.Value.Token.Text);
		}

		[TestMethod]
		public void ShouldReturnTokensThatIntersectAtMiddleOfTargetRange()
		{
			Entity<LinkedList<Token>> tokenizedBuffer = new Entity<LinkedList<Token>>();
			tokenizedBuffer.CurrentState = new LinkedList<Token>();
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym1", 0, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym2", 4, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym3", 8, 4));

			LinkedList<IndexToken> intersectingTokens = tokenizedBuffer.CurrentState.Intersection(10, 1);

			Assert.AreEqual(1, intersectingTokens.Count);
			Assert.AreEqual("sym3", intersectingTokens.First.Value.Token.Text);
		}

		[TestMethod]
		public void ShouldReturnTokensThatIntersectAtStartAtOneTokenAndEndInMiddleOfAnother()
		{
			Entity<LinkedList<Token>> tokenizedBuffer = new Entity<LinkedList<Token>>();
			tokenizedBuffer.CurrentState = new LinkedList<Token>();
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym1", 0, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym2", 4, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym3", 8, 4));

			LinkedList<IndexToken> intersectingTokens = tokenizedBuffer.CurrentState.Intersection(4, 8);

			Assert.AreEqual(2, intersectingTokens.Count);
			Assert.AreEqual("sym2", intersectingTokens.First.Value.Token.Text);
			Assert.AreEqual("sym3", intersectingTokens.Last.Value.Token.Text);
		}

		[TestMethod]
		public void ShouldReturnTokensThatIntersectWhenIntersectingLengthExtendsBeyondTokenList()
		{
			Entity<LinkedList<Token>> tokenizedBuffer = new Entity<LinkedList<Token>>();
			tokenizedBuffer.CurrentState = new LinkedList<Token>();
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym1", 0, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym2", 4, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym3", 8, 4));

			LinkedList<IndexToken> intersectingTokens = tokenizedBuffer.CurrentState.Intersection(4, 100);

			Assert.AreEqual(2, intersectingTokens.Count);
			Assert.AreEqual("sym2", intersectingTokens.First.Value.Token.Text);
			Assert.AreEqual("sym3", intersectingTokens.Last.Value.Token.Text);
		}

		[TestMethod]
		public void ShouldReturnNoTokensWhenIntersectingLengthIsZero()
		{
			Entity<LinkedList<Token>> tokenizedBuffer = new Entity<LinkedList<Token>>();
			tokenizedBuffer.CurrentState = new LinkedList<Token>();
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym1", 0, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym2", 4, 4));
			tokenizedBuffer.CurrentState.AddLast(new Token(TokenType.Symbol, "sym3", 8, 4));

			LinkedList<IndexToken> intersectingTokens = tokenizedBuffer.CurrentState.Intersection(4, 0);

			Assert.AreEqual(0, intersectingTokens.Count);
		}
	}
}