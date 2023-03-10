using Pixeler.Models.Colors;

namespace Pixeler.Models;

public class ColorMatrix
{
    private readonly Bitmap _bitmap;
    private readonly Dictionary<Point, ColorData> _data;

    public ColorMatrix(Bitmap bitmap)
    {
        _bitmap = bitmap;
        _data = new Dictionary<Point, ColorData>();

        for (int x = 0; x < _bitmap.Size.Width; x++)
            for (int y = 0; y < _bitmap.Size.Height; y++)
                _data.Add(new Point(x, y), GetPixel(x, y));
    }

    public ColorData GetPixel(Point point) => _data[point];
    public ColorData GetPixel(int x, int y) => GetPixel(new Point(x, y));
}
