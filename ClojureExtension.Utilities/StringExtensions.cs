// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

namespace ClojureExtension.Utilities
{
	public static class StringExtensions
	{
		public static string Repeat(this string str, int count)
		{
			var indent = "";
			for (var i = 0; i < count; i++) indent += " ";
			return indent;
		}

		public static int Count(this string str, char characterToCount)
		{
			var count = 0;

			for (var i = 0; i < str.Length; i++)
				if (str[i] == characterToCount)
					count++;

			return count;
		}
	}
}