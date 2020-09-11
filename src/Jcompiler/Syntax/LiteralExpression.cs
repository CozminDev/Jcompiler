using System.Collections.Generic;

namespace Jcompiler.Syntax
{
    public class LiteralExpression : Expression
    {
        public LiteralExpression(Token literalToken)
           : this(literalToken, literalToken.Value)
        {
        }

        public LiteralExpression(Token literalToken, object value)
        {
            LiteralToken = literalToken;
            Value = value;
        }

        public override NodeKind Kind => NodeKind.LiteralExpression;
        public Token LiteralToken { get; }
        public object Value { get; }

        public override IEnumerable<Node> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}
