using Jcompiler.Tokenizer;
using System;

namespace Jcompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            Lexer lexer = new Lexer(text);
            Token token = null;
            do
            {
                token = lexer.NextToken();
                Console.WriteLine(token.Kind.ToString());

            } while (token.Kind != TokenKind.EndOfFileToken && token.Kind != TokenKind.BadToken);
        }
    }
}
