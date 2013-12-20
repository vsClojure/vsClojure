// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System.Collections.Generic;

namespace ClojureExtension.Editor.TextBuffer
{
	public interface ITextBufferAdapter
	{
		string GetText(int startPosition);
		int Length { get; }
		void SetText(string text);
		List<string> GetSelectedLines();
		void ReplaceSelectedLines(List<string> newLines);
	}
}