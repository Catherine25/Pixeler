using Pixeler.ExtendedViews;
using Pixeler.Models;
using Pixeler.Models.Colors;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class PaletteView : ContentView
{
    public event Action<ColorData> OnColorDataChosen;

    private readonly TypedGrid<PaletteItemView> _gridView;
    private readonly IAudioService _audioService;
    private readonly ISettings _settings;

    public IEnumerable<ColorData> Colors
    {
        set
        {
            _colors = value;
            SetColors();
        }
    }
    private IEnumerable<ColorData> _colors;

    public PaletteView(
        ISettings settings,
        IAudioService audioService)
    {
        InitializeComponent();

        _audioService = audioService;
        _settings = settings;

        _gridView = new TypedGrid<PaletteItemView>
        {
            Columns = _settings.PaletteSize
        };

        Content = _gridView.Grid;
    }

    public void SetColors()
    {
        int paletteCount = Math.Min(_colors.Count(), _settings.PaletteSize);
        var subset = _colors.Take(paletteCount).ToList();
        _colors = _colors.Except(subset);

        for (int i = 0; i < subset.Count; i++)
        {
            ColorData item = subset[i];

            var rect = new PaletteItemView(_settings)
            {
                Color = item
            };

            rect.Clicked += Rect_Clicked;

            _gridView.Add(rect, i, 0);
        }
    }

    public void CompleteColor()
    {
        var item = _gridView.Children.Single(x => x.Selected);
        _gridView.Remove(item);

        if (_gridView.Count == 0)
            SetColors();
    }

    private void Rect_Clicked(PaletteItemView paletteItem)
    {
        _audioService.Play();

        DeselectPreviouslySelected();
        SelectPaletteItem(paletteItem);

        OnColorDataChosen(paletteItem.Color);
    }

    private void DeselectPreviouslySelected()
    {
        PaletteItemView selected = _gridView.Children.SingleOrDefault(x => x.Selected);

        if (selected != null)
            selected.Selected = false;
    }

    private void SelectPaletteItem(PaletteItemView paletteItem)
    {
        var paletteItemViews = _gridView.Children;
        var paletteItemView = paletteItemViews.Single(x => x.Color == paletteItem.Color);
        paletteItemView.Selected = true;
    }
}