using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio;

namespace ClojureExtension.Deployment
{
	public class CommandIDs
	{
		public static CommandID FormatDocument = new CommandID(VSConstants.VSStd2K, (int) VSConstants.VSStd2KCmdID.FORMATDOCUMENT);
		public static CommandID BlockComment = new CommandID(VSConstants.VSStd2K, (int) VSConstants.VSStd2KCmdID.COMMENT_BLOCK);
		public static CommandID BlockUncomment = new CommandID(VSConstants.VSStd2K, (int) VSConstants.VSStd2KCmdID.UNCOMMENT_BLOCK);
		public static CommandID GotoDefinition = new CommandID(typeof(VSConstants.VSStd97CmdID).GUID, (int)VSConstants.VSStd97CmdID.GotoDefn);
	}
}