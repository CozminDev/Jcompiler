namespace Jcompiler.Syntax
{
    public static class Facts
    {

        public static int GetUnaryOperatorPrecedence(this NodeKind kind)
        {
            switch (kind)
            {
                case NodeKind.PlusToken:
                case NodeKind.MinusToken:
                case NodeKind.BangToken:
                    return 6;

                default:
                    return 0;
            }
        }

        public static int GetBinaryOperatorPrecedence(this NodeKind kind)
        {
            switch (kind)
            {
                case NodeKind.StarToken:
                case NodeKind.SlashToken:
                    return 5;

                case NodeKind.PlusToken:
                case NodeKind.MinusToken:
                    return 4;

                case NodeKind.EqualsEqualsToken:
                case NodeKind.BangEqualsToken:
                    return 3;

                case NodeKind.AmpersandAmpersandToken:
                    return 2;

                case NodeKind.PipePipeToken:
                    return 1;

                default:
                    return 0;
            }
        }

        public static NodeKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "true":
                    return NodeKind.TrueKeyword;
                case "false":
                    return NodeKind.FalseKeyword;
                default:
                    return NodeKind.IdentifierToken;
            }
        }
    }
}
