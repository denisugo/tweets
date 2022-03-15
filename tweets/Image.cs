using SkiaSharp;

namespace tweets
{

    enum ThemeType
    {
        LIGHT,
        DARK
    }
    class Image
    {


        private readonly Tweet _tweet;
        private readonly ThemeType _theme;
        private readonly SKBitmap profileImage;
        private readonly int marginX = 15;
        private readonly int marginY = 15;
        private readonly int circleRadius = 30;
        private readonly int height;
        private readonly int width = 400;
        private readonly string[] lines;
        private readonly SKPaint textPaint;


        public Image(Tweet tweet, ThemeType theme = ThemeType.LIGHT)
        {
            _tweet = tweet;
            _theme = theme;

            // Getting profile image 
            using (SKBitmap bitmap = SKBitmap.Decode(tweet.ImageSrc))
            {
                profileImage = bitmap.Resize(new SKImageInfo(circleRadius * 2, circleRadius * 2), SKFilterQuality.High);
            }

            // Setting up tweet text
            using (SKTypeface typeface = SKTypeface.FromFamilyName("sans-serif", SKFontStyleWeight.Light, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright))
            {
                // Font
                const int fontSize = 17;
                SKFont font = new SKFont(typeface, fontSize);

                // Color
                SKColor color = SKColor.Parse("14171A");

                // Paint
                textPaint = new SKPaint(font);
                textPaint.IsAntialias = true;
                textPaint.Color = color;

                // Limits
                int lineLengthLimit = width - (2 * marginX);
                LinesData ld = Utils.SplitText(tweet.Text, textPaint, lineLengthLimit);
                lines = ld.Lines;
                height = ((int)(ld.TextHeight)) + marginY * 2 + 110;
            }
        }

        public void MakeTweet()
        {

            using (SKSurface surface = SKSurface.Create(new SKImageInfo(width, height)))
            {
                // Start time counter
                var watch = System.Diagnostics.Stopwatch.StartNew();

                using (SKCanvas canvas = surface.Canvas)
                {

                    UseBackground(canvas);
                    UseName(canvas);
                    UseProfileImage(canvas);
                    UseName(canvas);
                    UseText(canvas);
                    UseLikesAndRetweets(canvas);
                }



                watch.Stop();
                long elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("elapsed: " + elapsedMs + "ms");

                Save(surface);
            }

        }

        private void Save(SKSurface surface)
        {
            // Saving image
            SKImage image = surface.Snapshot();
            SkiaSharp.SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            using (FileStream stream = File.OpenWrite(Path.Combine("./", "1.png")))
            {
                // save the data to a stream
                data.SaveTo(stream);
                Console.WriteLine("Saved");
            }
        }

        private void UseBackground(SKCanvas canvas)
        {

            SKColor color;

            if (_theme == ThemeType.LIGHT)
            {
                color = SKColor.Parse("F5F8FA");
            }
            else
            {
                //TODO: Change color theme
                color = SKColor.Parse("000000");
            }

            canvas.DrawColor(color);

        }
        private void UseName(SKCanvas canvas)
        {
            //TODO: Add color theme
            // Colors
            SKColor nameColor = SKColor.Parse("14171A");
            SKColor nicknameColor = SKColor.Parse("657786");

            // Font
            const int fontSizeName = 16;
            const int fontSizeNickname = 15;
            SKTypeface typefaceName = SKTypeface.FromFamilyName("sans-serif", SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
            SKTypeface typefaceNickname = SKTypeface.FromFamilyName("sans-serif", SKFontStyleWeight.Light, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
            SKFont fontName = new SKFont(typefaceName, fontSizeName);
            SKFont fontNickname = new SKFont(typefaceNickname, fontSizeNickname);

            const int textMarginX = 10;
            // Name
            SKPaint namePaint = new SKPaint(fontName);
            namePaint.Color = nameColor;
            namePaint.IsAntialias = true;


            SKPoint namePoint = new SKPoint(marginX + circleRadius * 2 + textMarginX, marginY + circleRadius);

            // Nickname
            SKPaint nicknamePaint = new SKPaint(fontNickname);
            nicknamePaint.Color = nicknameColor;
            nicknamePaint.IsAntialias = true;

            SKPoint nicknamePoint = new SKPoint(marginX + circleRadius * 2 + textMarginX, nicknamePaint.FontSpacing + marginY + circleRadius);

            // Actual drawing
            canvas.DrawText(_tweet.Name, namePoint, namePaint);
            canvas.DrawText($"@{_tweet.Nickname}", nicknamePoint, nicknamePaint);



        }

        private void UseProfileImage(SKCanvas canvas)
        {
            SKBitmap mask = new SKBitmap(circleRadius * 2, circleRadius * 2);

            using (SKCanvas secondCanvas = new SKCanvas(mask))
            {
                SKPaint paint = new SKPaint();
                paint.IsAntialias = true;
                paint.Color = SKColor.Parse("F5F8FA");
                paint.BlendMode = SKBlendMode.Xor;

                secondCanvas.Clear();
                secondCanvas.DrawRect(new SKRect(0, 0, circleRadius * 2, circleRadius * 2), paint);
                secondCanvas.DrawCircle(circleRadius, circleRadius, circleRadius, paint);
            }

            canvas.DrawBitmap(profileImage, marginX, marginY);
            canvas.DrawBitmap(mask, marginX, marginY);

        }
        private void UseText(SKCanvas canvas)
        {
            float y = 110;
            foreach (string line in lines)
            {
                canvas.DrawText(line, marginX, y, textPaint);
                y += textPaint.FontSpacing;
            }
        }
        private void UseDate(SKCanvas canvas)
        {
            //TODO: To be added
        }
        private void UseLikesAndRetweets(SKCanvas canvas)
        {
            //TODO: To be added
        }



    }
};