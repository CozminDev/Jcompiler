using Jcompiler.Binding;
using Jcompiler.Syntax;
using System.Collections.Generic;

namespace Jcompiler
{
    public class Compilation
    {
        private readonly ExpressionTree expressionTree;
        private Dictionary<string, object> symbolTable;

        public Compilation(ExpressionTree expressionTree, System.Collections.Generic.Dictionary<string, object> symbolTable)
        {
            this.expressionTree = expressionTree;
            this.symbolTable = symbolTable;
        }

        public EvaluationResult Evaluate()
        {
            Binder binder = new Binder(symbolTable);

            BoundExpression boundExpression = binder.BindExpression(expressionTree.Root);

            Evaluator evaluator = new Evaluator(boundExpression, symbolTable);

            object result = evaluator.Evaluate();

            return new EvaluationResult(binder.Diagnostics, result);
        }
    }
}