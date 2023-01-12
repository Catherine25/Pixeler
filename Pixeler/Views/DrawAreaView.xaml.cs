using Pixeler.ExtendedViews;
using Pixeler.Models;
using Pixeler.Models.Colors;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class DrawAreaView : ContentView
{
    public Action ColorCompleted;

    private ColoringConfiguration _coloringConfiguration;
    private readonly ISettings _settings;
    private Counter _counter;
    private ColorData _pendingColor;
    private readonly TypedGrid<PixelView> _typedGrid;
    private readonly IAudioService _audioService;

    public DrawAreaView(
        ISettings settings,
        IAudioService audioService)
    {
        InitializeComponent();

        _settings = settings;
        _audioService = audioService;

        _typedGrid = new TypedGrid<PixelView>();

        Content = _typedGrid.Grid;
    }

    public void SetConfiguration(ColoringConfiguration coloringConfiguration)
    {
        _coloringConfiguration = coloringConfiguration;

        int resolution = coloringConfiguration.GridResolution.Value;
        _typedGrid.Size = new Size(resolution);

        for (int x = 0; x < resolution; x++)
            for (int y = 0; y < resolution; y++)
            {
                PixelView pixel = new(_settings);
                pixel.OnPixelClicked += OnPixelClicked;
                _typedGrid.Add(pixel, x, y);
            }
    }

    public void SetPixelsToColor(ColorData color)
    {
        _counter = new Counter();
        _counter.CountedToNull += () => ColorCompleted();

        _pendingColor = color;

        for (int i = 0; i < _typedGrid.Count; i++)
        {
            var existingPixelView = _typedGrid[i];
            var existingColor = existingPixelView.Color;
            var originalColor = _coloringConfiguration.GetPixel(existingPixelView.Location);

            if (existingColor == originalColor)
                continue;

            if (_pendingColor == originalColor)
            {
                existingPixelView.Active = true;
                _counter.Increase();
            }
            else
                existingPixelView.Active = false;
        }
    }

    private void OnPixelClicked(PixelView pixel)
    {
        _audioService.Play();

        pixel.Color = _pendingColor;

        _counter.Decrease();
    }
}