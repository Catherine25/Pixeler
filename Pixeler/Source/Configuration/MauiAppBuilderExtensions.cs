using Pixeler.Source;
using Pixeler.Source.Configuration.Images;
using Pixeler.Source.Drawing.Pixels;
using Pixeler.Source.Services;
using Pixeler.Source.Views;
using Plugin.Maui.Audio;

namespace Pixeler.Source.Configuration;

public static class MauiAppBuilderExtensions
{
    public static void RegisterServices(this MauiAppBuilder builder)
	{
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddSingleton<ISettings, Settings>();
        builder.Services.AddSingleton<IAudioService, AudioService>();
        builder.Services.AddSingleton<ILoaderService, BitmapLoadingService>();
        builder.Services.AddSingleton<ILocatorService, LocatorService>();
        builder.Services.AddSingleton<IColoringFuncService, ColoringFuncService>();
    }

    public static void RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<DrawAreaView>();
        builder.Services.AddSingleton<LevelSelectionView>();
        builder.Services.AddSingleton<ColoringConfigurationSelectionView>();
        builder.Services.AddSingleton<PaletteView>();
    }

    public static void RegisterPages(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<DrawingPage>();
        builder.Services.AddSingleton<ImageConfigurationPage>();
        builder.Services.AddSingleton<MainPage>();
    }
}