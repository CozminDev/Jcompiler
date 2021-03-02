using Jcompiler.Syntax;
using Jcompiler.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, object> symbolTable = new Dictionary<string, object>();
            StringBuilder textBuilder = new StringBuilder();

            while (true)
            {
                if (textBuilder.Length == 0)
                    Console.Write("> ");
                else
                    Console.Write("|");

                string input = Console.ReadLine();
                bool isBlank = string.IsNullOrWhiteSpace(input);
                textBuilder.AppendLine(input);

                string text = textBuilder.ToString();
                ExpressionTree expressionTree = ExpressionTree.Parse(text);

                if (!isBlank && expressionTree.Diagnostics.Any())
                    continue;

                if (expressionTree.Diagnostics.Any())
                {
                    PrintDiagnostics(expressionTree.Diagnostics, expressionTree.Text);
                    textBuilder.Clear();
                    continue;
                }

                Compilation compilation = new Compilation(expressionTree, symbolTable);
                EvaluationResult result = compilation.Evaluate();

                if (result.Diagnostics.Any())
                {
                    PrintDiagnostics(result.Diagnostics, expressionTree.Text);
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(result.Result);
                Console.ResetColor();
                PrettyPrint(expressionTree.Root.Expression);
                textBuilder.Clear();
            }
        }

        static void PrintDiagnostics(DiagnosticBag diagnostics, SourceText text)
        {
            foreach (Diagnostic diagnostic in diagnostics)
            {
                int lineIndex = text.GetLineIndexByPosition(diagnostic.Span.Start);
                TextLine line = text.Lines[lineIndex];

                int lineNumber = lineIndex + 1;
                int characterNumber = diagnostic.Span.Start - line.Start + 1;

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"({lineNumber}, {characterNumber}): ");
                Console.WriteLine(diagnostic);
                Console.ResetColor();

                string prefix = text.ToString(line.Start, diagnostic.Span.Start);
                string span = text.ToString(diagnostic.Span.Start, diagnostic.Span.Length);
                string suffix = text.ToString(diagnostic.Span.End, line.End - diagnostic.Span.End);

                Console.Write("     ");
                Console.Write(prefix);

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(span);
                Console.ResetColor();

                Console.Write(suffix);

                Console.WriteLine();
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