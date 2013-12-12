using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace ClojureExtension.Editor.Intellisense
{
	public class IntellisenseCommandFilter : IOleCommandTarget
	{
		private readonly IWpfTextView _textView;
		private readonly ICompletionBroker _broker;
		private ICompletionSession _currentSession;
		public IOleCommandTarget Next { get; set; }

		public IntellisenseCommandFilter(IWpfTextView textView, ICompletionBroker broker)
		{
			_textView = textView;
			_broker = broker;
			_currentSession = null;
		}

		private bool IsSessionStarted
		{
			get { return _currentSession != null && _currentSession.IsStarted && !_currentSession.IsDismissed; }
		}

		public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
		{
			var handled = false;
			var hresult = VSConstants.S_OK;

			if (pguidCmdGroup == VSConstants.VSStd2K)
			{
				switch ((VSConstants.VSStd2KCmdID) nCmdID)
				{
					case VSConstants.VSStd2KCmdID.AUTOCOMPLETE:
					case VSConstants.VSStd2KCmdID.COMPLETEWORD:
						handled = true;
						if (IsSessionStarted) break;
						_currentSession = StartSession();
						break;
					case VSConstants.VSStd2KCmdID.RETURN:
						if (!IsSessionStarted) break;
						handled = true;
						_currentSession.Commit();
						break;
					case VSConstants.VSStd2KCmdID.TAB:
						if (!IsSessionStarted) break;
						handled = true;
						_currentSession.Commit();
						break;
					case VSConstants.VSStd2KCmdID.CANCEL:
						if (!IsSessionStarted) break;
						handled = true;
						_currentSession.Dismiss();
						break;
				}
			}

			if (!handled) hresult = Next.Exec(pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);

			if (ErrorHandler.Succeeded(hresult))
			{
				if (pguidCmdGroup == VSConstants.VSStd2K)
				{
					switch ((VSConstants.VSStd2KCmdID) nCmdID)
					{
						case VSConstants.VSStd2KCmdID.TYPECHAR:
							if (IsSessionStarted) _currentSession.Filter();
							break;
						case VSConstants.VSStd2KCmdID.BACKSPACE:
							if (IsSessionStarted)
							{
								_currentSession.Dismiss();
								_currentSession = StartSession();
							}
							break;
					}
				}
			}

			return hresult;
		}

		private ICompletionSession StartSession()
		{
			var caret = _textView.Caret.Position.BufferPosition;
			var snapshot = caret.Snapshot;
			var session = _broker.CreateCompletionSession(_textView, snapshot.CreateTrackingPoint(caret, PointTrackingMode.Positive), true);
			session.Start();
			return session;
		}

		public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
		{
			if (pguidCmdGroup == VSConstants.VSStd2K)
			{
				switch ((VSConstants.VSStd2KCmdID) prgCmds[0].cmdID)
				{
					case VSConstants.VSStd2KCmdID.AUTOCOMPLETE:
					case VSConstants.VSStd2KCmdID.COMPLETEWORD:
						prgCmds[0].cmdf = (uint) OLECMDF.OLECMDF_ENABLED | (uint) OLECMDF.OLECMDF_SUPPORTED;
						return VSConstants.S_OK;
				}
			}

			return Next.QueryStatus(pguidCmdGroup, cCmds, prgCmds, pCmdText);
		}
	}
}