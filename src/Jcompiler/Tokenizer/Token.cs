namespace Jcompiler.Tokenizer
{
    public class Token
    {
        public Token(TokenKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public TokenKind Kind { get; set; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; set; }
    }

    public enum TokenKind
    {
        NumberToken,
        PlusToken,
        MinusToken,
        WhitespaceToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        BadToken,
        EndOfFileToken
    }
}
