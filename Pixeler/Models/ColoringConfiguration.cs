using Pixeler.Models.Colors;
using Pixeler.Views;

namespace Pixeler.Models;

public class ColoringConfiguration
{
    private Bitmap _bitmap;

    public int? GridResolution;
    public int PixelPadding = 0;

    public Modes? Mode;
    public Func<ColorData, ColorData, ColorData> ColoringFunc { get; internal set; }

    public ColoringConfiguration(Bitmap bitmap)
    {
        _bitmap = bitmap;
    }

    public ColorData GetPixel(Point point) => _bitmap.GetPixel(point);
    public ColorData GetPixel(int x, int y) => _bitmap.GetPixel(x, y);
    public Size Size => _bitmap.Size;
    public int SquaredResolution => _bitmap.SquaredResolution;
}
