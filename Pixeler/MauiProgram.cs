using Pixeler.Source;
using Pixeler.Source.Configuration.Images;
using Pixeler.Source.Drawing.Pixels;
using Pixeler.Source.Services;
using Pixeler.Source.Views;
using Plugin.Maui.Audio;

namespace Pixeler;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		RegisterServices(builder);
        RegisterViews(builder);
        RegisterPages(builder);

        return builder.Build();
	}

	private static void RegisterServices(MauiAppBuilder builder)
	{
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddSingleton<ISettings, Settings>();
        builder.Services.AddSingleton<IAudioService, AudioService>();
        builder.Services.AddSingleton<ILoaderService, BitmapLoadingService>();
        builder.Services.AddSingleton<ILocatorService, LocatorService>();
        builder.Services.AddSingleton<IColoringFuncService, ColoringFuncService>();
    }

    private static void RegisterViews(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<DrawAreaView>();
        builder.Services.AddSingleton<LevelSelectionView>();
        builder.Services.AddSingleton<ColoringConfigurationSelectionView>();
        builder.Services.AddSingleton<PaletteView>();
    }

    private static void RegisterPages(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<DrawingPage>();
        builder.Services.AddSingleton<ImageConfigurationPage>();
        builder.Services.AddSingleton<MainPage>();
    }
}
