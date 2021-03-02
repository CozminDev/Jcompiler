using System.Collections.Generic;

namespace Jcompiler.Syntax
{
    public class CompilationUnit : Expression
    {
        public CompilationUnit(Expression expression, Token endOfFileToken)
        {
            Expression = expression;
            EndOfFileToken = endOfFileToken;
        }
        public override NodeKind Kind => NodeKind.CompilationUnit;
        public Expression Expression { get; }
        public Token EndOfFileToken { get; }

        public override IEnumerable<Node> GetChildren()
        {
            yield return Expression;
        }
    }
}