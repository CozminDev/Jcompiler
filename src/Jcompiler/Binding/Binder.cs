using Jcompiler.Syntax;
using System;

namespace Jcompiler.Binding
{
    public class Binder
    {
        private DiagnosticBag diagnostics;

        public DiagnosticBag Diagnostics => diagnostics;

        public Binder()
        {
            diagnostics = new DiagnosticBag();
        }

        public BoundExpression BindExpression(Expression expression)
        {
            switch (expression.Kind)
            {
                case NodeKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpression)expression);
                case NodeKind.UnaryExpression:
                    return BindUnaryExpression((UnaryExpression)expression);
                case NodeKind.BinaryExpression:
                    return BindBinaryExpression((BinaryExpression)expression);
                case NodeKind.ParenthesizedExpression:
                    return BindParenthesizedExpression((ParenthesizedExpression)expression);
                default:
                    throw new Exception($"Unexpected expression {expression.Kind}");
            }
        }

        private BoundExpression BindParenthesizedExpression(ParenthesizedExpression expression)
        {
            BoundExpression boundExpression = BindExpression(expression.Expression);   

            return new BoundParenthesizedExpression(boundExpression.Type, boundExpression);
        }

        private BoundExpression BindUnaryExpression(UnaryExpression expression)
        {
            BoundExpression boundOperand = BindExpression(expression.Operand);
            BoundUnaryOperator boundOperator = BoundUnaryOperator.Bind(expression.OperatorToken.Kind, boundOperand.Type);

            if (boundOperator == null)
            {
                diagnostics.ReportUndefinedUnaryOperator(expression.OperatorToken.Span, expression.OperatorToken.Text, boundOperand.Type);
                return boundOperand;
            }

            return new BoundUnaryExpression(boundOperator, boundOperand);
        }

        private BoundExpression BindLiteralExpression(LiteralExpression expression)
        {
            return new BoundLiteralExpression(expression.Value ?? 0);
        }

        private BoundExpression BindBinaryExpression(BinaryExpression expression)
        {
            BoundExpression boundLeft = BindExpression(expression.Left);
            BoundExpression boundRight = BindExpression(expression.Right);
            BoundBinaryOperator boundOperator = BoundBinaryOperator.Bind(expression.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

            if (boundOperator == null)
            {
                diagnostics.ReportUndefinedBinaryOperator(expression.OperatorToken.Span, expression.OperatorToken.Text, boundLeft.Type, boundRight.Type);
                return boundLeft;
            }

            return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
        }
    }
}