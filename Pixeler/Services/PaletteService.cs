using Pixeler.Models;
using Pixeler.Models.Colors;

namespace Pixeler.Services;

public static class PaletteService
{
    public static HashSet<ColorData> Build(Bitmap bitmap)
    {
        var colorHashes = new HashSet<string>();
        var palette = new HashSet<ColorData>();

        for (int x = 0; x < bitmap.Size.Width; x++)
            for (int y = 0; y < bitmap.Size.Height; y++)
            {
                var pixel = bitmap.GetPixel(x, y);
                if (colorHashes.Add(pixel.Hex))
                    palette.Add(pixel);
            }

        return palette;
    }
}
