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
	}
}