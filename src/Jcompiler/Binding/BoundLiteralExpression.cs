using System;

namespace Jcompiler.Binding
{
    public class BoundLiteralExpression : BoundExpression
    {

        public BoundLiteralExpression(object value)
        {
            this.Value = value;
        }

        public override BoundNodeKind Kind => BoundNodeKind.BoundLiteralExpression;

        public override Type Type => Value.GetType();

        public object Value { get; set; }
    }
}