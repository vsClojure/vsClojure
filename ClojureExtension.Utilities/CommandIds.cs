using System.ComponentModel.Design;
using Microsoft.VisualStudio;

namespace ClojureExtension.Utilities
{
	public class CommandIDs
	{
		public static CommandID FormatDocument = new CommandID(VSConstants.VSStd2K, (int) VSConstants.VSStd2KCmdID.FORMATDOCUMENT);
		public static CommandID BlockComment = new CommandID(VSConstants.VSStd2K, (int) VSConstants.VSStd2KCmdID.COMMENT_BLOCK);
		public static CommandID BlockUncomment = new CommandID(VSConstants.VSStd2K, (int) VSConstants.VSStd2KCmdID.UNCOMMENT_BLOCK);
		public static CommandID GotoDefinition = new CommandID(typeof(VSConstants.VSStd97CmdID).GUID, (int)VSConstants.VSStd97CmdID.GotoDefn);
        public static CommandID StartReplUsingProjectVersion = new CommandID(Guids.GuidClojureExtensionCmdSet, 10);
        public static CommandID LoadProjectIntoActiveRepl = new CommandID(Guids.GuidClojureExtensionCmdSet, 11);
        public static CommandID LoadFileIntoActiveRepl = new CommandID(Guids.GuidClojureExtensionCmdSet, 12);
        public static CommandID LoadActiveDocumentIntoRepl = new CommandID(Guids.GuidClojureExtensionCmdSet, 13);
        public static CommandID SwitchReplNamespaceToActiveDocument = new CommandID(Guids.GuidClojureExtensionCmdSet, 14);
        public static CommandID LoadSelectedTextIntoRepl = new CommandID(Guids.GuidClojureExtensionCmdSet, 15);
    }
}