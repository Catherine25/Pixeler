using Pixeler.ExtendedViews;
using Pixeler.Models;
using Pixeler.Models.Colors;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class DrawAreaView : ContentView
{
    public Action ColorCompleted;

    private readonly Bitmap _bitmap;
    private readonly ISettings _settings;
    private Counter _counter;
    private ColorData _pendingColor;
    private readonly TypedGrid<PixelView> _typedGrid;
    private readonly IAudioService _audioService;

    public DrawAreaView() => InitializeComponent();

    public DrawAreaView(ISettings settings, Bitmap bitmap, IAudioService audioService)
    {
        InitializeComponent();

        _settings = settings;
        _bitmap = bitmap;
        _audioService = audioService;

        _typedGrid = new TypedGrid<PixelView>
        {
            Size = bitmap.Size
        };

        for (int x = 0; x < bitmap.Size.Width; x++)
            for (int y = 0; y < bitmap.Size.Height; y++)
            {
                PixelView pixel = new(_settings);
                pixel.OnPixelClicked += OnPixelClicked;
                _typedGrid.Add(pixel, x, y);
            }

        Content = _typedGrid.Grid;
    }

    public void SetPixelsToColor(ColorData color)
    {
        _counter = new Counter();
        _counter.CountedToNull += () => ColorCompleted();

        _pendingColor = color;

        for (int i = 0; i < _typedGrid.Count; i++)
        {
            var pixel = _typedGrid[i];
            var old = pixel.Color;

            if (old == _bitmap.GetPixel(pixel.Location))
                continue;

            if (color == _bitmap.GetPixel(pixel.Location))
            {
                pixel.Active = true;
                _counter.Increase();
            }
            else
                pixel.Active = false;
        }
    }

    private void OnPixelClicked(PixelView pixel)
    {
        _audioService.Play();

        pixel.Color = _pendingColor;

        _counter.Decrease();
    }
}