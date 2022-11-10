using Pixeler.Extensions;
using Pixeler.Models.Colors;
using SkiaSharp;

namespace Pixeler.Models;

public class Bitmap
{
    private readonly SKBitmap _bitmap;
    private Rect _rect;

    protected Rect Rect
    {
        get => _rect;
        set
        {
            _rect.X = value.X;
            _rect.Y = value.Y;
            _rect.Width = Math.Min(_bitmap.Width, value.Width);
            _rect.Height = Math.Min(_bitmap.Height, value.Height);
        }
    }

    public Bitmap(SKBitmap bitmap)
    {
        _bitmap = bitmap;
        _rect = new Rect(new Point(0,0), bitmap.GetSize());
    }

    protected ColorData GetPixel(Point point) => GetPixel((int)point.X, (int)point.Y);
    protected ColorData GetPixel(int x, int y)
    {
        if (x < 0 || x > _rect.Width)
            throw new ArgumentOutOfRangeException(nameof(x));

        if (y < 0 || y > _rect.Height)
            throw new ArgumentOutOfRangeException(nameof(y));

        return new ColorData(_bitmap.GetPixel(x, y).ToString());
    }
}
