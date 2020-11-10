using Jcompiler.Syntax;
using System;

namespace Jcompiler.Binding
{
    internal class BoundBinaryOperator
    {
        private BoundBinaryOperator(NodeKind nodeKind, BoundBinaryOperatorKind kind, Type type)
        : this(nodeKind, kind, type, type, type)
        {

        }

        private BoundBinaryOperator(NodeKind nodeKind, BoundBinaryOperatorKind kind, Type operandType, Type resultType)
        : this(nodeKind, kind, operandType, operandType, resultType)
        {

        }

        private BoundBinaryOperator(NodeKind nodeKind, BoundBinaryOperatorKind kind, Type leftType, Type rightTye, Type resultType)
        {
            NodeKind = nodeKind;
            Kind = kind;
            LeftType = leftType;
            RightTye = rightTye;
            Type = resultType;
        }

        public NodeKind NodeKind { get; }
        public BoundBinaryOperatorKind Kind { get; }
        public Type LeftType { get; }
        public Type RightTye { get; }
        public Type Type { get; }

        private static BoundBinaryOperator[] _operators =
        {
            new BoundBinaryOperator(NodeKind.PlusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
            new BoundBinaryOperator(NodeKind.MinusToken, BoundBinaryOperatorKind.Subtraction, typeof(int)),
            new BoundBinaryOperator(NodeKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int)),
            new BoundBinaryOperator(NodeKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int)),
            new BoundBinaryOperator(NodeKind.EqualsEqualsToken, BoundBinaryOperatorKind.Equals, typeof(int), typeof(bool)),
            new BoundBinaryOperator(NodeKind.BangEqualsToken, BoundBinaryOperatorKind.NotEquals, typeof(int), typeof(bool)),

            new BoundBinaryOperator(NodeKind.AmpersandAmpersandToken, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
            new BoundBinaryOperator(NodeKind.PipePipeToken, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),
            new BoundBinaryOperator(NodeKind.EqualsEqualsToken, BoundBinaryOperatorKind.Equals, typeof(bool)),
            new BoundBinaryOperator(NodeKind.BangEqualsToken, BoundBinaryOperatorKind.NotEquals, typeof(bool)),
        };

        public static BoundBinaryOperator Bind(NodeKind nodeKind, Type leftType, Type rightType)
        {
            foreach (var op in _operators)
            {
                if (op.NodeKind == nodeKind && op.LeftType == leftType && op.RightTye == rightType)
                    return op;
            }

            return null;
        }
    }
}