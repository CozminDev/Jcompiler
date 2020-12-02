using Jcompiler.Binding;
using System;
using System.Collections.Generic;

namespace Jcompiler
{
    public class Evaluator
    {
        private readonly BoundExpression root;
        private Dictionary<string, object> symbolTable;

        public Evaluator(BoundExpression root, Dictionary<string, object> symbolTable)
        {
            this.root = root;
            this.symbolTable = symbolTable;
        }

        public object Evaluate()
        {
            return EvaluateExpression(root);
        }

        private object EvaluateExpression(BoundExpression node)
        {
            if (node is BoundLiteralExpression n)
                return n.Value;

            if (node is BoundUnaryExpression u)
            {
                var operand = EvaluateExpression(u.Operand);

                switch (u.Op.Kind)
                {
                    case BoundUnaryOperatorKind.Identity:
                        return (int)operand;
                    case BoundUnaryOperatorKind.Negation:
                        return -(int)operand;
                    case BoundUnaryOperatorKind.LogicalNegation:
                        return !(bool)operand;
                    default:
                        throw new Exception($"Unexpected unary operator {u.Op}");
                }
            }

            if (node is BoundBinaryExpression b)
            {
                object left = EvaluateExpression(b.Left);
                object right = EvaluateExpression(b.Right);

                switch (b.Op.Kind)
                {
                    case BoundBinaryOperatorKind.Addition:
                        return (int)left + (int)right;
                    case BoundBinaryOperatorKind.Subtraction:
                        return (int)left - (int)right;
                    case BoundBinaryOperatorKind.Multiplication:
                        return (int)left * (int)right;
                    case BoundBinaryOperatorKind.Division:
                        return (int)left / (int)right;
                    case BoundBinaryOperatorKind.LogicalAnd:
                        return (bool)left && (bool)right;
                    case BoundBinaryOperatorKind.LogicalOr:
                        return (bool)left || (bool)right;
                    case BoundBinaryOperatorKind.Equals:
                        return Equals(left, right);
                    case BoundBinaryOperatorKind.NotEquals:
                        return !Equals(left, right);
                    default:
                        throw new Exception($"Unexpected binary operator {b.Op}");
                }
            }

            if(node is BoundParenthesizedExpression p)
            {
                return EvaluateExpression(p.Expression);
            }

            if(node is BoundIdentifierExpression i)
            {
                if (i.Value.GetType() == typeof(BoundBinaryExpression))
                    return EvaluateExpression((BoundBinaryExpression)i.Value);


                return Convert.ChangeType(i.Value, i.Type);
            }

            if (node is BoundAssignmentExpression a)
            {
                if (a.Value.GetType() == typeof(BoundBinaryExpression))
                {
                    object value = EvaluateExpression((BoundBinaryExpression)a.Value);
                    symbolTable[a.Name] = value;
                    return value;
                }

                return Convert.ChangeType(a.Value, a.Type);
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}