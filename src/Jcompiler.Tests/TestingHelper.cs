using Jcompiler.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Jcompiler.Tests
{
    public class TestingHelper
    {
        public static string GetNodeKindText(NodeKind kind)
        {
            switch (kind)
            {
                case NodeKind.PlusToken:
                    return "+";
                case NodeKind.MinusToken:
                    return "-";
                case NodeKind.StarToken:
                    return "*";
                case NodeKind.SlashToken:
                    return "/";
                case NodeKind.BangToken:
                    return "!";
                case NodeKind.EqualsToken:
                    return "=";
                case NodeKind.AmpersandAmpersandToken:
                    return "&&";
                case NodeKind.PipePipeToken:
                    return "||";
                case NodeKind.EqualsEqualsToken:
                    return "==";
                case NodeKind.BangEqualsToken:
                    return "!=";
                case NodeKind.OpenParenthesisToken:
                    return "(";
                case NodeKind.CloseParenthesisToken:
                    return ")";
                case NodeKind.FalseKeyword:
                    return "false";
                case NodeKind.TrueKeyword:
                    return "true";
                default:
                    return null;
            }
        }
    }

    public class AssertingEnumerator
    {
        public IEnumerator<Node> enumerator;

        public AssertingEnumerator(Node node)
        {
            enumerator = Flatten(node).GetEnumerator();
        }

        private IEnumerable<Node> Flatten(Node node)
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(node);

            while (stack.Count > 0)
            {
                Node n = stack.Pop();
                yield return n;

                foreach (Node child in n.GetChildren().Reverse())
                    stack.Push(child);
            }
        }

        public void AssertNode(NodeKind kind)
        {
            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsNotInstanceOfType(enumerator.Current, typeof(Token));
            Assert.AreEqual(kind, enumerator.Current.Kind);
        }

        public void AssertToken(NodeKind kind, string text)
        {
            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsInstanceOfType(enumerator.Current, typeof(Token));
            Token token = (Token)enumerator.Current;
            Assert.AreEqual(kind, token.Kind);
            Assert.AreEqual(text, token.Text);
        }
    }
}