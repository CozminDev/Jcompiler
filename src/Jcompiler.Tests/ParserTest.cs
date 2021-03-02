using Jcompiler.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jcompiler.Tests
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Parse_PlusExpression_ReturnsCorrectTree()
        {
            string text = "1+2";
            ExpressionTree tree = ExpressionTree.Parse(text);
            AssertingEnumerator enumerator = new AssertingEnumerator(tree.Root.Expression);

            //   +
            //  / \
            // 1   2 

            enumerator.AssertNode(NodeKind.BinaryExpression);
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "1");
            enumerator.AssertToken(NodeKind.PlusToken, TestingHelper.GetNodeKindText(NodeKind.PlusToken));
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "2");
        }

        [TestMethod]
        public void Parse_PlusAndTimesExpression_HonorsPrecedence_ReturnsCorrectTree()
        {
            string text = "1+2*5";
            ExpressionTree tree = ExpressionTree.Parse(text);
            AssertingEnumerator enumerator = new AssertingEnumerator(tree.Root.Expression);

            //   +
            //  / \
            // 1   *
            //    / \ 
            //   2   5 

            enumerator.AssertNode(NodeKind.BinaryExpression);
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "1");
            enumerator.AssertToken(NodeKind.PlusToken, TestingHelper.GetNodeKindText(NodeKind.PlusToken));
            enumerator.AssertNode(NodeKind.BinaryExpression);
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "2");
            enumerator.AssertToken(NodeKind.StarToken, TestingHelper.GetNodeKindText(NodeKind.StarToken));
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "5");
        }

        [TestMethod]
        public void Parse_ParenthesizedExpression_ReturnsCorrectTree()
        {
            string text = "(1+2)*5";
            ExpressionTree tree = ExpressionTree.Parse(text);
            AssertingEnumerator enumerator = new AssertingEnumerator(tree.Root.Expression);

            //      *
            //     / \
            //    +   5
            //   / \
            //  1   2

            enumerator.AssertNode(NodeKind.BinaryExpression);
            enumerator.AssertNode(NodeKind.ParenthesizedExpression);
            enumerator.AssertToken(NodeKind.OpenParenthesisToken, TestingHelper.GetNodeKindText(NodeKind.OpenParenthesisToken));
            enumerator.AssertNode(NodeKind.BinaryExpression);
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "1");
            enumerator.AssertToken(NodeKind.PlusToken, TestingHelper.GetNodeKindText(NodeKind.PlusToken));
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "2");
            enumerator.AssertToken(NodeKind.CloseParenthesisToken, TestingHelper.GetNodeKindText(NodeKind.CloseParenthesisToken));
            enumerator.AssertToken(NodeKind.StarToken, TestingHelper.GetNodeKindText(NodeKind.StarToken));
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "5");
        }

        [TestMethod]
        public void Parse_UnaryExpression_ReturnsCorrectTree()
        {
            string text = "-2+5";
            ExpressionTree tree = ExpressionTree.Parse(text);
            AssertingEnumerator enumerator = new AssertingEnumerator(tree.Root.Expression);

            //      *
            //     / \
            //    -   5
            //    |
            //    2

            enumerator.AssertNode(NodeKind.BinaryExpression);
            enumerator.AssertNode(NodeKind.UnaryExpression);
            enumerator.AssertToken(NodeKind.MinusToken, TestingHelper.GetNodeKindText(NodeKind.MinusToken));
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "2");
            enumerator.AssertToken(NodeKind.PlusToken, TestingHelper.GetNodeKindText(NodeKind.PlusToken));
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "5");
        }

        [TestMethod]
        public void Parse_AssignmentExpression_ReturnsCorrectTree()
        {
            string text = "a = 1";
            ExpressionTree tree = ExpressionTree.Parse(text);
            AssertingEnumerator enumerator = new AssertingEnumerator(tree.Root.Expression);

            //   =
            //  / \
            // a   1 

            enumerator.AssertNode(NodeKind.AssignmentExpression);
            enumerator.AssertNode(NodeKind.IdentifierExpression);
            enumerator.AssertToken(NodeKind.IdentifierToken, "a");
            enumerator.AssertToken(NodeKind.EqualsToken, TestingHelper.GetNodeKindText(NodeKind.EqualsToken));
            enumerator.AssertNode(NodeKind.LiteralExpression);
            enumerator.AssertToken(NodeKind.NumberToken, "1");
        }
    }
}