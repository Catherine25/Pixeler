using Pixeler.Source.Configuration;
using Pixeler.Source.Configuration.Images;
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
        builder.Services.AddScoped<ISettings, Settings>();
        builder.Services.AddScoped<IAudioService, AudioService>();
        builder.Services.AddScoped<ILoaderService, BitmapLoadingService>();
        builder.Services.AddScoped<ILocatorService, LocatorService>();
    }

    private static void RegisterViews(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<DrawAreaView>();
        builder.Services.AddTransient<LevelSelectionView>();
        builder.Services.AddTransient<ColoringConfigurationSelectionView>();
        builder.Services.AddTransient<PaletteView>();
    }

    private static void RegisterPages(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<DrawingPage>();
        builder.Services.AddTransient<ImageConfigurationPage>();
        builder.Services.AddTransient<MainPage>();
    }
}
