namespace ClojureExtension.Utilities
{
	public interface IProvider<T>
	{
		T Get();
	}
}