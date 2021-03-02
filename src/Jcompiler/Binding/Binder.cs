using Jcompiler.Syntax;
using System;
using System.Collections.Generic;

namespace Jcompiler.Binding
{
    public class Binder
    {
        private DiagnosticBag diagnostics;

        private Dictionary<string, object> symbolTable;

        public DiagnosticBag Diagnostics => diagnostics;

        public Binder(Dictionary<string, object> symbolTable)
        {
            diagnostics = new DiagnosticBag();
            this.symbolTable = symbolTable;
        }

        public BoundExpression BindExpression(Expression expression)
        {
            switch (expression.Kind)
            {
                case NodeKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpression)expression);
                case NodeKind.IdentifierExpression:
                    return BindIdentifierExpression((IdentifierExpression)expression);
                case NodeKind.UnaryExpression:
                    return BindUnaryExpression((UnaryExpression)expression);
                case NodeKind.BinaryExpression:
                    return BindBinaryExpression((BinaryExpression)expression);
                case NodeKind.ParenthesizedExpression:
                    return BindParenthesizedExpression((ParenthesizedExpression)expression);
                case NodeKind.AssignmentExpression:
                    return BindAssignmentExpression((AssignmentExpression)expression);
                default:
                    throw new Exception($"Unexpected expression {expression.Kind}");
            }
        }

        private BoundExpression BindIdentifierExpression(IdentifierExpression expression)
        {
            string name = expression.IdentifierToken.Text;

            if(symbolTable.TryGetValue(expression.IdentifierToken.Text, out var value))
                return new BoundIdentifierExpression(name, value.GetType(), value);

            diagnostics.Report(expression.IdentifierToken.Span, "Variable not defined");

            return new BoundIdentifierExpression(name, typeof(int), 0);
        }

        private BoundExpression BindAssignmentExpression(AssignmentExpression expression)
        {
            var name = expression.Left.IdentifierToken.Text;
            BoundExpression boundExpression = BindExpression(expression.Right);
            object value = null;

            if(boundExpression.GetType() == typeof(BoundAssignmentExpression))
            {
                value = ((BoundAssignmentExpression)boundExpression).Value;
            }
            else if(boundExpression.GetType() == typeof(BoundLiteralExpression))
            {
                value = ((BoundLiteralExpression)boundExpression).Value;
            }
            else
            {
                value = ((BoundBinaryExpression)boundExpression);
            }

            symbolTable[name] = value;
           
            return new BoundAssignmentExpression(name, boundExpression.Type, value);
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