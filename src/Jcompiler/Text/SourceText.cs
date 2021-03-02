using System.Collections.Generic;

namespace Jcompiler.Text
{
    public class SourceText
    {
        private readonly string text;

        public SourceText(string text)
        {
            this.text = text;
            Lines = BuildTextLines(text);
        }

        public List<TextLine> Lines { get; }

        public char this[int index] => text[index];

        public int Length => text.Length;

        public static SourceText From(string text)
        {
            return new SourceText(text);
        }

        public override string ToString()
        {
            return text;
        }

        public string ToString(int start, int length)
        {
            return text.Substring(start, length);
        }

        public int GetLineIndexByPosition(int position)
        {
            int start = 0;
            int end = Lines.Count - 1;

            while (start <= end)
            {
                int mid = start + (end - start) / 2;

                if (LineContainsPosition(mid, position))
                    return mid;

                if (Lines[mid].Start > position)
                    end = mid - 1;
                else
                    start = mid + 1;
            }

            return -1;
        }

        private bool LineContainsPosition(int lineIndex, int position)
        {
            return Lines[lineIndex].Start <= position && Lines[lineIndex].End >= position;
        }

        private List<TextLine> BuildTextLines(string text)
        {
            List<TextLine> lines = new List<TextLine>();

            int position = 0;
            int start = 0;

            while (position < text.Length)
            {
                int lineBreakWidth = GetLineBreakWidth(position);

                if(lineBreakWidth == 0)
                    position++;

                if (lineBreakWidth > 0)
                {
                    int length = position - start;
                    int lengthWithLineBreak = position - start + lineBreakWidth;

                    lines.Add(new TextLine(start, length, lengthWithLineBreak));

                    position += lineBreakWidth;
                    start = position;
                }
            }

            if (position >= start)
                lines.Add(new TextLine(start, position, 0));

            return lines;
        }

        private int GetLineBreakWidth(int position)
        {
            char c = text[position];
            char l = position + 1 >= text.Length ? '\0' : text[position + 1];

            if (c == '\r' && l == '\n')
                return 2;
            else if (c == '\n')
                return 1;
            else return 0;
        }
    }
}