namespace compiler {
    public class Lexer {
        private readonly string _text;
        private int _position;

        public Lexer (string text) {
            this._text = text;
        }

        private char Current {
            get {
                if (_position >= _text.Length)
                    return '\0';

                return _text[_position];
            }
        }

        private void Next () {
            _position++;
        }

        public SyntaxToken NextToken () {
            if(char.IsDigit(Current)){
                int start = _position;
                
                while(char.IsDigit(Current)){
                    Next();
                }

                int length = _position - start;
                string text = _text.Substring(start, length);
                int.TryParse(text, out int value);
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if(char.IsWhiteSpace(Current)){
                int start = _position;
                
                while(char.IsWhiteSpace(Current)){
                    Next();
                }

                int length = _position - start;
                string text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
            }

            if (Current == '+')
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
            else if (Current == '-')
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            else if (Current == '*')
                return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
            else if (Current == '/')
                return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
            else if (Current == '(')
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
            else if (Current == ')')
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);

            if (Current == '\0')
                return new SyntaxToken (SyntaxKind.EndOfFileToken, _position, "\0", null);

            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);            
        }
    }

    public class SyntaxToken {
        public SyntaxToken (SyntaxKind kind, int start, string text, object value) {
            Kind = kind;
            Start = start;
            Text = text;
            Value = value;
        }

        public SyntaxKind Kind { get; }
        public int Start { get; }
        public string Text { get; }
        public object Value { get; }
    }

    public enum SyntaxKind {
        EndOfFileToken,
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        BadToken
    }
}