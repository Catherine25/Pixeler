using Pixeler.ExtendedViews;
using Pixeler.Models;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class ImageConfigurationView : ContentView
{
	public Action<Bitmap> BitmapSelected;

	private readonly IAudioService _audioService;
    private readonly IImageService _imageService;
	private readonly TypedGrid<Button> _grid;
	private Bitmap _bitmap;

    public ImageConfigurationView(IAudioService audioService,
		IImageService imageService)
	{
		InitializeComponent();

		_audioService = audioService;
        _imageService = imageService;
		_grid = new TypedGrid<Button>();

        SelectButton.Clicked += SelectButton_Clicked;
    }

	private async void SelectButton_Clicked(object sender, EventArgs e)
	{
		_audioService.Play();

        _bitmap = await _imageService.GetBitmapFromStorage();

		ImageResolutionLabel.IsVisible = true;
        ImageResolutionValueLabel.Text = $"{_bitmap.Size.Width}x{_bitmap.Size.Height}, {_bitmap.Size.Width * _bitmap.Size.Height} pixels";

		GenerateLevels((int)Math.Min(_bitmap.Size.Width, _bitmap.Size.Height));

		StartButton.IsVisible = true;
    }

	private void GenerateLevels(int bitmapSize)
	{
        int minimumSize = 2;
        int step = 2;
        int currentSize = minimumSize;
		List<Button> availableLevels = new();

		while (currentSize < bitmapSize)
        {
			Button button = new()
			{
				Text = currentSize.ToString(),
			};

			button.Clicked += LevelButton_Clicked;

            availableLevels.Add(button);

			currentSize *= step;
        }

		_grid.Columns = availableLevels.Count;

        for (int i = 0; i < availableLevels.Count; i++)
			_grid.Add(availableLevels[i], new Point(i, 0));

		Body.Add(_grid.Grid, 0, 4);
    }

	private void LevelButton_Clicked(object sender, EventArgs e)
	{
        foreach (var item in _grid.Children)
        	item.IsEnabled = true; // MAUI issue here

        var button = (Button)sender;
		button.IsEnabled = false;

		int size = int.Parse(button.Text);
        _bitmap.Size = new Size(size, size);

		StartButton.IsEnabled = true;
        StartButton.Clicked += StartButton_Clicked;
    }

	private void StartButton_Clicked(object sender, EventArgs e)
	{
		_audioService.Play();
		BitmapSelected(_bitmap);
    }
}