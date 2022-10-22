using Pixeler.Models.Colors;

namespace Pixeler.Views;

public partial class PaletteItemView : ContentView
{
    public event Action<PaletteItemView> Clicked;

    public ColorData Color
    {
        get => _color;
        set
        {
            _color = value;
            Body.Background = new SolidColorBrush(_color.MColor);
        }
    }
    private ColorData _color;

    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            Body.BorderColor = _selected ? Colors.Red : Colors.Transparent;
        }
    }
    private bool _selected;

    public PaletteItemView()
    {
        InitializeComponent();

        Body.Clicked += (o, e) => Clicked(this);
    }
}