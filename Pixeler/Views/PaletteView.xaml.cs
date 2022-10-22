using Pixeler.ExtendedViews;
using Pixeler.Models.Colors;
using Plugin.Maui.Audio;

namespace Pixeler.Views;

public partial class PaletteView : ContentView
{
    public event Action<ColorData> OnColorDataChosen;
    private TypedGrid<PaletteItemView> _gridView;
    private HashSet<ColorData> _colors;
    private int _count;
    private IAudioPlayer _player;

    public PaletteView(IAudioPlayer player, HashSet<ColorData> colors, int count)
    {
        InitializeComponent();

        _colors = colors;
        _count = count;
        _player = player;

        _gridView = new TypedGrid<PaletteItemView>
        {
            Columns = count
        };

        SetColors();

        Content = _gridView.Grid;
    }

    public void SetColors()
	{
        var subset = _colors.Take(Math.Min(_colors.Count, _count)).ToList();

        for(int i = 0; i < subset.Count; i++)
        {
            ColorData item = subset[i];

            var rect = new PaletteItemView
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
    }

    private void Rect_Clicked(PaletteItemView paletteItem)
    {
        _player.Play();

        DeselectPreviouslySelected();

        var paletteItemViews = _gridView.Children;
        var paletteItemView = paletteItemViews.Single(x => x.Color == paletteItem.Color);
        paletteItemView.Selected = true;

        OnColorDataChosen(paletteItem.Color);
    }

    private void DeselectPreviouslySelected()
    {
        PaletteItemView selected = _gridView.Children.SingleOrDefault(x => x.Selected);

        if (selected != null)
            selected.Selected = false;
    }
}