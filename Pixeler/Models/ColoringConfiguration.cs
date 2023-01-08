using Pixeler.Models.Colors;
using Pixeler.Views;

namespace Pixeler.Models;

public class ColoringConfiguration
{
    public Bitmap Bitmap { set => bitmap = value; }
    private Bitmap bitmap;

    public int? GridResolution;

    public Modes? Mode;
    public Func<ColorData, ColorData, ColorData> ColoringFunc { get; internal set; }

    public ColorData GetPixel(Point point) => bitmap.GetPixel(point);
    public ColorData GetPixel(int x, int y) => bitmap.GetPixel(x, y);
    public Size Size => bitmap.Size;
    public int SquaredResolution => bitmap.SquaredResolution;
}
