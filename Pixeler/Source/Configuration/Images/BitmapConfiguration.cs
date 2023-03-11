using Pixeler.Source.Colors;
using Pixeler.Source.Services;

namespace Pixeler.Source.Configuration.Images;

public class BitmapConfiguration
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

    private readonly ILocatorService _locatorService;

    public BitmapConfiguration(Bitmap bitmap, ILocatorService locatorService)
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
