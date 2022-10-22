using Pixeler.Models;
using Pixeler.Services;
using Pixeler.Views;
using Plugin.Maui.Audio;

namespace Pixeler;

public partial class MainPage : ContentPage
{
	private readonly ISettings _settings;
	private readonly IAudioManager _audioManager;
	private DrawAreaView _drawAreaView;
    private PaletteView _paletteView;

	private const int _paletteSize = 10;

    public MainPage(IAudioManager audioManager,
		ISettings settings)
	{
		InitializeComponent();

		_settings = settings;
		_audioManager = audioManager;

        Loaded += (o, e) => Load();
    }

	private async void Load()
	{
		var player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(_settings.SoundPath));

        var imageService = new ImageService();
		var bitmap = await imageService.GetBitmapFromStorage();

        bitmap.Size = _settings.BitmapSize;

        var paletteService = new PaletteService();
		var palette = paletteService.Build(bitmap);

		var paletteView = new PaletteView(player, palette, _paletteSize);
		_paletteView = paletteView;
		Body.Add(paletteView, 0, 1);

		var grid = new DrawAreaView(bitmap, player);
		_drawAreaView = grid;
        _drawAreaView.ColorCompleted += _paletteView.CompleteColor;
        Body.Add(grid, 0, 0);

		paletteView.OnColorDataChosen += _drawAreaView.SetPixelsToColor;
    }
}

