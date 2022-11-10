using SkiaSharp;

namespace Pixeler.Extensions;

public static class SKBitmapExtensions
{
    public static Size GetSize(this SKBitmap bitmap) =>
        new(bitmap.Width, bitmap.Height);
}
