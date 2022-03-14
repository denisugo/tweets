using SkiaSharp;


namespace tweets
{


    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            Tweet tweet = new Tweet
            {
                Name = "Tweet",
                Nickname = "Tweeter",
                Text = "Voluptas blanditiis minima occaecati id. Fugit est qui quia non. Cum eos est explicabo id. Aperiam asperiores consequuntur tempore.",
                ImageSrc = "./public/file.jpg"
            };

            Image tweetImage = new Image(tweet);
            tweetImage.MakeTweet();



        }



        private static void Draw(SKSurface surface)
        {
            // Parameters
            // Circle
            SKColor redColor = SKColor.Parse("ff0000");
            SKPaint paint = new SKPaint();
            paint.Color = redColor;
            paint.IsAntialias = true;

            // Text
            const string text = "My test";
            SKPoint point = new SKPoint(150, 250);
            SKTypeface typeface = SKTypeface.FromFamilyName("sans-serif");
            SKColor greenColor = SKColor.Parse("398557");
            SKFont font = new SKFont(typeface, 24);
            SKPaint textPaint = new SKPaint(font);
            textPaint.Color = greenColor;
            textPaint.IsAntialias = true;
            float width = textPaint.MeasureText(text);

            SKCanvas canvas = surface.Canvas;

            // Darwing items
            canvas.DrawCircle(100, 100, 100, paint);
            canvas.DrawText(text, point, textPaint);
            canvas.DrawText(text, new SKPoint(150 + width + 5, 250), textPaint);
        }

        private static void MeasuteRunTime(Action<SKSurface> callback, SKSurface surface)
        {
            // Start time counter
            var watch = System.Diagnostics.Stopwatch.StartNew();
            callback(surface);
            // Stop time counter
            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("elapsed: " + elapsedMs + "ms");
        }
    }

};