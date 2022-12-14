using Pixeler.Models;
using Pixeler.Models.Colors;

namespace Pixeler.Views;

public partial class PixelView : ContentView
{
    private static readonly ColorData _defaultColor = new(null);
    private static readonly SolidColorBrush _defaultBrush = new(_defaultColor.MColor);
    private ISettings _settings;
    public Action<PixelView> OnPixelClicked { get; set; }

    public PixelView(ISettings settings)
    {
        InitializeComponent();

        _settings = settings;

        Body.Background = _defaultBrush;
        Body.BorderColor = _defaultColor.MColor;
        Body.Margin = 5;

        Body.Clicked += Body_Clicked;
    }

    private void Body_Clicked(object sender, EventArgs e)
    {
        if (!Active)
            return;

        OnPixelClicked(this);
        Body.CornerRadius = 0;
        Body.Margin = 0;
        Active = false;
    }

    public ColorData Color
    {
        get
        {
            return _color ?? _defaultColor;
        }
        set
        {
            _color = value;
            Body.Background = new SolidColorBrush(_color.MColor);
        }
    }
    private ColorData _color;

    public Point Location => new(Grid.GetColumn(this), Grid.GetRow(this));

    public bool Active
    {
        get => _active;
        set
        {
            _active = value;
            Body.BorderColor = _active ? _settings.AccentColor.MColor : _defaultColor.MColor;
        }
    }
    private bool _active;
}