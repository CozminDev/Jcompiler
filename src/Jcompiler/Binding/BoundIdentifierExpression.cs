using System;

namespace Jcompiler.Binding
{
    internal class BoundIdentifierExpression : BoundExpression
    {
        public BoundIdentifierExpression(string name, Type type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public override Type Type { get; }
        public object Value { get; }

        public override BoundNodeKind Kind => BoundNodeKind.BoundIdentifierExpression;

        public string Name { get; }
    }
}