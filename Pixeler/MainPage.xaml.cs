using Pixeler.Models;
using Pixeler.Services;
using Pixeler.Views;

namespace Pixeler;

public partial class MainPage : ContentPage
{
	private readonly DrawAreaView _drawAreaView;
	private readonly PaletteView _paletteView;
	private readonly ISettings _settings;

    public MainPage(
        DrawAreaView drawAreaView,
		PaletteView paletteView,
		ISettings settings)
	{
		InitializeComponent();

        _drawAreaView = drawAreaView;
		_settings = settings;
		_paletteView = paletteView;

        _drawAreaView.ColorCompleted += _paletteView.CompleteColor;
        _paletteView.OnColorDataChosen += _drawAreaView.SetPixelsToColor;

		Body.Add(_drawAreaView, 0, 0);
		Body.Add(_paletteView, 0, 1);

        Loaded += (o, e) => Load();
	}

	private async void Load()
	{
		var bitmap = await ImageService.GetBitmapFromStorage();
		bitmap.Size = _settings.BitmapSize;
		_drawAreaView.SetBitmap(bitmap);

		var palette = PaletteService.Build(bitmap);
		_paletteView.Colors = palette;
	}
}
