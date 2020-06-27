using System;

namespace compiler {
    class Program {
        static void Main (string[] args) {
            string text = Console.ReadLine();
            Lexer lexer = new Lexer(text);
            while(true) {
                SyntaxToken token = lexer.NextToken();
                if(token.Kind == SyntaxKind.EndOfFileToken || token.Kind == SyntaxKind.BadToken)
                    break;

                Console.WriteLine($"Kind:{token.Kind}, Start:{token.Start} Text:{token.Text}, Value:{token.Value}");
            }
            Console.ReadKey();
        }
    }
}