using System.Collections.Generic;

namespace Jcompiler
{
    public class EvaluationResult
    {
        public EvaluationResult(DiagnosticBag diagnostics, object result)
        {
            Diagnostics = diagnostics;
            Result = result;
        }

        public DiagnosticBag Diagnostics { get; }
        public object Result { get; }
    }
}