using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using ClojureExtension.Parsing;
using ClojureExtension.Utilities;
using Microsoft.VisualStudio.Text;
using vsClojure;
using Microsoft.VisualStudio.Language.Intellisense;

namespace ClojureExtension.Editor.Intellisense
{
  public class HippieCompletionSource : ICompletionSource
  {
    private readonly Entity<LinkedList<Token>> _tokenizedBuffer;
    private Metadata _metadata = new Metadata();

    public static void Initialize()
    {
      new Thread(() =>
      {
        new Metadata(); // pre-load clojure.dll due to slow start-up times
      }).Start();
    }

    public HippieCompletionSource(Entity<LinkedList<Token>> tokenizedBuffer)
    {
      _tokenizedBuffer = tokenizedBuffer;
    }

    public void Dispose()
    {
    }

    public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
    {
      string fileNameBeingEdited = "";

      var caretPosition = session.TextView.Caret.Position.BufferPosition.Position;
      var tokenTriggeringIntellisense = _tokenizedBuffer.CurrentState.FindTokenAtIndex(caretPosition);
      if (caretPosition == tokenTriggeringIntellisense.IndexToken.StartIndex) tokenTriggeringIntellisense = tokenTriggeringIntellisense.Previous();
      var numberOfCharactersBeforeCursor = caretPosition - tokenTriggeringIntellisense.IndexToken.StartIndex;
      var textFromSymbolBeforeCursor = tokenTriggeringIntellisense.IndexToken.Token.Text.Substring(0, numberOfCharactersBeforeCursor);

      var completions = new List<Completion>();

      //todo: convert current file being edited to constantly try to compile in background thread & update metadatacache (allowing Clojure commands to present intellisense for file being edited)
      //todo: add context sensitivity to filter for functions/variables depending on 1st argument to list or not & whether list is quoted or list starts with .. or ->
      NamespaceParser namespaceParser = new NamespaceParser(NamespaceParser.NamespaceSymbols);
      string namespaceOfFile = "";
      try
      {
        namespaceOfFile = namespaceParser.Execute(_tokenizedBuffer.CurrentState);
      }
      catch { }

      var currentIndexToken = _tokenizedBuffer.CurrentState.FindTokenAtIndex(0);

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

      completions.AddRange(_metadata.LoadCoreCompletionsMatchingString(textFromSymbolBeforeCursor));

      var snapshot = session.TextView.TextSnapshot;
      var start = new SnapshotPoint(snapshot, tokenTriggeringIntellisense.IndexToken.StartIndex);
      var end = new SnapshotPoint(snapshot, start.Position + tokenTriggeringIntellisense.IndexToken.Token.Text.Length);
      var applicableTo = snapshot.CreateTrackingSpan(new SnapshotSpan(start, end), SpanTrackingMode.EdgeInclusive);
      completionSets.Add(new CompletionSet("All", "All", applicableTo, completions, new List<Completion>()));
    }
  }
}