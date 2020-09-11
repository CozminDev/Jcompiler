using System.Collections.Generic;

namespace Jcompiler.Syntax
{
    public class UnaryExpression : Expression
    {
        public UnaryExpression(Token operatorToken, Expression operand)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }

        public override NodeKind Kind => NodeKind.UnaryExpression;
        public Token OperatorToken { get; }
        public Expression Operand { get; }

        public override IEnumerable<Node> GetChildren()
        {
            yield return OperatorToken;
            yield return Operand;
        }
    }
}
