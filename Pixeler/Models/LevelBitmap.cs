using SkiaSharp;

namespace Pixeler.Models;

public class LevelBitmap : Bitmap
{
    public Size Size { get; private set; }
    private readonly Point _current;
    private readonly HashSet<Point> _completed;

    public LevelBitmap(SKBitmap bitmap, Size levelRectSize) : base(bitmap)
    {
        Size = levelRectSize;
        _completed = new HashSet<Point>();
    }
}
