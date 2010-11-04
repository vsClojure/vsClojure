using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.ClojureExtension.Utilities;

namespace Microsoft.ClojureExtension.Repl
{
	public class MetaKeyWatcher
	{
		private readonly Entity<ReplState> _replEntity;

		public MetaKeyWatcher(Entity<ReplState> replEntity)
		{
			_replEntity = replEntity;
		}

		public void PreviewKeyDown(object sender, KeyEventArgs e)
		{
			LinkedList<Key> downKeys = _replEntity.CurrentState.DownKeys;
			downKeys.AddLast(e.Key);
			_replEntity.CurrentState = _replEntity.CurrentState.ChangeDownKeys(downKeys);
		}

		public void PreviewKeyUp(object sender, KeyEventArgs e)
		{
			LinkedList<Key> downKeys = _replEntity.CurrentState.DownKeys;
			downKeys.Remove(e.Key);
			_replEntity.CurrentState = _replEntity.CurrentState.ChangeDownKeys(downKeys);
		}

		public bool IsShiftDown()
		{
			return _replEntity.CurrentState.DownKeys.Contains(Key.LeftShift) || _replEntity.CurrentState.DownKeys.Contains(Key.RightShift);
		}

		public bool ControlIsDown()
		{
			return _replEntity.CurrentState.DownKeys.Contains(Key.LeftCtrl) || _replEntity.CurrentState.DownKeys.Contains(Key.RightCtrl);
		}
	}
}