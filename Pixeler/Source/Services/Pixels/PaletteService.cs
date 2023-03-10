using Pixeler.Source.Colors;
using Pixeler.Source.Configuration;
using Pixeler.Source.Configuration.Coloring;

namespace Pixeler.Source.Services.Pixels;

public class PaletteService
{
    public static IEnumerable<ColorData> Build(GameConfiguration gameConfiguration)
    {
        var layoring = gameConfiguration.ColoringConfiguration.Layoring;
        var pixelGrouping = gameConfiguration.ColoringConfiguration.PixelGrouping;

        if (layoring == Layoring.Oil && pixelGrouping == PixelGrouping.None)
            return BuildForDirectMode(gameConfiguration);

        if (layoring == Layoring.Acryllic && pixelGrouping == PixelGrouping.None)
            return BuildForDirectAcryllicMode(gameConfiguration);

        throw new NotSupportedException();
    }

    public static IEnumerable<ColorData> BuildForDirectMode(GameConfiguration gameConfiguration)
    {
        var palette = new HashSet<ColorData>();
        int gridResolution = gameConfiguration.GridResolution;

        for (int x = 0; x < gridResolution; x++)
            for (int y = 0; y < gridResolution; y++)
            {
                var pixel = gameConfiguration.GetPixel(x, y);
                palette.Add(pixel);
            }

        return palette;
    }

    public static IEnumerable<ColorData> BuildForDirectAcryllicMode(GameConfiguration gameConfiguration)
    {
        var palette = BuildForDirectMode(gameConfiguration);

        return palette.OrderByDescending(x => x.L);
    }
}
