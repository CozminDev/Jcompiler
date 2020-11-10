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

                if (expressionTree.Diagnostics.Any())
                {
                    PrintDiagnostics(expressionTree.Diagnostics);
                    continue;
                }

                Compilation compilation = new Compilation(expressionTree);
                EvaluationResult result = compilation.Evaluate();

                if (result.Diagnostics.Any())
                {
                    PrintDiagnostics(result.Diagnostics);
                    continue;
                }

                Console.WriteLine(result.Result);
                PrettyPrint(expressionTree.Root);
            }
        }

        static void PrintDiagnostics(List<string> diagnostics)
        {
            foreach (string error in diagnostics)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(error);
                Console.ResetColor();
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
