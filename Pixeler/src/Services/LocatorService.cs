namespace Pixeler.Services
{
    public class LocatorService : ILocatorService
    {
        public Point CalculateRealPixelLocation(int x, int y, Size margin, int padding)
        {
            var realPixelX = margin.Width + (x * padding);
            var realPixelY = margin.Height + (y * padding);

            return new Point(realPixelX, realPixelY);
        }

        public Size CalculateLeftTopMargin(Size bitmapSize, int squaredResolution, int pixelResolution)
        {
            int bitmapWidth = (int)bitmapSize.Width;
            int bitmapHeight = (int)bitmapSize.Height;

            // calculate cropped area size
            int croppedWidth = bitmapWidth - squaredResolution;
            int croppedHeight = bitmapHeight - squaredResolution;

            if (croppedWidth != 0 && croppedHeight != 0)
                throw new ArithmeticException("At least one dimension of the cropped image must have same size as the original image!");

            // if difference is odd, ignore the last pixel and make it even
            // to ease the calculations
            int normalizedCroppedWidth = Normalize(croppedWidth);
            int normalizedCroppedHeight = Normalize(croppedHeight);

            var margin = new Size
            {
                Width = normalizedCroppedWidth / 2,
                Height = normalizedCroppedHeight / 2
            };

            // shift by half of pixel resolution
            return ShiftToRightBottom(margin, pixelResolution / 2);
        }

        public int CalculatePixelResolution(int realSquaredResolution, int drawingResolution)
        {
            return realSquaredResolution / drawingResolution;
        }

        private Size ShiftToRightBottom(Size uhshifted, int pixelAreaSize)
        {
            return new Size
            {
                Width = SumIfNotZero(uhshifted.Width, pixelAreaSize),
                Height = SumIfNotZero(uhshifted.Height, pixelAreaSize)
            };
        }

        private int Normalize(int number) => number % 2 == 0 ? number : number--;
        private double SumIfNotZero(double number, double number2) => number == 0 ? 0 : number + number2;
    }
}
