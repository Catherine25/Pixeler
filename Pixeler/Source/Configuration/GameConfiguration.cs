using Pixeler.Source.Configuration.Coloring;
using Pixeler.Source.Configuration.Images;

namespace Pixeler.Source.Configuration;

public class GameConfiguration
{
    public readonly ColorMatrix ColorMatrix;
    public readonly ColoringConfiguration ColoringConfiguration;
    public readonly BitmapConfiguration BitmapConfiguration;

    public GameConfiguration(BitmapConfiguration bitmapConfiguration,
        ColoringConfiguration coloringConfiguration)
    {
        BitmapConfiguration = bitmapConfiguration;
        ColoringConfiguration = coloringConfiguration;

        ColorMatrix = new(BitmapConfiguration);
    }
}
