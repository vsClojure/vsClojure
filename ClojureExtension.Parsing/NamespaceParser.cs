using System;
using System.Collections.Generic;

namespace ClojureExtension.Parsing
{
	public class NamespaceParser
	{
		private readonly List<string> _namespaceSymbols;
		public static List<string> NamespaceSymbols = new List<string>() { "ns", "in-ns" };

		public NamespaceParser(List<string> namespaceSymbols)
		{
			_namespaceSymbols = namespaceSymbols;
		}

		public string Execute(LinkedList<Token> tokens)
		{
			LinkedListNode<Token> tokenNode = tokens.First;

			while (tokenNode != null)
			{
				Token token = tokenNode.Value;
				if (_namespaceSymbols.Contains(token.Text)) return FindFirstSymbolInList(tokenNode.Next);
				tokenNode = tokenNode.Next;
			}

			throw new Exception("Could not determine file namespace.");
		}

		private string FindFirstSymbolInList(LinkedListNode<Token> startingNode)
		{
			LinkedListNode<Token> currentTokenNode = startingNode;

			while (currentTokenNode != null)
			{
				Token currentToken = currentTokenNode.Value;
				if (currentToken.Type.IsBraceStart()) currentTokenNode = SkipDataStructure(currentTokenNode);
				else if (currentToken.Type.IsBraceEnd()) throw new Exception("Could not determine file namespace.");
				else if (currentToken.Type != TokenType.Symbol) currentTokenNode = currentTokenNode.Next;
				else return currentToken.Text;
			}

			throw new Exception("Could not determine file namespace.");
		}

		private LinkedListNode<Token> SkipDataStructure(LinkedListNode<Token> startingNode)
		{
			LinkedListNode<Token> currentTokenNode = startingNode.Next;
			TokenType endTokenType = startingNode.Value.Type.MatchingBraceType();
			int nestingDepth = 1;

			while (currentTokenNode != null && nestingDepth > 0)
			{
				if (currentTokenNode.Value.Type == startingNode.Value.Type) nestingDepth++;
				if (currentTokenNode.Value.Type == endTokenType) nestingDepth--;
				currentTokenNode = currentTokenNode.Next;
			}

			if (currentTokenNode != null) return currentTokenNode.Next;
			return null;
		}
	}
}