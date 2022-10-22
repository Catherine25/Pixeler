﻿using Pixeler.Models;
using SkiaSharp;

namespace Pixeler.Services;

public static class ImageService
{
    public static async Task<Bitmap> GetBitmapFromStorage()
    {
        var file = await SelectFile();

        using Stream fileStream = await file.OpenReadAsync();

        var bitmap = new Bitmap(SKBitmap.Decode(fileStream));

        return bitmap;
    }

    private static async Task<FileResult> SelectFile()
    {
        try
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
        catch (Exception)
        {
        }

        return null;
    }
}
