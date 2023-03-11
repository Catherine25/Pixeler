using Pixeler.Source.Colors;

namespace Pixeler.Source.Configuration.Images;

public class ColorMatrix
{
    private readonly BitmapConfiguration _bitmapConfiguration;
    private readonly Dictionary<Point, ColorData> _data;

    public ColorMatrix(BitmapConfiguration bitmapConfiguration)
    {
        _data = new Dictionary<Point, ColorData>();
        _bitmapConfiguration = bitmapConfiguration;

        for (int x = 0; x < _bitmapConfiguration.GridResolution; x++)
            for (int y = 0; y < _bitmapConfiguration.GridResolution; y++)
            {
                var point = new Point(x, y);
                _data.Add(point, _bitmapConfiguration.GetPixel(point));
            }
    }

    public ColorData GetPixel(Point point) => _data[point];
    public ColorData GetPixel(int x, int y) => GetPixel(new Point(x, y));
    public int GridResolution => _bitmapConfiguration.GridResolution;
}
