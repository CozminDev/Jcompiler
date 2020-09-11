using System;
using System.Collections.Generic;
using System.Text;

namespace Jcompiler.Syntax
{
    public class ParenthesizedExpression : Expression
    {
        public ParenthesizedExpression(Token openParenthesisToken, Expression expression, Token closeParenthesisToken)
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }

        public override NodeKind Kind => NodeKind.ParenthesizedExpression;
        public Token OpenParenthesisToken { get; }
        public Expression Expression { get; }
        public Token CloseParenthesisToken { get; }

        public override IEnumerable<Node> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return CloseParenthesisToken;
        }
    }
}