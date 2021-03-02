using Jcompiler.Syntax;
using Jcompiler.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Jcompiler.Tests
{
    [TestClass]
    public class LexerTest
    {
        private DiagnosticBag diagnostics;

        [TestInitialize]
        public void Initialize()
        {
            diagnostics = new DiagnosticBag();
        }

        [TestMethod]
        public void NextToken_CorrectTokenReturned()
        {
            foreach ((NodeKind kind, string text) expectedToken in GetExpectedTokens())
            {
                Token[] actualTokens = GetActualTokens(expectedToken.text, diagnostics).ToArray();
                Assert.AreEqual(1, actualTokens.Length);
                Assert.AreEqual(expectedToken.kind, actualTokens[0].Kind);
                Assert.AreEqual(expectedToken.text, actualTokens[0].Text);
            }
        }

        [TestMethod]
        public void NextToken_TokenDoesNotExist_BadTokenReturned()
        {
            Token[] actualTokens = GetActualTokens("@", diagnostics).ToArray();
            Assert.AreEqual(1, actualTokens.Length);
            Assert.AreEqual(NodeKind.BadToken, actualTokens[0].Kind);
            Assert.AreEqual("@", actualTokens[0].Text);
        }

        private static IEnumerable<Token> GetActualTokens(string text, DiagnosticBag diagnostics)
        {
            SourceText sourceText = SourceText.From(text);
            Lexer lexer = new Lexer(sourceText, diagnostics);

            while (true)
            {
                Token token = lexer.NextToken();
                if (token.Kind == NodeKind.EndOfFileToken)
                    break;

                yield return token;
            }
        }

        private static IEnumerable<(NodeKind kind, string text)> GetExpectedTokens()
        {
            return new[]
            {
                (NodeKind.PlusToken, "+"),
                (NodeKind.MinusToken, "-"),
                (NodeKind.StarToken, "*"),
                (NodeKind.SlashToken, "/"),
                (NodeKind.BangToken, "!"),
                (NodeKind.EqualsToken, "="),
                (NodeKind.AmpersandAmpersandToken, "&&"),
                (NodeKind.PipePipeToken, "||"),
                (NodeKind.EqualsEqualsToken, "=="),
                (NodeKind.BangEqualsToken, "!="),
                (NodeKind.OpenParenthesisToken, "("),
                (NodeKind.CloseParenthesisToken, ")"),
                (NodeKind.FalseKeyword, "false"),
                (NodeKind.TrueKeyword, "true"),
                (NodeKind.NumberToken, "1"),
                (NodeKind.NumberToken, "123"),
                (NodeKind.IdentifierToken, "a"),
                (NodeKind.IdentifierToken, "abc"),
            };
        }
    }
}