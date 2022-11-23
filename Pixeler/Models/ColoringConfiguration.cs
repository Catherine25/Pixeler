using Pixeler.Models.Colors;
using Pixeler.Views;

namespace Pixeler.Models;

public class ColoringConfiguration
{
    public Bitmap Bitmap;
    public int? GridResolution;

    public Modes? Mode;
    public Func<ColorData, ColorData, ColorData> ColoringFunc { get; internal set; }
}
