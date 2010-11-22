using System.Collections.Generic;
using Microsoft.ClojureExtension.Editor.Parsing;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Editor.BraceMatching
{
	public class MatchingBraceFinder
	{
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;

		public MatchingBraceFinder(Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_tokenizedBuffer = tokenizedBuffer;
		}

		public MatchingBracePair FindMatchingBraces(int caretPosition)
		{
			IndexTokenNode tokenNode = _tokenizedBuffer.CurrentState.FindTokenAtIndex(caretPosition);

			if (!tokenNode.IndexToken.Token.Type.IsBraceStart()) tokenNode = tokenNode.Previous();

			if (tokenNode.IndexToken.Token.Type.IsBrace())
			{
				IndexToken matchingBrace = FindMatchingBrace(tokenNode);
				if (matchingBrace == null) return new MatchingBracePair(tokenNode.IndexToken, null);
				return new MatchingBracePair(tokenNode.IndexToken, matchingBrace);
			}

			return new MatchingBracePair(null, null);
		}

		private static IndexToken FindMatchingBrace(IndexTokenNode tokenNode)
		{
			TokenType matchingType = tokenNode.IndexToken.Token.Type.MatchingBraceType();
			IndexTokenNode candidateToken = tokenNode;
			bool matchForward = tokenNode.IndexToken.Token.Type.IsBraceStart();
			int braceCounter = 0;

			while (candidateToken != null)
			{
				if (candidateToken.IndexToken.Token.Type == matchingType) braceCounter--;
				if (candidateToken.IndexToken.Token.Type == tokenNode.IndexToken.Token.Type) braceCounter++;
				if (braceCounter == 0) return candidateToken.IndexToken;
				candidateToken = matchForward ? candidateToken.Next() : candidateToken.Previous();
			}

			return null;
		}
	}
}