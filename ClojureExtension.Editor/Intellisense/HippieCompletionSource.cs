// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ClojureExtension.Parsing;
using ClojureExtension.Project;
using ClojureExtension.Utilities;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Text;
using VSLangProj;
using vsClojure;
using Microsoft.VisualStudio.Language.Intellisense;
using System.Reflection;
using Thread = System.Threading.Thread;

namespace ClojureExtension.Editor.Intellisense
{
    public class HippieCompletionSource : ICompletionSource
    {
        private static IServiceProvider _serviceProvider;
        private static Metadata _metadata;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

          Thread delayedLoadMetadataThread = new Thread(() =>
          {
            _metadata = new Metadata(); // SlowLoadingProcess for the 1st time.
          });
          delayedLoadMetadataThread.IsBackground = true;
          delayedLoadMetadataThread.Start();
        }

        private readonly Entity<LinkedList<Token>> _tokenizedBuffer;
        private DTE2 _dte;

        public HippieCompletionSource(Entity<LinkedList<Token>> tokenizedBuffer)
        {
            _tokenizedBuffer = tokenizedBuffer;
            _dte = (DTE2)_serviceProvider.GetService(typeof(DTE));
        }

        public void Dispose()
        {
        }

        public List<EnvDTE.Project> GetProjects()
        {
            return ((Array)_dte.ActiveSolutionProjects).Cast<EnvDTE.Project>().ToList();
        }

        public List<Reference> GetAllProjectReferences()
        {
            List<EnvDTE.Project> projects = GetProjects();
            List<Reference> clojureProjectReferences = projects.Select(x => x.Object).Where(x => x is ClojureProjectNode).Cast<ClojureProjectNode>().SelectMany(x => x.References.Cast<Reference>()).ToList();
            List<Reference> otherProjectReferences = projects.Select(x => x.Object).Where(x => x is VSProject).Cast<VSProject>().SelectMany(x => x.References.Cast<Reference>()).ToList();
            return clojureProjectReferences.Union(otherProjectReferences).ToList();
        }

        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            if (_metadata == null)
            {
                return;
            }

            var caretPosition = session.TextView.Caret.Position.BufferPosition.Position;
            var tokenTriggeringIntellisense = _tokenizedBuffer.CurrentState.FindTokenAtIndex(caretPosition);
            if (caretPosition == tokenTriggeringIntellisense.IndexToken.StartIndex) tokenTriggeringIntellisense = tokenTriggeringIntellisense.Previous();
            var numberOfCharactersBeforeCursor = caretPosition - tokenTriggeringIntellisense.IndexToken.StartIndex;
            var textFromSymbolBeforeCursor = tokenTriggeringIntellisense.IndexToken.Token.Text.Substring(0, numberOfCharactersBeforeCursor);

            if (string.IsNullOrWhiteSpace(textFromSymbolBeforeCursor))
            {
                return;
            }

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

            //todo: load assemblies in separate appDomain
            List<Reference> references = GetAllProjectReferences();
            List<Assembly> referencedAssemblies = references.Select(x => Assembly.LoadFrom(x.Path)).ToList();

            completions.AddRange(referencedAssemblies.SelectMany(x => _metadata.LoadCompletionsInAssemblyMatchingString(x, textFromSymbolBeforeCursor)));

            //completions.AddRange(_metadata.LoadCompletionsInCljFileMatchingString(, textFromSymbolBeforeCursor));

            var snapshot = session.TextView.TextSnapshot;
            var start = new SnapshotPoint(snapshot, tokenTriggeringIntellisense.IndexToken.StartIndex);
            var end = new SnapshotPoint(snapshot, start.Position + tokenTriggeringIntellisense.IndexToken.Token.Text.Length);
            var applicableTo = snapshot.CreateTrackingSpan(new SnapshotSpan(start, end), SpanTrackingMode.EdgeInclusive);
            completionSets.Add(new CompletionSet("All", "All", applicableTo, completions, new List<Completion>()));
        }
    }
}