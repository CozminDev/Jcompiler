using System.Collections.Generic;

namespace Jcompiler.Syntax
{
    public class BinaryExpression : Expression
    {
        public BinaryExpression(Expression left, Token operatorToken, Expression right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public override NodeKind Kind => NodeKind.BinaryExpression;
        public Expression Left { get; }
        public Token OperatorToken { get; }
        public Expression Right { get; }

        public override IEnumerable<Node> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}
