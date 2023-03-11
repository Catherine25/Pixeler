using Pixeler.Source.Colors;
using Pixeler.Source.Configuration;
using Pixeler.Source.Configuration.Coloring;
using Pixeler.Source.Configuration.Images;

namespace Pixeler.Source.Drawing.Pixels;

public class PaletteService
{
    public static IEnumerable<ColorData> Build(GameConfiguration gameConfiguration)
    {
        var layoring = gameConfiguration.ColoringConfiguration.Layoring;
        var pixelGrouping = gameConfiguration.ColoringConfiguration.PixelGrouping;

        if (layoring == Layoring.Oil && pixelGrouping == PixelGrouping.None)
            return BuildForDirectMode(gameConfiguration.ColorMatrix);

        if (layoring == Layoring.Acryllic && pixelGrouping == PixelGrouping.None)
            return BuildForDirectAcryllicMode(gameConfiguration.ColorMatrix);

        throw new NotSupportedException();
    }

    public static IEnumerable<ColorData> BuildForDirectMode(ColorMatrix colorMatrix)
    {
        var palette = new HashSet<ColorData>();
        int gridResolution = colorMatrix.GridResolution;

        for (int x = 0; x < gridResolution; x++)
            for (int y = 0; y < gridResolution; y++)
            {
                var pixel = colorMatrix.GetPixel(x, y);
                palette.Add(pixel);
            }

        return palette;
    }

    public static IEnumerable<ColorData> BuildForDirectAcryllicMode(ColorMatrix colorMatrix)
    {
        var palette = BuildForDirectMode(colorMatrix);

        return palette.OrderByDescending(x => x.L);
    }
}
