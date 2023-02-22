namespace Pixeler.ExtendedViews;

public class ToggleButton : Button
{
    private static readonly Color _enabledColor = Color.FromArgb("#512BD4");
    private static readonly Color _disabledColor = Color.FromArgb("#FFFFFF");

    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled == value)
                return;

            _enabled = value;

            TextColor = _enabled ? _disabledColor : _enabledColor;
            BackgroundColor = _enabled ? _enabledColor : _disabledColor;
        }
    }
    private bool _enabled;

    public bool ToggleOnClick;

    public ToggleButton(bool toggleOnClick = true)
    {
        Enabled = true;
        ToggleOnClick = toggleOnClick;
        Clicked += ToggleButton_Clicked;
    }

    private void ToggleButton_Clicked(object sender, EventArgs e)
    {
        if(ToggleOnClick)
            Enabled = !Enabled;
    }
}