// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

namespace ClojureExtension.Utilities
{
	public interface IProvider<T>
	{
		T Get();
	}
}