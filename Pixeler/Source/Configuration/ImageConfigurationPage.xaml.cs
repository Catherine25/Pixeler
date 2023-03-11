using Pixeler.Source.Configuration;
using Pixeler.Source.Configuration.Coloring;
using Pixeler.Source.Configuration.Images;
using Pixeler.Source.Extensions;
using Pixeler.Source.Services;

namespace Pixeler.Source.Views;

public partial class ImageConfigurationPage : ContentPage
{
    public Action<GameConfiguration> ConfigurationCompleted;

    private readonly ILoaderService _imageService;
    private readonly ILocatorService _locatorService;
    private readonly LevelSelectionView _levelSelectionView;
    private readonly ColoringConfigurationSelectionView _coloringConfigurationSelectionView;

    private Bitmap _bitmap;
    private int _levelResolution;
    private ColoringConfiguration _coloringConfiguration;

    private readonly Point _levelSelectionViewLocation = new(0, 4);
    private readonly Point _modeSelectionViewLocation = new(0, 5);

    public ImageConfigurationPage(IAudioService audioService,
        ILoaderService imageService,
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
        _coloringConfiguration = coloringConfiguration;

        TryEnableStartButton();
    }

    private void LevelSelectionView_LevelSelected(int levelResolution)
    {
        _levelResolution = levelResolution;

        TryEnableStartButton();
    }

    private void TryEnableStartButton()
    {
        if (_levelResolution == 0)
            return;

        StartButton.IsEnabled = true;
    }

    private async void SelectButton_Clicked(object sender, EventArgs e)
    {
        _bitmap = await _imageService.LoadBitmapFromStorage();
        int width = (int)_bitmap.Size.Width;
        int height = (int)_bitmap.Size.Height;

        ImageResolutionLabel.IsVisible = true;
        ImageResolutionValueLabel.Text = $"{width}x{height}, {width * height} pixels";

        _levelSelectionView.GenerateLevelButtons(_bitmap.SquaredResolution);
        Body.Add(_levelSelectionView, _levelSelectionViewLocation);
        Body.Add(_coloringConfigurationSelectionView, _modeSelectionViewLocation);

        StartButton.IsVisible = true;
    }

    private void StartButton_Clicked(object sender, EventArgs e)
    {
        BitmapConfiguration bitmapConfiguration = new(_bitmap, _locatorService)
        {
            GridResolution = _levelResolution
        };

        GameConfiguration gameConfiguration = new(bitmapConfiguration, _coloringConfiguration);

        ConfigurationCompleted(gameConfiguration);
    }
}