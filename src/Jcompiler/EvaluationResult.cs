using System.Collections.Generic;

namespace Jcompiler
{
    public class EvaluationResult
    {
        public EvaluationResult(List<string> diagnostics, object result)
        {
            Diagnostics = diagnostics;
            Result = result;
        }

        public List<string> Diagnostics { get; }
        public object Result { get; }
    }
}