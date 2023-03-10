using Pixeler.Source.Colors;
using Pixeler.Source.Configuration;
using Pixeler.Source.ExtendedViews;
using Pixeler.Source.Services;

namespace Pixeler.Source.Views;

public partial class DrawAreaView : ContentView
{
    public Action ColorCompleted;

    private GameConfiguration _coloringConfiguration;
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

    public void SetConfiguration(GameConfiguration coloringConfiguration)
    {
        _coloringConfiguration = coloringConfiguration;

        int resolution = coloringConfiguration.GridResolution;
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

        // todo foreach
        for (int i = 0; i < _typedGrid.Count; i++)
        {
            var existingPixelView = _typedGrid[i];

            // skip if the pixel coloring is done
            if(existingPixelView.ColoringState == ColoringStates.Done)
                continue;

            var existingColor = existingPixelView.Color;
            var originalColor = _coloringConfiguration.GetPixel(existingPixelView.Location);

            // calculate color
            var newColor = _coloringConfiguration.CalculateColor(existingColor, _pendingColor, originalColor);

            // if the color was not calculated - skip
            // it means that existing and selected colors will not result the needed color
            if (newColor == null)
                existingPixelView.ColoringState = ColoringStates.Passive;
            else
            {
                // mark as 'finishing' if new color is the same as original
                existingPixelView.ColoringState = newColor.IsFinal
                    ? existingPixelView.ColoringState = ColoringStates.Finising
                    : existingPixelView.ColoringState = ColoringStates.Waiting;
                _counter.Increase();
            }
        }

        // if the cell was colored by mixing other lighter colors - skip it
        if (_counter.Value == 0)
            ColorCompleted();
    }

    private void OnPixelClicked(PixelView pixel)
    {
        _audioService.Play();

        pixel.Color = _pendingColor;

        _counter.Decrease();
    }
}