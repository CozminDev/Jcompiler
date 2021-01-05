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
            Dictionary<string, object> symbolTable = new Dictionary<string, object>();

            while (true)
            {
                Console.Write("> ");
                string text = Console.ReadLine();
                ExpressionTree expressionTree = ExpressionTree.Parse(text);

                if (expressionTree.Diagnostics.Any())
                {
                    PrintDiagnostics(expressionTree.Diagnostics, text);
                    continue;
                }

                Compilation compilation = new Compilation(expressionTree, symbolTable);
                EvaluationResult result = compilation.Evaluate();

                if (result.Diagnostics.Any())
                {
                    PrintDiagnostics(result.Diagnostics, text);
                    continue;
                }

                Console.WriteLine(result.Result);
                PrettyPrint(expressionTree.Root);
            }
        }

        static void PrintDiagnostics(DiagnosticBag diagnostics, string text)
        {
            foreach (Diagnostic error in diagnostics)
            {
                string prefix = text.Substring(0, error.Span.Position);
                string span = text.Substring(error.Span.Position, error.Span.Length);
                string suffix = text.Substring(error.Span.Position + error.Span.Length);

                Console.WriteLine();
                Console.Write("     ");
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(span);
                Console.ResetColor();
                Console.Write(suffix);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                Console.WriteLine();
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