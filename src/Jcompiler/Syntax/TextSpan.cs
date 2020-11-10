namespace Jcompiler.Syntax
{
    public class TextSpan
    {
        public TextSpan(int position, int length)
        {
            Position = position;
            Length = length;
        }

        public int Position { get; }
        public int Length { get; }
    }
}