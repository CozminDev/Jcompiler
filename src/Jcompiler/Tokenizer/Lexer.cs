using System.Collections.Generic;

namespace Jcompiler.Tokenizer
{
    public class Lexer
    {
        private readonly string text;
        private int position;
        private List<string> diagnostics;

        public char Current
        {
            get
            {
                if (position > text.Length - 1)
                    return '\0';

                return text[position];
            }
        }

        public Lexer(string text)
        {
            this.text = text;
            diagnostics = new List<string>();
        }

        public Token NextToken()
        {
            if (position >= text.Length)
                return new Token(TokenKind.EndOfFileToken, position, "\0", null);

            if (char.IsDigit(Current))
            {
                int start = position;
                while (char.IsDigit(Current))
                {
                    Next();
                }

                int length = position - start;
                string txt = text.Substring(start, length);
                if (!int.TryParse(txt, out int value))
                    diagnostics.Add($"The number {txt} isn't valid Int32.");

                return new Token(TokenKind.NumberToken, start, txt, value);
            }


            if (char.IsWhiteSpace(Current))
            {
                var start = position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = position - start;
                var txt = text.Substring(start, length);
                return new Token(TokenKind.WhitespaceToken, start, txt, null);
            }

            if (Current == '+')
                return new Token(TokenKind.PlusToken, position++, "+", null);
            else if (Current == '-')
                return new Token(TokenKind.MinusToken, position++, "-", null);
            else if (Current == '*')
                return new Token(TokenKind.StarToken, position++, "*", null);
            else if (Current == '/')
                return new Token(TokenKind.SlashToken, position++, "/", null);
            else if (Current == '(')
                return new Token(TokenKind.OpenParenthesisToken, position++, "(", null);
            else if (Current == ')')
                return new Token(TokenKind.CloseParenthesisToken, position++, ")", null);

            diagnostics.Add($"ERROR: bad character input: '{Current}'");
            return new Token(TokenKind.BadToken, position++, text.Substring(position - 1, 1), null);
        }

        private void Next()
        {
            position++;
        }
    }
}