using Pixeler.Source.Configuration;
using Pixeler.Source.Extensions;
using Pixeler.Source.Services.Pixels;
using Pixeler.Source.Views;

namespace Pixeler;

public partial class MainPage : ContentPage
{
	private readonly DrawAreaView _drawAreaView;
	private readonly ImageConfigurationPage _imageConfigurationPage;
    private readonly PaletteView _paletteView;

	private readonly Point _paletteViewLocation = new(0, 1);

    public MainPage(
        DrawAreaView drawAreaView,
        ImageConfigurationPage imageConfigurationPage,
		PaletteView paletteView)
	{
		InitializeComponent();

        _drawAreaView = drawAreaView;
        _imageConfigurationPage = imageConfigurationPage;
        _paletteView = paletteView;

        _drawAreaView.ColorCompleted += _paletteView.CompleteColor;
        _imageConfigurationPage.ColoringConfigurationCompleted += GameConfigurationCompleted;
        _paletteView.OnColorDataChosen += _drawAreaView.SetPixelsToColor;
        _paletteView.AreaColorsDone += AreaColorsDone;

		Body.Add(_paletteView, _paletteViewLocation);

		Navigation.PushAsync(_imageConfigurationPage);
	}

    private void GameConfigurationCompleted(GameConfiguration gameConfiguration)
	{
		Body.Add(_drawAreaView);
        _drawAreaView.SetConfiguration(gameConfiguration);

		var palette = PaletteService.Build(gameConfiguration);
		_paletteView.Colors = palette;
    }

	private void AreaColorsDone()
	{
        DisplayAlert("Congratulations", "You have finished the drawing!", "OK");
	}
}
