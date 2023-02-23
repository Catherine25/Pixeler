using Pixeler.Extensions;
using Pixeler.Models;
using Pixeler.Services;
using Pixeler.Views;

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
		_imageConfigurationView.ColoringConfigurationCompleted += ColoringConfigurationCompleted;
        _paletteView.OnColorDataChosen += _drawAreaView.SetPixelsToColor;
        _paletteView.AreaColorsDone += AreaColorsDone;

		Body.Add(_imageConfigurationView, _imageConfigurationViewLocation);
		Body.Add(_paletteView, _paletteViewLocation);
	}

	private void ColoringConfigurationCompleted(ColoringConfiguration coloringConfiguration)
	{
		Body.ReplaceChild(_imageConfigurationView, _drawAreaView);
        _drawAreaView.SetConfiguration(coloringConfiguration);

		var palette = PaletteService.Build(coloringConfiguration);
		_paletteView.Colors = palette;
    }

	private void AreaColorsDone()
	{
		throw new NotImplementedException();
	}
}
