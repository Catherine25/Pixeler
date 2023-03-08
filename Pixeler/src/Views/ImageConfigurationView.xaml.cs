using Pixeler.Configuration;
using Pixeler.Configuration.Coloring;
using Pixeler.Extensions;
using Pixeler.Services;
using Pixeler.Services.Pixels;

namespace Pixeler.Views;

public partial class ImageConfigurationView : ContentView
{
	public Action<GameConfiguration> ColoringConfigurationCompleted;

    private readonly IImageService _imageService;
    private readonly ILocatorService _locatorService;
	private readonly LevelSelectionView _levelSelectionView;
	private readonly ColoringConfigurationSelectionView _coloringConfigurationSelectionView;
    private GameConfiguration _gameConfiguration;
    private readonly Point _levelSelectionViewLocation = new(0, 4);
    private readonly Point _modeSelectionViewLocation = new(0, 5);

    public ImageConfigurationView(IAudioService audioService,
		IImageService imageService,
        ILocatorService locatorService,
        LevelSelectionView levelSelectionView,
        ColoringConfigurationSelectionView coloringConfigurationSelectionView)
	{
		InitializeComponent();

        _imageService = imageService;
        _locatorService = locatorService;
        _levelSelectionView = levelSelectionView;
        _coloringConfigurationSelectionView = coloringConfigurationSelectionView;

        _levelSelectionView.LevelSelected += LevelSelectionView_LevelSelected;
        _coloringConfigurationSelectionView.SelectedColoringConfigurationChanged += ColoringConfigurationSelectionView_SelectedColoringConfigurationChanged;

        SelectButton.SetClickSound(audioService);
        SelectButton.Clicked += SelectButton_Clicked;

        StartButton.SetClickSound(audioService);
        StartButton.Clicked += StartButton_Clicked;
    }

    private void ColoringConfigurationSelectionView_SelectedColoringConfigurationChanged(ColoringConfiguration coloringConfiguration)
    {
        _gameConfiguration.ColoringConfiguration = coloringConfiguration;
        _gameConfiguration.CalculateColor = new ColoringFuncService().GetForMode(coloringConfiguration);
        TryEnableStartButton();
    }

    private void LevelSelectionView_LevelSelected(int levelResolution)
	{
        _gameConfiguration.GridResolution = levelResolution;

        TryEnableStartButton();
    }

    private void TryEnableStartButton()
    {
        if (_gameConfiguration.GridResolution == 0)
            return;

        StartButton.IsEnabled = true;
    }

    private async void SelectButton_Clicked(object sender, EventArgs e)
	{
        var bitmap = await _imageService.GetBitmapFromStorage();

        _gameConfiguration = new GameConfiguration(bitmap, _locatorService);

        ImageResolutionLabel.IsVisible = true;
        var size = _gameConfiguration.Size;
        ImageResolutionValueLabel.Text = $"{size.Width}x{size.Height}, {size.Width * size.Height} pixels";

		_levelSelectionView.GenerateLevelButtons(_gameConfiguration.SquaredResolution);
        Body.Add(_levelSelectionView, _levelSelectionViewLocation);
        Body.Add(_coloringConfigurationSelectionView, _modeSelectionViewLocation);

        StartButton.IsVisible = true;
    }

	private void StartButton_Clicked(object sender, EventArgs e)
	{
		ColoringConfigurationCompleted(_gameConfiguration);
    }
}