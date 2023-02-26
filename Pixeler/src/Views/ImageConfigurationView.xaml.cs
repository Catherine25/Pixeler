using Pixeler.Extensions;
using Pixeler.Models;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class ImageConfigurationView : ContentView
{
	public Action<ColoringConfiguration> ColoringConfigurationCompleted;

    private readonly IImageService _imageService;
    private readonly ILocatorService _locatorService;
	private readonly LevelSelectionView _levelSelectionView;
	private readonly ModeSelectionView _modeSelectionView;
    private ColoringConfiguration _coloringConfiguration;
    private readonly Point _levelSelectionViewLocation = new(0, 4);
    private readonly Point _modeSelectionViewLocation = new(0, 5);

    public ImageConfigurationView(IAudioService audioService,
		IImageService imageService,
        ILocatorService locatorService,
        LevelSelectionView levelSelectionView,
        ModeSelectionView modeSelectionView)
	{
		InitializeComponent();

        _imageService = imageService;
        _locatorService = locatorService;
        _levelSelectionView = levelSelectionView;
        _modeSelectionView = modeSelectionView;

        _levelSelectionView.LevelSelected += LevelSelectionView_LevelSelected;
        _modeSelectionView.SelectedModeChanged += ModeSelectionView_SelectedModeChanged;

        SelectButton.SetClickSound(audioService);
        SelectButton.Clicked += SelectButton_Clicked;

        StartButton.SetClickSound(audioService);
        StartButton.Clicked += StartButton_Clicked;
    }

    private void ModeSelectionView_SelectedModeChanged(Modes mode)
    {
        _coloringConfiguration.Mode = mode;
        _coloringConfiguration.CalculateColor = ColoringFuncService.GetForMode(mode);
        TryEnableStartButton();
    }

    private void LevelSelectionView_LevelSelected(int levelResolution)
	{
        _coloringConfiguration.GridResolution = levelResolution;

        TryEnableStartButton();
    }

    private void TryEnableStartButton()
    {
        if (_coloringConfiguration.Mode == null || _coloringConfiguration.GridResolution == 0)
            return;

        StartButton.IsEnabled = true;
    }

    private async void SelectButton_Clicked(object sender, EventArgs e)
	{
        var bitmap = await _imageService.GetBitmapFromStorage();

        _coloringConfiguration = new ColoringConfiguration(bitmap, _locatorService);

        ImageResolutionLabel.IsVisible = true;
        var size = _coloringConfiguration.Size;
        ImageResolutionValueLabel.Text = $"{size.Width}x{size.Height}, {size.Width * size.Height} pixels";

		_levelSelectionView.GenerateLevelButtons(_coloringConfiguration.SquaredResolution);
        Body.Add(_levelSelectionView, _levelSelectionViewLocation);
        Body.Add(_modeSelectionView, _modeSelectionViewLocation);

        StartButton.IsVisible = true;
    }

	private void StartButton_Clicked(object sender, EventArgs e)
	{
		ColoringConfigurationCompleted(_coloringConfiguration);
    }
}