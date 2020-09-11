using Jcompiler.Syntax;
using System;
using System.Linq;

namespace Jcompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("> ");
            string text = Console.ReadLine();
            ExpressionTree expressionTree = ExpressionTree.Parse(text);
            PrettyPrint(expressionTree.Root);
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
