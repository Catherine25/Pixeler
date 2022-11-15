﻿using Pixeler.Extensions;
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
		_imageConfigurationView.BitmapSelected += BitmapSelected;
        _paletteView.OnColorDataChosen += _drawAreaView.SetPixelsToColor;

		Body.Add(_imageConfigurationView, _imageConfigurationViewLocation);
		Body.Add(_paletteView, _paletteViewLocation);
	}

	private void BitmapSelected(Bitmap bitmap)
	{
		Body.ReplaceChild(_imageConfigurationView, _drawAreaView);
        _drawAreaView.SetBitmap(bitmap);

		var palette = PaletteService.Build(bitmap);
		_paletteView.Colors = palette;
    }
}
