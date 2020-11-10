using Jcompiler.Binding;
using Jcompiler.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jcompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                string text = Console.ReadLine();
                ExpressionTree expressionTree = ExpressionTree.Parse(text);
                Binder binder = new Binder();
                BoundExpression expr = binder.BindExpression(expressionTree.Root);
                IEnumerable<string> diagnostics = expressionTree.Diagnostics.Concat(binder.Diagnostics);
                if (!diagnostics.Any())
                {
                    Evaluator evaluator = new Evaluator(expr);
                    Console.WriteLine(evaluator.Evaluate());
                    Console.WriteLine();
                    PrettyPrint(expressionTree.Root);

                }
                else
                {
                    foreach (string error in diagnostics)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(error);
                        Console.ResetColor();
                    }
                }
            }
        }

        static void PrettyPrint(Node node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is Token t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "   " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }
    }
}
