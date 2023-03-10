using Pixeler.Source.Configuration;
using Pixeler.Source.Extensions;
using Pixeler.Source.Services.Pixels;
using Pixeler.Source.Views;

namespace Pixeler;

public partial class MainPage : ContentPage
{
	private readonly DrawAreaView _drawAreaView;
	private readonly ImageConfigurationView _imageConfigurationView;
    private readonly PaletteView _paletteView;

	private readonly Point _imageConfigurationViewLocation = new(0, 0);
	private readonly Point _paletteViewLocation = new(0, 1);

    public MainPage(
        DrawAreaView drawAreaView,
		ImageConfigurationView imageConfigurationView,
		PaletteView paletteView)
	{
		InitializeComponent();

        _drawAreaView = drawAreaView;
		_imageConfigurationView = imageConfigurationView;
        _paletteView = paletteView;

        _drawAreaView.ColorCompleted += _paletteView.CompleteColor;
		_imageConfigurationView.ColoringConfigurationCompleted += GameConfigurationCompleted;
        _paletteView.OnColorDataChosen += _drawAreaView.SetPixelsToColor;
        _paletteView.AreaColorsDone += AreaColorsDone;

		Body.Add(_imageConfigurationView, _imageConfigurationViewLocation);
		Body.Add(_paletteView, _paletteViewLocation);
	}

	private void GameConfigurationCompleted(GameConfiguration gameConfiguration)
	{
		Body.ReplaceChild(_imageConfigurationView, _drawAreaView);
        _drawAreaView.SetConfiguration(gameConfiguration);

		var palette = PaletteService.Build(gameConfiguration);
		_paletteView.Colors = palette;
    }

	private void AreaColorsDone()
	{
        DisplayAlert("Congratulations", "You have finished the drawing!", "OK");
	}
}
