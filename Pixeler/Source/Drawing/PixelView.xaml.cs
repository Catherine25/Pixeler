using Pixeler.Source.Colors;

namespace Pixeler.Source.Views;

public enum ColoringStates
{
    Passive,
    Waiting,
    Finising,
    Done,
}

public partial class PixelView : ContentView
{
    private static readonly ColorData _defaultColor = new(null);
    private static readonly SolidColorBrush _defaultBrush = new(_defaultColor.MColor);

    private readonly ISettings _settings;
    public Action<PixelView> OnPixelClicked { get; set; }
    public bool ColoringDone = false;

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

    public ColoringStates ColoringState
    {
        get => _coloringState;
        set
        {
            _coloringState = value;

            Body.BorderColor = IsWaitingForInteraction ? _settings.AccentColor.MColor : _defaultColor.MColor;
            Body.CornerRadius = IsWaitingForInteraction ? 5 : 0;
        }
    }
    private ColoringStates _coloringState;

    public bool IsWaitingForInteraction => ColoringState == ColoringStates.Waiting || ColoringState == ColoringStates.Finising;

    public PixelView(ISettings settings)
    {
        InitializeComponent();

        _settings = settings;

        Body.Background = _defaultBrush;
        Body.BorderColor = _defaultColor.MColor;
        Body.BorderWidth = _settings.PixelHighlightThickness;
        Body.Margin = _settings.PixelMargin;

        // old way of handling
        // Body.Clicked += (_, _) => Interacted();
        GraphicsView.StartHoverInteraction += (_, _) => Interacted();
    }

    private void Interacted()
    {
        if (!IsWaitingForInteraction)
            return;

        OnPixelClicked(this);
        Body.CornerRadius = 0;
        Body.Margin = 0;

        ColoringState = ColoringState == ColoringStates.Finising
            ? ColoringState = ColoringStates.Done
            : ColoringState = ColoringStates.Passive;
    }
}