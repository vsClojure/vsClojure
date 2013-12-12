using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace ClojureExtension.Editor.Intellisense
{
	[Export(typeof (IVsTextViewCreationListener))]
	[ContentType("Clojure")]
	[TextViewRole(PredefinedTextViewRoles.Interactive)]
	public class IntellisenseCommandFilterFactory : IVsTextViewCreationListener
	{
		[Import] private IVsEditorAdaptersFactoryService _adapterFactory = null;
		[Import] private ICompletionBroker _completionBroker = null;

		public void VsTextViewCreated(IVsTextView textViewAdapter)
		{
			var view = _adapterFactory.GetWpfTextView(textViewAdapter);
			var filter = new IntellisenseCommandFilter(view, _completionBroker);

			IOleCommandTarget next;
			textViewAdapter.AddCommandFilter(filter, out next);
			filter.Next = next;
		}
	}
}