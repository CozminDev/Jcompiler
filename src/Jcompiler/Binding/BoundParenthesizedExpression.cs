using System;

namespace Jcompiler.Binding
{
    public class BoundParenthesizedExpression : BoundExpression
    {
        public BoundParenthesizedExpression(Type type, BoundExpression expression)
        {
            Type = type;
            Expression = expression;
        }
        public override Type Type { get; }

        public override BoundNodeKind Kind => BoundNodeKind.ParenthesizedExpression;

        public BoundExpression Expression { get; }
    }
}