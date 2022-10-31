using Pixeler.Models;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class ImageConfigurationView : ContentView
{
	public Action<Bitmap> BitmapSelected;

    private readonly IImageService _imageService;
	private readonly ISettings _settings;

    public ImageConfigurationView(ISettings settings,
		IImageService imageService)
	{
		InitializeComponent();

		_settings = settings;
        _imageService = imageService;

		SelectButton.Clicked += SelectButton_Clicked;
	}

	private async void SelectButton_Clicked(object sender, EventArgs e)
	{
        var bitmap = await _imageService.GetBitmapFromStorage();

		// todo set it in other view
        bitmap.Size = _settings.BitmapSize;

        BitmapSelected(bitmap);
    }
}