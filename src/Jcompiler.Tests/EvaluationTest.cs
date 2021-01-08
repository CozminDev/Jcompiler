using Jcompiler.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Jcompiler.Tests
{
    [TestClass]
    public class EvaluationTest
    {
        [TestMethod]
        public void Evaluate_PlusExpression_ReturnsCorrectResult()
        {
            string text = "1+2";
            Parser parser = new Parser(text);
            ExpressionTree tree = parser.Parse();
            Dictionary<string, object> symbolTable = new Dictionary<string, object>();
            Compilation compilation = new Compilation(tree, symbolTable);

            EvaluationResult result = compilation.Evaluate();

            Assert.AreEqual(3, result.Result);
        }

        [TestMethod]
        public void Evaluate_PlusAndTimesExpression_ReturnsCorrectResult()
        {
            string text = "1+2*5";
            Parser parser = new Parser(text);
            ExpressionTree tree = parser.Parse();
            Dictionary<string, object> symbolTable = new Dictionary<string, object>();
            Compilation compilation = new Compilation(tree, symbolTable);

            EvaluationResult result = compilation.Evaluate();

            Assert.AreEqual(11, result.Result);
        }

        [TestMethod]
        public void Evaluate_ParenthesizedExpression_ReturnsCorrectResult()
        {
            string text = "(1+2)*5";
            Parser parser = new Parser(text);
            ExpressionTree tree = parser.Parse();
            Dictionary<string, object> symbolTable = new Dictionary<string, object>();
            Compilation compilation = new Compilation(tree, symbolTable);

            EvaluationResult result = compilation.Evaluate();

            Assert.AreEqual(15, result.Result);
        }

        [TestMethod]
        public void Evaluate_AssignmentExpression_ReturnsCorrectResult()
        {
            string text = "a = 1";
            Parser parser = new Parser(text);
            ExpressionTree tree = parser.Parse();
            Dictionary<string, object> symbolTable = new Dictionary<string, object>();
            Compilation compilation = new Compilation(tree, symbolTable);

            EvaluationResult result = compilation.Evaluate();

            Assert.AreEqual(1, result.Result);

            text = "a";
            parser = new Parser(text);
            tree = parser.Parse();
            compilation = new Compilation(tree, symbolTable);
            result = compilation.Evaluate();

            Assert.AreEqual(1, result.Result);
        }
    }
}