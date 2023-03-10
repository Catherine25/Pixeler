using SkiaSharp;

namespace Pixeler.Source.Images;

/// <summary>
/// Service for loading <see cref="Bitmap"/> from the storage.
/// </summary>
public interface ILoaderService
{
    public Task<Bitmap> LoadBitmapFromStorage();
}

public class BitmapLoadingService : ILoaderService
{
    public async Task<Bitmap> LoadBitmapFromStorage()
    {
        var file = await SelectFile();

        using Stream fileStream = await file.OpenReadAsync();

        var bitmap = new Bitmap(SKBitmap.Decode(fileStream));

        return bitmap;
    }

    private static async Task<FileResult> SelectFile()
    {
        var result = await FilePicker.Default.PickAsync(new PickOptions());
        if (result != null)
        {
            if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
            {
                using var stream = await result.OpenReadAsync();
                var image = ImageSource.FromStream(() => stream);
            }
        }

        return result;
    }
}
