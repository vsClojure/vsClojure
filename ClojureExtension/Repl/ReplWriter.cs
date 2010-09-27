namespace Microsoft.ClojureExtension.Repl
{
    public class ReplWriter
    {
        public void WriteBehindTheSceneExpressionToRepl(ReplData repl, string expression)
        {
            WriteExpressionToRepl(repl, expression);
            WriteToInteractive(repl, "\r\n");
        }

        public void WriteExpressionToRepl(ReplData repl, string expression)
        {
            repl.ReplProcess.StandardInput.WriteLine(expression);
        }

        public void WriteToInteractive(ReplData repl, string s)
        {
            repl.InteractiveTextBox.AppendText(s);
        }
    }
}