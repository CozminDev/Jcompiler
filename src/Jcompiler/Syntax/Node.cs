using System.Collections.Generic;

namespace Jcompiler.Syntax
{
    public abstract class Node
    {
        public abstract NodeKind Kind { get; }

        public abstract IEnumerable<Node> GetChildren();
    }
}