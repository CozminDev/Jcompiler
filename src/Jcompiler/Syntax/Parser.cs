using System.Collections.Generic;

namespace Jcompiler.Syntax
{
    public class Parser
    {
        private List<string> diagnostics;
        private int position;
        private List<Token> tokens;
        private Token Current
        {
            get
            {
                if (position > tokens.Count - 1)
                    return null;

                return tokens[position];
            }
        }

        public Parser(string text)
        {
            diagnostics = new List<string>();
            tokens = new List<Token>();

            Lexer lexer = new Lexer(text);
            Token token;
            do
            {
                token = lexer.NextToken();

                if (token.Kind != NodeKind.WhitespaceToken && token.Kind != NodeKind.BadToken)
                    tokens.Add(token);
            } while (token.Kind != NodeKind.EndOfFileToken);
        }

        private Token GetTokenAndMoveNext()
        {
            Token current = Current;
            position++;
            return current;
        }

        private Token MatchToken(NodeKind kind)
        {
            if (Current.Kind == kind)
                return GetTokenAndMoveNext();

            diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected <{kind}>");
            return new Token(kind, Current.Position, null, null);
        }

        public ExpressionTree Parse()
        {
            Expression expression = ParseExpression();
            Token endOfFileToken = MatchToken(NodeKind.EndOfFileToken);
            return new ExpressionTree(diagnostics, expression, endOfFileToken);
        }

        private Expression ParseExpression(int parentPrecedence = 0)
        {
            Expression left;
            int unaryOperatorPrecedence = Current.Kind.GetUnaryOperatorPrecedence();
            if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
            {
                Token operatorToken = GetTokenAndMoveNext();
                Expression operand = ParseExpression(unaryOperatorPrecedence);
                left = new UnaryExpression(operatorToken, operand);
            }
            else
            {
                left = ParsePrimaryExpression();
            }

            while (true)
            {
                int precedence = Current.Kind.GetBinaryOperatorPrecedence();
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;

                Node operatorNode = GetTokenAndMoveNext();
                Expression right = ParseExpression(precedence);
                left = new BinaryExpression(left, operatorNode, right);
            }

            return left;
        }

        private Expression ParsePrimaryExpression()
        {
            switch (Current.Kind)
            {
                case NodeKind.OpenParenthesisToken:
                    {
                        var left = GetTokenAndMoveNext();
                        var expression = ParseExpression();
                        var right = MatchToken(NodeKind.CloseParenthesisToken);
                        return new ParenthesizedExpression(left, expression, right);
                    }

                case NodeKind.FalseKeyword:
                case NodeKind.TrueKeyword:
                    {
                        var keywordToken = GetTokenAndMoveNext();
                        var value = keywordToken.Kind == NodeKind.TrueKeyword;
                        return new LiteralExpression(keywordToken, value);
                    }

                default:
                    {
                        var numberToken = MatchToken(NodeKind.NumberToken);
                        return new LiteralExpression(numberToken);
                    }
            }
        }
    }
}
