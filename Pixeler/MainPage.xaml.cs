using Pixeler.Extensions;
using Pixeler.Models;
using Pixeler.Services;
using Pixeler.Views;

namespace Pixeler;

public partial class MainPage : ContentPage
{
	private readonly ImageAreaSelectionView _imageAreaSelectionView;
	private readonly DrawAreaView _drawAreaView;
	private readonly ImageConfigurationView _imageConfigurationView;
    private readonly PaletteView _paletteView;

    public MainPage(
		ImageAreaSelectionView imageAreaSelectionView,
        DrawAreaView drawAreaView,
		ImageConfigurationView imageConfigurationView,
		PaletteView paletteView)
	{
		InitializeComponent();

		_drawAreaView = drawAreaView;
		_imageAreaSelectionView = imageAreaSelectionView;
		_imageConfigurationView = imageConfigurationView;
        _paletteView = paletteView;

        _drawAreaView.ColorCompleted += _paletteView.CompleteColor;
		_imageConfigurationView.BitmapSelected += BitmapSelected;
        _paletteView.OnColorDataChosen += _drawAreaView.SetPixelsToColor;

		Body.Add(_imageConfigurationView, 0, 0);
		Body.Add(_paletteView, 0, 1);
	}

	private void BitmapSelected(Bitmap bitmap)
	{
		Body.ReplaceChild(_imageConfigurationView, _imageAreaSelectionView);

		//Body.ReplaceChild(_imageConfigurationView, _drawAreaView);
		//_drawAreaView.SetBitmap(bitmap);

		//var palette = PaletteService.Build(bitmap);
		//_paletteView.Colors = palette;
    }
}
