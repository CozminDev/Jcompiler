using System.Collections.Generic;
using System.Linq;

namespace Jcompiler.Syntax
{
    public class Token : Node
    {
        public Token(NodeKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override NodeKind Kind { get; }

        public int Position { get; }
        public string Text { get; }
        public object Value { get; set; }

        public TextSpan Span => new TextSpan(Position, Text.Length);

        public override IEnumerable<Node> GetChildren()
        {
            return Enumerable.Empty<Node>();
        }
    }
}