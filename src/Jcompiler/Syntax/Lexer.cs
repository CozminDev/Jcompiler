using System.Collections.Generic;

namespace Jcompiler.Syntax
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

        public char Lookahead
        {
            get
            {
                if (position+1 > text.Length - 1)
                    return '\0';

                return text[position+1];
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
                return new Token(NodeKind.EndOfFileToken, position, "\0", null);

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

                return new Token(NodeKind.NumberToken, start, txt, value);
            }


            if (char.IsWhiteSpace(Current))
            {
                var start = position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = position - start;
                var txt = text.Substring(start, length);
                return new Token(NodeKind.WhitespaceToken, start, txt, null);
            }

            if (char.IsLetter(Current))
            {
                var start = position;

                while (char.IsLetter(Current))
                    Next();

                var length = position - start;
                var txt = text.Substring(start, length);

                if (txt == "true")
                {
                    return new Token(NodeKind.TrueKeyword, start, txt, null);
                }
                else if (txt == "false")
                {
                    return new Token(NodeKind.FalseKeyword, start, txt, null);
                }
                else
                {
                    diagnostics.Add($"The keyword {txt} isn't valid Keyword.");
                }

            }

            switch (Current)
            {
                case '+':
                    return new Token(NodeKind.PlusToken, position++, "+", null);
                case '-':
                    return new Token(NodeKind.MinusToken, position++, "-", null);
                case '*':
                    return new Token(NodeKind.StarToken, position++, "*", null);
                case '/':
                    return new Token(NodeKind.SlashToken, position++, "/", null);
                case '(':
                    return new Token(NodeKind.OpenParenthesisToken, position++, "(", null);
                case ')':
                    return new Token(NodeKind.CloseParenthesisToken, position++, ")", null);
                case '!':
                    return new Token(NodeKind.BangToken, position++, "!", null);
                case '|':
                    {
                       if(Lookahead == '|')
                        {
                            int start = position;
                            position += 2;
                            return new Token(NodeKind.PipePipeToken, start, "||", null);
                        }                           
                        break;
                    }

                case '&':
                    {                       
                        if (Lookahead == '&')
                        {
                            int start = position;
                            position += 2;
                            return new Token(NodeKind.AmpersandAmpersandToken, start, "&&", null);
                        }                           
                        break;
                    }
            }


            diagnostics.Add($"ERROR: bad character input: '{Current}'");
            return new Token(NodeKind.BadToken, position++, text.Substring(position - 1, 1), null);
        }

        private void Next()
        {
            position++;
        }
    }
}