// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.Collections.Generic;

namespace ClojureExtension.Parsing
{
	public static class TokenTypeExtensions
	{
		private static readonly Dictionary<TokenType, TokenType> BraceMatchingMap = new Dictionary<TokenType, TokenType>()
		{
			{TokenType.ListStart, TokenType.ListEnd},
			{TokenType.ListEnd, TokenType.ListStart},
			{TokenType.MapStart, TokenType.MapEnd},
			{TokenType.MapEnd, TokenType.MapStart},
			{TokenType.VectorStart, TokenType.VectorEnd},
			{TokenType.VectorEnd, TokenType.VectorStart}
		};

		private static readonly List<TokenType> BraceBeginTypes = new List<TokenType>()
		{
			TokenType.ListStart, TokenType.VectorStart, TokenType.MapStart
		};

		public static bool IsBraceEnd(this TokenType type)
		{
			return BraceMatchingMap.ContainsKey(type) && !BraceBeginTypes.Contains(type);
		}

		public static bool IsBraceStart(this TokenType type)
		{
			return BraceBeginTypes.Contains(type);
		}

		public static bool IsBrace(this TokenType type)
		{
			return BraceMatchingMap.ContainsKey(type);
		}

		public static TokenType MatchingBraceType(this TokenType type)
		{
			if (BraceMatchingMap.ContainsKey(type))
				return BraceMatchingMap[type];

			return type;
		}
	}
}
