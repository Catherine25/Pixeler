using Pixeler.Source.Configuration;
using Pixeler.Source.Extensions;
using Pixeler.Source.Services.Pixels;

namespace Pixeler.Source.Views;

public partial class DrawingPage : ContentPage
{
    private readonly DrawAreaView _drawAreaView;
    private readonly PaletteView _paletteView;
    private readonly Point _paletteViewLocation = new(0, 1);

    public DrawingPage(DrawAreaView drawAreaView,
		PaletteView paletteView)
	{
		InitializeComponent();

        _drawAreaView = drawAreaView;
        _paletteView = paletteView;

        _drawAreaView.ColorCompleted += _paletteView.CompleteColor;
        _paletteView.OnColorDataChosen += _drawAreaView.SetPixelsToColor;
        _paletteView.AreaColorsDone += AreaColorsDone;

        Body.Add(_paletteView, _paletteViewLocation);
    }

    public void SetGameConfiguration(GameConfiguration gameConfiguration)
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