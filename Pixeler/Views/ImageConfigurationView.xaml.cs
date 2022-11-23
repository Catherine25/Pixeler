using Pixeler.Extensions;
using Pixeler.Models;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class ImageConfigurationView : ContentView
{
	public Action<ColoringConfiguration> ColoringConfigurationCompleted;

    private readonly IImageService _imageService;
	private readonly LevelSelectionView _levelSelectionView;
	private readonly ModeSelectionView _modeSelectionView;
    private ColoringConfiguration _coloringConfiguration;

    public ImageConfigurationView(IAudioService audioService,
		IImageService imageService,
        LevelSelectionView levelSelectionView,
        ModeSelectionView modeSelectionView)
	{
		InitializeComponent();

        _imageService = imageService;
        _levelSelectionView = levelSelectionView;
        _modeSelectionView = modeSelectionView;
        _coloringConfiguration = new ColoringConfiguration();

        _levelSelectionView.LevelSelected += LevelSelectionView_LevelSelected;
        _modeSelectionView.SelectedModeChanged += _modeSelectionView_SelectedModeChanged;

        SelectButton.SetClickSound(audioService);
        SelectButton.Clicked += SelectButton_Clicked;

        StartButton.SetClickSound(audioService);
        StartButton.Clicked += StartButton_Clicked;
    }

    private void _modeSelectionView_SelectedModeChanged(Modes mode)
    {
        _coloringConfiguration.Mode = mode;
        _coloringConfiguration.ColoringFunc = ColoringFuncService.GetForMode(mode);
        TryEnableStartButton();
    }

    private void LevelSelectionView_LevelSelected(int levelResolution)
	{
        _coloringConfiguration.GridResolution = levelResolution;
        TryEnableStartButton();
    }

    private void TryEnableStartButton()
    {
        if (_coloringConfiguration.Mode == null || _coloringConfiguration.GridResolution == null)
            return;

        StartButton.IsEnabled = true;
    }

    private async void SelectButton_Clicked(object sender, EventArgs e)
	{
        _coloringConfiguration.Bitmap = await _imageService.GetBitmapFromStorage();

		ImageResolutionLabel.IsVisible = true;
        ImageResolutionValueLabel.Text = $"{_coloringConfiguration.Bitmap.Size.Width}x{_coloringConfiguration.Bitmap.Size.Height}, {_coloringConfiguration.Bitmap.Size.Width * _coloringConfiguration.Bitmap.Size.Height} pixels";

		_levelSelectionView.GenerateLevels(_coloringConfiguration.Bitmap.SquaredResolution);
        Body.Add(_levelSelectionView, 0, 4);

        Body.Add(_modeSelectionView, 0, 5);

        StartButton.IsVisible = true;
    }

	private void StartButton_Clicked(object sender, EventArgs e)
	{
		ColoringConfigurationCompleted(_coloringConfiguration);
    }
}