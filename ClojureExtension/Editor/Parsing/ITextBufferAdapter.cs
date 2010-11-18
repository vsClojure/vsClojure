namespace Microsoft.ClojureExtension.Editor.Parsing
{
	public interface ITextBufferAdapter
	{
		string GetText(int startPosition);
		int Length { get; }
	}
}