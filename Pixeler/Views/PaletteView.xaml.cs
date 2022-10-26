using Pixeler.ExtendedViews;
using Pixeler.Models;
using Pixeler.Models.Colors;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class PaletteView : ContentView
{
    public event Action<ColorData> OnColorDataChosen;
    private readonly TypedGrid<PaletteItemView> _gridView;
    private readonly HashSet<ColorData> _colors;
    private readonly IAudioService _audioService;
    private readonly ISettings _settings;

    public PaletteView(ISettings settings, IAudioService audioService, HashSet<ColorData> colors)
    {
        InitializeComponent();

        _colors = colors;
        _audioService = audioService;
        _settings = settings;

        _gridView = new TypedGrid<PaletteItemView>
        {
            Columns = _settings.PaletteSize
        };

        SetColors();

        Content = _gridView.Grid;
    }

    public void SetColors()
    {
        var subset = _colors.Take(Math.Min(_colors.Count, _settings.PaletteSize)).ToList();
        subset.ForEach(x => _colors.Remove(x));

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

        if(_gridView.Count == 0)
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