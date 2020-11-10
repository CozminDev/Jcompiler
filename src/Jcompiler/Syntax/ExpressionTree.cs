using System.Collections.Generic;
using System.Linq;

namespace Jcompiler.Syntax
{
    public class ExpressionTree
    {
        public ExpressionTree(DiagnosticBag diagnostics, Expression root, Token endOfFileToken)
        {
            Diagnostics = diagnostics;
            Root = root;
            EndOfFileToken = endOfFileToken;
        }

        public DiagnosticBag Diagnostics { get; }
        public Expression Root { get; }
        public Token EndOfFileToken { get; }

        public static ExpressionTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}