using Pixeler.Models;
using Pixeler.Services;
using Pixeler.Views;

namespace Pixeler;

public partial class MainPage : ContentPage
{
	private readonly ISettings _settings;
	private readonly IAudioService _audioService;
	private DrawAreaView _drawAreaView;
	private PaletteView _paletteView;

    public MainPage(ISettings settings, IAudioService audioService)
	{
		InitializeComponent();

		_settings = settings;
        _audioService = audioService;

		Loaded += (o, e) => Load();
	}

	private async void Load()
	{
		var bitmap = await ImageService.GetBitmapFromStorage();
		bitmap.Size = _settings.BitmapSize;

		var palette = PaletteService.Build(bitmap);

		PaletteView paletteView = new(_settings, _audioService, palette);
		_paletteView = paletteView;
		Body.Add(paletteView, 0, 1);

        DrawAreaView grid = new(_settings, bitmap, _audioService);
		_drawAreaView = grid;
		_drawAreaView.ColorCompleted += _paletteView.CompleteColor;
		Body.Add(grid, 0, 0);

		paletteView.OnColorDataChosen += _drawAreaView.SetPixelsToColor;
	}
}

