namespace Pixeler.Configuration.Coloring;

public struct ColoringConfiguration
{
    public PixelGrouping PixelGrouping { get; set; } = PixelGrouping.None;
    public Layoring Layoring { get; set; } = Layoring.Oil;

    public ColoringConfiguration(PixelGrouping pixelGrouping, Layoring layoring)
    {
        PixelGrouping = pixelGrouping;
        Layoring = layoring;
    }
}
