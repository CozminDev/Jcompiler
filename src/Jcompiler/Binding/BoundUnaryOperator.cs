using Jcompiler.Syntax;
using System;

namespace Jcompiler.Binding
{
    public class BoundUnaryOperator
    {
        private BoundUnaryOperator(NodeKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType)
           : this(syntaxKind, kind, operandType, operandType)
        {
        }

        private BoundUnaryOperator(NodeKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType, Type resultType)
        {
            NodeKind = syntaxKind;
            Kind = kind;
            OperandType = operandType;
            Type = resultType;
        }

        public NodeKind NodeKind { get; }
        public BoundUnaryOperatorKind Kind { get; }
        public Type OperandType { get; }
        public Type Type { get; }

        private static BoundUnaryOperator[] _operators =
        {
            new BoundUnaryOperator(NodeKind.BangToken, BoundUnaryOperatorKind.LogicalNegation, typeof(bool)),

            new BoundUnaryOperator(NodeKind.PlusToken, BoundUnaryOperatorKind.Identity, typeof(int)),
            new BoundUnaryOperator(NodeKind.MinusToken, BoundUnaryOperatorKind.Negation, typeof(int)),
        };

        public static BoundUnaryOperator Bind(NodeKind nodeKind, Type operandType)
        {
            foreach (var op in _operators)
            {
                if (op.NodeKind == nodeKind && op.OperandType == operandType)
                    return op;
            }

            return null;
        }
    }
}