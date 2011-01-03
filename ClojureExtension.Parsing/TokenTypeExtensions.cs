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
