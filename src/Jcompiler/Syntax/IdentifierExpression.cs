using System.Collections.Generic;

namespace Jcompiler.Syntax
{
    internal class IdentifierExpression : Expression
    {

        public IdentifierExpression(Token identifierToken)
        {
            IdentifierToken = identifierToken;
        }

        public Token IdentifierToken { get; }

        public override NodeKind Kind => NodeKind.IdentifierExpression;

        public override IEnumerable<Node> GetChildren()
        {
            yield return IdentifierToken;
        }
    }
}