using Jcompiler.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Jcompiler
{
    public class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private List<Diagnostic> diagnostics;

        public DiagnosticBag()
        {
            diagnostics = new List<Diagnostic>();
        }
        public IEnumerator<Diagnostic> GetEnumerator()
        {
            return diagnostics.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Report(TextSpan span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            diagnostics.Add(diagnostic);
        }

        public void ReportInvalidNumber(TextSpan span, string text, Type type)
        {
            var message = $"The number {text} isn't valid {type}.";
            Report(span, message);
        }

        public void ReportBadCharacter(int position, char character)
        {
            var span = new TextSpan(position, 1);
            var message = $"Bad character input: '{character}'.";
            Report(span, message);
        }

        public void ReportUnexpectedToken(TextSpan span, NodeKind actualKind, NodeKind expectedKind)
        {
            var message = $"Unexpected token <{actualKind}>, expected <{expectedKind}>.";
            Report(span, message);
        }

        public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
        {
            var message = $"Unary operator '{operatorText}' is not defined for type {operandType}.";
            Report(span, message);
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, Type leftType, Type rightType)
        {
            var message = $"Binary operator '{operatorText}' is not defined for types {leftType} and {rightType}.";
            Report(span, message);
        }
    }
}