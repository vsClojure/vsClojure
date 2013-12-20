// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

namespace Microsoft.ClojureExtension.Editor.Options
{
	public class EditorOptions
	{
		private readonly int _indentSize;

		public EditorOptions(int indentSize)
		{
			_indentSize = indentSize;
		}

		public int IndentSize
		{
			get { return _indentSize; }
		}
	}
}