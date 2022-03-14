using SkiaSharp;

namespace tweets
{
    struct Coordinates
    {
        public float X { get; init; }
        public float Y { get; init; }
    }

    struct LinesData
    {
        public string[] Lines { get; init; }
        public float TextHeight { get; init; }
    }
    //TODO: add height measurement
    class Utils
    {
        /// <summary>
        /// Method <c>SplitText</c> calculates height of passed text and return it with splited lines.
        /// </summary>
        public static LinesData SplitText(string text, SKPaint paint, int lineLengthLimit)
        {

            // Init parameters
            float textHeight = 0;
            List<string> lines = new List<string>();
            float lineLength = 0f;
            string line = "";
            string[] splittedText = text.Split(' ');
            for (int index = 0; index < splittedText.Length; index++)
            {
                string word = splittedText[index];
                string wordWithSpace = word + " ";
                float wordWithSpaceLength = paint.MeasureText(wordWithSpace);
                if (lineLength + wordWithSpaceLength > lineLengthLimit)
                {
                    lines.Add(line);
                    textHeight += paint.FontSpacing;
                    line = "" + wordWithSpace;
                    lineLength = wordWithSpaceLength;
                }
                else
                {
                    line += wordWithSpace;
                    if (index == splittedText.Length - 1)
                    {
                        lines.Add(line);
                        textHeight += paint.FontSpacing;
                    }
                    else
                    {

                        lineLength += wordWithSpaceLength;
                    }
                }
            }
            return new LinesData
            {
                Lines = lines.ToArray(),
                TextHeight = textHeight
            };

        }
    }
}