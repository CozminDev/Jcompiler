using Jcompiler.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jcompiler.Tests.Text
{
    [TestClass]
    public class SourceTextTest
    {
        [TestMethod]
        public void From_MultipleLines_ReturnsCorrectNumberOfLines()
        {
            string text = ".";
            AssertNumberOfLines(1, text);

            text = ".\r\n";
            AssertNumberOfLines(2, text);

            text = ".\r\n\r\n";
            AssertNumberOfLines(3, text);
        }

        private void AssertNumberOfLines(int number, string text)
        {
            SourceText sourceText = SourceText.From(text);
            Assert.AreEqual(number, sourceText.Lines.Count);
        }
    }
}