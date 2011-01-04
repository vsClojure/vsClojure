using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace ClojureExtension.Editor.InputHandling
{
	public class TextChangeAdapter
	{
		private readonly BufferTextChangeHandler _bufferTextChangeHandler;

		public TextChangeAdapter(BufferTextChangeHandler bufferTextChangeHandler)
		{
			_bufferTextChangeHandler = bufferTextChangeHandler;
		}

		public void OnTextChange(object sender, TextContentChangedEventArgs args)
		{
			List<TextChangeData> changeData = new List<TextChangeData>();

			foreach (var change in args.Changes)
			{
				changeData.Add(new TextChangeData(change.OldPosition, change.Delta, Math.Max(change.NewSpan.Length, change.OldSpan.Length)));
			}

			_bufferTextChangeHandler.OnTextChanged(changeData);
		}
	}
}
