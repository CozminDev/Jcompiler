using Jcompiler.Binding;
using Jcompiler.Syntax;

namespace Jcompiler
{
    public class Compilation
    {
        private readonly ExpressionTree expressionTree;

        public Compilation(ExpressionTree expressionTree)
        {
            this.expressionTree = expressionTree;
        }

        public EvaluationResult Evaluate()
        {
            Binder binder = new Binder();

            BoundExpression boundExpression = binder.BindExpression(expressionTree.Root);

            Evaluator evaluator = new Evaluator(boundExpression);

            object result = evaluator.Evaluate();

            return new EvaluationResult(binder.Diagnostics, result);
        }
    }
}