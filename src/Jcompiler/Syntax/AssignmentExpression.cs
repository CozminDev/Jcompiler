using System.Collections.Generic;

namespace Jcompiler.Syntax
{
    internal class AssignmentExpression : Expression
    {
        public AssignmentExpression(IdentifierExpression left, Token operatorToken, Expression right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public IdentifierExpression Left { get; }
        public Token OperatorToken { get; }
        public Expression Right { get; }

        public override NodeKind Kind => NodeKind.AssignmentExpression;

        public override IEnumerable<Node> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}