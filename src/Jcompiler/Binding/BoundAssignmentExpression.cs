using System;

namespace Jcompiler.Binding
{
    internal class BoundAssignmentExpression : BoundExpression
    {
        public BoundAssignmentExpression(string name, Type type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public string Name { get; }
        public override Type Type { get; }
        public object Value { get; }

        public override BoundNodeKind Kind => BoundNodeKind.BoundAssignmentExpression;
    }
}