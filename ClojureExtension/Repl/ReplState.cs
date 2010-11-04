using System.Collections.Generic;
using System.Windows.Input;

namespace Microsoft.ClojureExtension.Repl
{
	public class ReplState
	{
		private readonly int _promptPosition;
		private readonly LinkedList<string> _history;
		private readonly LinkedList<Key> _downKeys;

		public ReplState(int promptPosition, LinkedList<string> history, LinkedList<Key> downKeys)
		{
			_promptPosition = promptPosition;
			_history = history;
			_downKeys = downKeys;
		}

		public LinkedList<Key> DownKeys
		{
			get { return new LinkedList<Key>(_downKeys); }
		}

		public LinkedList<string> History
		{
			get { return new LinkedList<string>(_history); }
		}

		public int PromptPosition
		{
			get { return _promptPosition; }
		}

		public ReplState ChangePromptPosition(int position)
		{
			return new ReplState(position, History, DownKeys);
		}

		public ReplState ChangeHistory(LinkedList<string> history)
		{
			return new ReplState(PromptPosition, history, DownKeys);
		}

		public ReplState ChangeDownKeys(LinkedList<Key> downKeys)
		{
			return new ReplState(PromptPosition, History, downKeys);
		}
	}
}