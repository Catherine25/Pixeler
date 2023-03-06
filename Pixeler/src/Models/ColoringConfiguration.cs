using Pixeler.Models.Colors;
using Pixeler.Services;
using Pixeler.Views;

namespace Pixeler.Models;

public class ColoringConfiguration
{
    private Bitmap _bitmap;

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

    public Modes? Mode;
    public Func<ColorData, ColorData, ColorData, MixingResult> CalculateColor { get; internal set; }
    private ILocatorService _locatorService;

    public ColoringConfiguration(Bitmap bitmap, ILocatorService locatorService)
    {
        _bitmap = bitmap;
        _locatorService = locatorService;
    }

    public ColorData GetPixel(Point point) => GetPixel((int)point.X, (int)point.Y);
    public ColorData GetPixel(int x, int y)
    {
        var real = _locatorService.CalculateRealPixelLocation(x, y, Margin, Padding);

        this.Log($"{x}, {y} -> {real.X}, {real.Y}");

        return _bitmap.GetPixel(real);
    }

    public Size Size => _bitmap.Size;
    public int SquaredResolution => _bitmap.SquaredResolution;
}
