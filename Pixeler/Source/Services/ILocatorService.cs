namespace Pixeler.Source.Services;

public interface ILocatorService
{
    public Point CalculateRealPixelLocation(int x, int y, Size margin, int padding);
    public Size CalculateLeftTopMargin(Size bitmapSize, int squaredResolution, int pixelResolution);
    public int CalculatePixelResolution(int realSquaredResolution, int drawingResolution);
}
