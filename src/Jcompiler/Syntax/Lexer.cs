using Jcompiler.Text;

namespace Jcompiler.Syntax
{
    public class Lexer
    {
        private readonly SourceText text;
        private DiagnosticBag diagnostics;

        private int position;
        private int start;
        private NodeKind kind;
        private object value;

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
                if (position + 1 > text.Length - 1)
                    return '\0';

                return text[position + 1];
            }
        }

        public Lexer(SourceText text, DiagnosticBag diagnostics)
        {
            this.text = text;
            this.diagnostics = diagnostics;
        }

        public Token NextToken()
        {
            start = position;
            kind = NodeKind.BadToken;
            value = null;

            switch (Current)
            {
                case '\0':
                    kind = NodeKind.EndOfFileToken;
                    break;
                case '+':
                    kind = NodeKind.PlusToken;
                    position++;
                    break;
                case '-':
                    kind = NodeKind.MinusToken;
                    position++;
                    break;
                case '*':
                    kind = NodeKind.StarToken;
                    position++;
                    break;
                case '/':
                    kind = NodeKind.SlashToken;
                    position++;
                    break;
                case '(':
                    kind = NodeKind.OpenParenthesisToken;
                    position++;
                    break;
                case ')':
                    kind = NodeKind.CloseParenthesisToken;
                    position++;
                    break;
                case '&':
                    if (Lookahead == '&')
                    {
                        kind = NodeKind.AmpersandAmpersandToken;
                        position += 2;
                    }
                    break;
                case '|':
                    if (Lookahead == '|')
                    {
                        kind = NodeKind.PipePipeToken;
                        position += 2;
                    }
                    break;
                case '=':
                    position++;
                    if (Current != '=')
                    {
                        kind = NodeKind.EqualsToken;
                    }
                    else
                    {
                        kind = NodeKind.EqualsEqualsToken;
                        position++;
                    }
                    break;
                case '!':
                    position++;
                    if (Current != '=')
                    {
                        kind = NodeKind.BangToken;
                    }
                    else
                    {
                        kind = NodeKind.BangEqualsToken;
                        position++;
                    }
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    ReadNumber();
                    break;
                case ' ':
                case '\t':
                case '\n':
                case '\r':
                    ReadWhiteSpace();
                    break;
                default:
                    if (char.IsLetter(Current))
                    {
                        ReadIdentifierOrKeyword();
                    }
                    else if (char.IsWhiteSpace(Current))
                    {
                        ReadWhiteSpace();
                    }
                    else
                    {
                        diagnostics.ReportBadCharacter(position, Current);
                        position++;
                    }
                    break;
            }

            int length = position - start;
            string txt = Facts.GetText(kind);
            if (txt == null)
                txt = text.ToString(start, length);

            return new Token(kind, start, txt, value);
        }

        private void ReadWhiteSpace()
        {
            while (char.IsWhiteSpace(Current))
                position++;

            kind = NodeKind.WhitespaceToken;
        }

        private void ReadNumber()
        {
            while (char.IsDigit(Current))
                position++;

            int length = position - start;
            string txt = text.ToString(start, length);
            if (!int.TryParse(txt, out int val))
                diagnostics.ReportInvalidNumber(new TextSpan(start, length), txt, typeof(int));

            value = val;
            kind = NodeKind.NumberToken;
        }

        private void ReadIdentifierOrKeyword()
        {
            while (char.IsLetter(Current))
                position++;

            int length = position - start;
            string txt = text.ToString(start, length);
            kind = Facts.GetKeywordKind(txt);
        }
    }
}