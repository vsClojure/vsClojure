namespace ClojureExtension.Editor.InputHandling
{
	public interface ITextBufferAdapter
	{
		string GetText(int startPosition);
		int Length { get; }
		void SetText(string text);
	}
}