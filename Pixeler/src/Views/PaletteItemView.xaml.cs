using Pixeler.Models.Colors;
using Pixeler.Configuration;

namespace Pixeler.Views;

public partial class PaletteItemView : ContentView
{
    public event Action<PaletteItemView> Clicked;
    private ISettings _settings;

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
            Body.BorderColor = _selected ? _settings.AccentColor.MColor : Colors.Transparent;
        }
    }
    private bool _selected;

    public PaletteItemView(ISettings settings)
    {
        InitializeComponent();

        _settings = settings;

        Body.BorderColor = Colors.Transparent;
        Body.Clicked += (o, e) => Clicked(this);
    }
}