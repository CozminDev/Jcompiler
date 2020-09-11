using System.Collections.Generic;

namespace Jcompiler.Syntax
{
    public class BinaryExpression : Expression
    {
        public BinaryExpression(Expression left, Node operatorNode, Expression right)
        {
            Left = left;
            OperatorNode = operatorNode;
            Right = right;
        }

        public override NodeKind Kind => NodeKind.BinaryExpression;
        public Expression Left { get; }
        public Node OperatorNode { get; }
        public Expression Right { get; }

        public override IEnumerable<Node> GetChildren()
        {
            yield return Left;
            yield return OperatorNode;
            yield return Right;
        }
    }
}
