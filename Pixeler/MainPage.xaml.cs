using Pixeler.Services;
using Pixeler.Views;
using Plugin.Maui.Audio;

namespace Pixeler;

public partial class MainPage : ContentPage
{
	private DrawAreaView _drawAreaView;
    private PaletteView _paletteView;
	private IAudioManager _audioManager;

	private readonly Size _bitmapSize = new Size(10, 10);
	private const int _paletteSize = 10;
	private const string _soundPath = "demo-2.wav";

    public MainPage(IAudioManager audioManager)
	{
		InitializeComponent();

		_audioManager = audioManager;

        Loaded += (o, e) => Load();
    }

	private async void Load()
	{
		var player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(_soundPath));

        var imageService = new ImageService();
		var bitmap = await imageService.GetBitmapFromStorage();

        bitmap.Size = _bitmapSize;

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

