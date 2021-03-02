namespace Jcompiler.Text
{
    public class TextLine
    {
        public TextLine(int start, int length, int lengthWithLineBreak)
        {
            Start = start;
            Length = length;
            LengthWithLineBreak = lengthWithLineBreak;
        }

        public int Start { get; }

        public int Length { get; }

        public int LengthWithLineBreak { get; }

        public int End => Start + Length;

        public TextSpan Span => new TextSpan(Start, Length);
    }
}