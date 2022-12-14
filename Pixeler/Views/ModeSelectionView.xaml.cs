using Pixeler.ExtendedViews;
using Pixeler.Extensions;
using Pixeler.Services;

namespace Pixeler.Views;

public enum Modes
{
	Direct,

    LayeredBigToSmall_Acryllic,
    LayeredBigToSmall_BlackBoard,
    LayeredBigToSmall_Oil
}

public partial class ModeSelectionView : ContentView
{
    public event Action<Modes> SelectedModeChanged;

	private readonly IAudioService _audioService;
	private readonly AutoExtendableTypedGrid<ToggleButton> _modesGrid;

    private Dictionary<Modes, string> _modeNames = new()
    {
        { Modes.Direct, "Direct" },
        { Modes.LayeredBigToSmall_Acryllic, "Layered, Big-To-Small (Acryllic)" },
        { Modes.LayeredBigToSmall_BlackBoard, "Layered, Big-To-Small (BlackBoard)" },
        { Modes.LayeredBigToSmall_Oil, "Layered, Big-To-Small (Oil)" },
    };

    public ModeSelectionView(IAudioService audioService)
	{
		_audioService = audioService;

		InitializeComponent();

        _modesGrid = new AutoExtendableTypedGrid<ToggleButton>();

        CreateButton(Modes.Direct);
        CreateButton(Modes.LayeredBigToSmall_Acryllic);
        CreateButton(Modes.LayeredBigToSmall_BlackBoard);
        CreateButton(Modes.LayeredBigToSmall_Oil);

        Content = _modesGrid.Grid;
    }

	private ToggleButton CreateButton(Modes mode)
	{
        ToggleButton button = new(false) { Text = _modeNames[mode] };
        button.SetClickSound(_audioService);
        button.Clicked += (_,_) => Button_Clicked(button, mode);
        _modesGrid.AddToRight(button);
		return button;
    }

    private void Button_Clicked(ToggleButton button, Modes mode)
    {
        foreach (var item in _modesGrid.Children)
            item.Enabled = true;

        button.Enabled = false;

        SelectedModeChanged(mode);
    }
}