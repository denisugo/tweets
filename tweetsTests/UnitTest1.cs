using Xunit;
using tweets;
using SkiaSharp;

namespace tweetsTests;

public class SplitText
{
    string oneLineString = "Simple String";
    string twoLinesString = "Omnis omnis voluptatem excepturi aut nesciunt.";
    SKPaint paint = new SKPaint(new SKFont(SKTypeface.FromFamilyName("sans-serif", SKFontStyleWeight.Light, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright), 17));
    int lineLengthLimit = 200;

    [Fact]
    public void OneLine()
    {
        LinesData linesData = Utils.SplitText(oneLineString, paint, lineLengthLimit);

        Assert.Equal(1, linesData.Lines.Length);
    }
    [Fact]
    public void TwoLine()
    {
        LinesData linesData = Utils.SplitText(twoLinesString, paint, lineLengthLimit);

        Assert.Equal(2, linesData.Lines.Length);
    }
}