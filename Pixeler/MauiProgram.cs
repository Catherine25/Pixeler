using Pixeler.Configuration;
using Pixeler.Services;
using Pixeler.Views;
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

		return builder.Build();
	}

	private static void RegisterServices(MauiAppBuilder builder)
	{
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddScoped<ISettings, Settings>();
        builder.Services.AddScoped<IAudioService, AudioService>();
        builder.Services.AddScoped<IImageService, ImageService>();
        builder.Services.AddScoped<ILocatorService, LocatorService>();
    }

    private static void RegisterViews(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<DrawAreaView>();
        builder.Services.AddTransient<ImageConfigurationView>();
        builder.Services.AddTransient<LevelSelectionView>();
        builder.Services.AddTransient<ColoringConfigurationSelectionView>();
        builder.Services.AddTransient<PaletteView>();

        builder.Services.AddTransient<MainPage>();
    }
}
