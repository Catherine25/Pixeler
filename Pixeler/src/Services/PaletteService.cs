using Pixeler.Models;
using Pixeler.Models.Colors;
using Pixeler.Views;

namespace Pixeler.Services;

public static class PaletteService
{
    public static IEnumerable<ColorData> Build(ColoringConfiguration coloringConfiguration)
    {
        if (coloringConfiguration.Mode == Modes.Direct)
            return BuildForDirectMode(coloringConfiguration);

        if (coloringConfiguration.Mode == Modes.LayeredBigToSmall_Acryllic)
            return BuildForLayeredBigToSmallAcryllicMode(coloringConfiguration);

        throw new NotSupportedException();
    }

    public static IEnumerable<ColorData> BuildForDirectMode(ColoringConfiguration coloringConfiguration)
    {
        var palette = new HashSet<ColorData>();

        for (int x = 0; x < coloringConfiguration.GridResolution; x++)
            for (int y = 0; y < coloringConfiguration.GridResolution; y++)
            {
                var pixel = coloringConfiguration.GetPixel(x, y);
                palette.Add(pixel);
            }

        return palette;
    }

    public static IEnumerable<ColorData> BuildForLayeredBigToSmallAcryllicMode(ColoringConfiguration coloringConfiguration)
    {
        var palette = new HashSet<ColorData>();

        for (int x = 0; x < coloringConfiguration.GridResolution; x++)
            for (int y = 0; y < coloringConfiguration.GridResolution; y++)
            {
                var pixel = coloringConfiguration.GetPixel(x, y);
                palette.Add(pixel);
            }

        return palette.OrderByDescending(x => x.L);
    }
}
