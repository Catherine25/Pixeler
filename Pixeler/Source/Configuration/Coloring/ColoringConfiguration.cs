using Pixeler.Source.Colors;
using Pixeler.Source.Drawing.Pixels;

namespace Pixeler.Source.Configuration.Coloring;

public struct ColoringConfiguration
{
    public PixelGrouping PixelGrouping { get; set; } = PixelGrouping.None;
    public Layoring Layoring { get; set; } = Layoring.Oil;
    public Func<ColorData, ColorData, ColorData, MixingResult> CalculateColor { get; set; }

    public ColoringConfiguration(PixelGrouping pixelGrouping, Layoring layoring)
    {
        PixelGrouping = pixelGrouping;
        Layoring = layoring;
    }
}
