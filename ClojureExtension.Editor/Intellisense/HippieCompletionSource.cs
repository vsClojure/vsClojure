using System.Collections.Generic;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;

namespace ClojureExtension.Editor.Intellisense
{
	public class HippieCompletionSource : ICompletionSource
	{
		private readonly Entity<LinkedList<Token>> _tokenizedBuffer;

		public HippieCompletionSource(Entity<LinkedList<Token>> tokenizedBuffer)
		{
			_tokenizedBuffer = tokenizedBuffer;
		}

		public void Dispose()
		{
		}

		public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
		{
			var caretPosition = session.TextView.Caret.Position.BufferPosition.Position;
			var tokenTriggeringIntellisense = _tokenizedBuffer.CurrentState.FindTokenAtIndex(caretPosition);
			if (caretPosition == tokenTriggeringIntellisense.IndexToken.StartIndex) tokenTriggeringIntellisense = tokenTriggeringIntellisense.Previous();
			var numberOfCharactersBeforeCursor = caretPosition - tokenTriggeringIntellisense.IndexToken.StartIndex;
			var textFromSymbolBeforeCursor = tokenTriggeringIntellisense.IndexToken.Token.Text.Substring(0, numberOfCharactersBeforeCursor);
			var currentIndexToken = _tokenizedBuffer.CurrentState.FindTokenAtIndex(0);
			var completions = new List<Completion>();

			while (currentIndexToken != null)
			{
				if (currentIndexToken.IndexToken.StartIndex != tokenTriggeringIntellisense.IndexToken.StartIndex)
				{
					if (currentIndexToken.Node.Value.Type == TokenType.Symbol && currentIndexToken.Node.Value.Text.StartsWith(textFromSymbolBeforeCursor))
					{
						if (completions.Find(c => c.DisplayText == currentIndexToken.Node.Value.Text) == null)
						{
							completions.Add(new Completion(currentIndexToken.Node.Value.Text));
						}
					}
				}

				currentIndexToken = currentIndexToken.Next();
			}

			var snapshot = session.TextView.TextSnapshot;
			var start = new SnapshotPoint(snapshot, tokenTriggeringIntellisense.IndexToken.StartIndex);
			var end = new SnapshotPoint(snapshot, start.Position + tokenTriggeringIntellisense.IndexToken.Token.Text.Length);
			var applicableTo = snapshot.CreateTrackingSpan(new SnapshotSpan(start, end), SpanTrackingMode.EdgeInclusive);
			completionSets.Add(new CompletionSet("All", "All", applicableTo, completions, new List<Completion>()));
		}
	}
}