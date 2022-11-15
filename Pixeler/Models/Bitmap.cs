using Pixeler.Models.Colors;
using SkiaSharp;

namespace Pixeler.Models;

public class Bitmap
{
    private readonly SKBitmap _bitmap;

    private Size _size;
    public Size Size
    {
        get => _size;
        set
        {
            _size.Width = Math.Min(_bitmap.Width, value.Width);
            _size.Height = Math.Min(_bitmap.Height, value.Height);
        }
    }

    public int SquaredResolution => (int)Math.Min(Size.Width, Size.Height);

    public Bitmap(SKBitmap bitmap)
    {
        _bitmap = bitmap;
        _size = new Size(bitmap.Width, bitmap.Height);
    }

    public ColorData GetPixel(Point point) => GetPixel((int)point.X, (int)point.Y);
    public ColorData GetPixel(int x, int y)
    {
        if (x < 0 || x > Size.Width)
            throw new ArgumentOutOfRangeException(nameof(x));

        if (y < 0 || y > Size.Height)
            throw new ArgumentOutOfRangeException(nameof(y));

        return new ColorData(_bitmap.GetPixel(x, y).ToString());
    }
}
