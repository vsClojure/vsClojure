using System.Collections.Generic;

namespace ClojureExtension.Repl
{
	public class ReplState
	{
		private readonly int _promptPosition;
		private readonly LinkedList<string> _history;

		public ReplState() : this(0, new LinkedList<string>())
		{
			
		}

		public ReplState(int promptPosition, LinkedList<string> history)
		{
			_promptPosition = promptPosition;
			_history = history;
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
			return new ReplState(position, History);
		}

		public ReplState ChangeHistory(LinkedList<string> history)
		{
			return new ReplState(PromptPosition, history);
		}
	}
}