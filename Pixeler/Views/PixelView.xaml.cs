using Pixeler.Models.Colors;

namespace Pixeler.Views;

public partial class PixelView : ContentView
{
    private static readonly ColorData _defaultColor = new(Colors.Transparent.ToHex());
    private static readonly SolidColorBrush _defaultBrush = new(_defaultColor.MColor);
    public Action<PixelView> OnPixelClicked { get; set; }

    public PixelView()
    {
        InitializeComponent();

        Body.Background = _defaultBrush;

        Body.Clicked += Body_Clicked;
    }

    private void Body_Clicked(object sender, EventArgs e)
    {
        if (Active)
        {
            OnPixelClicked(this);
            Active = false;
        }
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
            Body.BorderColor = _active ? Colors.Red : Colors.Transparent;
        }
    }
    private bool _active;
}