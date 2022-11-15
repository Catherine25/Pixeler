using Pixeler.Models;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class ImageConfigurationView : ContentView
{
	public Action<Bitmap> BitmapSelected;

	private readonly IAudioService _audioService;
    private readonly IImageService _imageService;
	private readonly LevelSelectionView _levelSelectionView;
	private Bitmap _bitmap;

    public ImageConfigurationView(IAudioService audioService,
		IImageService imageService,
		LevelSelectionView levelSelectionView)
	{
		InitializeComponent();

		_audioService = audioService;
        _imageService = imageService;
        _levelSelectionView = levelSelectionView;

		_levelSelectionView.LevelSelected += LevelSelectionView_LevelSelected;
        SelectButton.Clicked += SelectButton_Clicked;
    }

	private void LevelSelectionView_LevelSelected(int levelResolution)
	{
        _bitmap.Size = new Size(levelResolution);

        StartButton.IsEnabled = true;
        StartButton.Clicked += StartButton_Clicked;
    }

    private async void SelectButton_Clicked(object sender, EventArgs e)
	{
		_audioService.Play();

        _bitmap = await _imageService.GetBitmapFromStorage();

		ImageResolutionLabel.IsVisible = true;
        ImageResolutionValueLabel.Text = $"{_bitmap.Size.Width}x{_bitmap.Size.Height}, {_bitmap.Size.Width * _bitmap.Size.Height} pixels";

		_levelSelectionView.GenerateLevels(_bitmap.SquaredResolution);
        Body.Add(_levelSelectionView, 0, 4);

        StartButton.IsVisible = true;
    }

	private void StartButton_Clicked(object sender, EventArgs e)
	{
		_audioService.Play();
		BitmapSelected(_bitmap);
    }
}