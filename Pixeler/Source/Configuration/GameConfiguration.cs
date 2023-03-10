using Pixeler.Source.Colors;
using Pixeler.Source.Configuration.Coloring;
using Pixeler.Source.Images;
using Pixeler.Source.Services;
using Pixeler.Source.Services.Pixels;

namespace Pixeler.Source.Configuration;

public class GameConfiguration
{
    private readonly Bitmap _bitmap;

    public int GridResolution
    {
        get => _gridResolution;
        set
        {
            _gridResolution = value;

            int pixelResolution = _locatorService.CalculatePixelResolution(SquaredResolution, _gridResolution);

            var margin = _locatorService.CalculateLeftTopMargin(Size, SquaredResolution, pixelResolution / 2);

            Margin = margin;
            Padding = pixelResolution;
        }
    }
    private int _gridResolution;

    private Size Margin;
    private int Padding;

    public ColoringConfiguration ColoringConfiguration;
    public Func<ColorData, ColorData, ColorData, MixingResult> CalculateColor { get; internal set; }
    private readonly ILocatorService _locatorService;

    public GameConfiguration(Bitmap bitmap, ILocatorService locatorService)
    {
        _bitmap = bitmap;
        _locatorService = locatorService;
    }

    public ColorData GetPixel(Point point) => GetPixel((int)point.X, (int)point.Y);
    public ColorData GetPixel(int x, int y)
    {
        var real = _locatorService.CalculateRealPixelLocation(x, y, Margin, Padding);

        return _bitmap.GetPixel(real);
    }

    public Size Size => _bitmap.Size;
    public int SquaredResolution => _bitmap.SquaredResolution;
}
