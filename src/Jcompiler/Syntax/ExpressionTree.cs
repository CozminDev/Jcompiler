using Jcompiler.Text;

namespace Jcompiler.Syntax
{
    public class ExpressionTree
    {
        public ExpressionTree(SourceText text)
        {
            Parser parser = new Parser(text);
            Diagnostics = parser.Diagnostics;
            Root = parser.ParseCompilationUnit();
            Text = text;
        }

        public DiagnosticBag Diagnostics { get; }
        public CompilationUnit Root { get; }
        public SourceText Text { get; }

        public static ExpressionTree Parse(string text)
        {
            SourceText sourceText = SourceText.From(text);
            return new ExpressionTree(sourceText);
        }
    }
}