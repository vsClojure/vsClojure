namespace Microsoft.ClojureExtension.Utilities
{
	public static class StringExtensions
	{
		public static string Repeat(this string str, int count)
		{
			string indent = "";
			for (int i = 0; i < count; i++) indent += " ";
			return indent;
		}

		public static int Count(this string str, char characterToCount)
		{
			int count = 0;

			for (int i = 0; i < str.Length; i++)
				if (str[i] == characterToCount)
					count++;

			return count;
		}
	}
}