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