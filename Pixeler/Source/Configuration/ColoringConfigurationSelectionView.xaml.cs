using Pixeler.Source.Configuration.Coloring;
using Pixeler.Source.Drawing.Pixels;
using Pixeler.Source.ExtendedViews;
using Pixeler.Source.Extensions;
using Pixeler.Source.Services;

namespace Pixeler.Source.Views;

public partial class ColoringConfigurationSelectionView : ContentView
{
    public event Action<ColoringConfiguration> SelectedColoringConfigurationChanged;

	private readonly IAudioService _audioService;
	private readonly AutoExtendableTypedGrid<ToggleButton> _modesGrid;

    public ColoringConfigurationSelectionView(IAudioService audioService)
	{
		_audioService = audioService;

		InitializeComponent();

        _modesGrid = new AutoExtendableTypedGrid<ToggleButton>();

        CreateButton(new(PixelGrouping.None, Layoring.Oil));
        CreateButton(new(PixelGrouping.None, Layoring.Acryllic));

        Content = _modesGrid.Grid;
    }

	private ToggleButton CreateButton(ColoringConfiguration coloringConfiguration)
	{
        ToggleButton button = new(false) { Text = $"{coloringConfiguration.Layoring}, {coloringConfiguration.PixelGrouping}" };
        button.SetClickSound(_audioService);
        button.Clicked += (_,_) => Button_Clicked(button, coloringConfiguration);
        _modesGrid.AddToRight(button);
		return button;
    }

    private void Button_Clicked(ToggleButton button, ColoringConfiguration configuration)
    {
        foreach (var item in _modesGrid.Children)
            item.Enabled = true;

        button.Enabled = false;

        // todo refactor
        configuration.CalculateColor = new ColoringFuncService().GetForMode(configuration);

        SelectedColoringConfigurationChanged(configuration);
    }
}