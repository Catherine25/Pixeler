using Pixeler.Models;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class ImageConfigurationView : ContentView
{
	public Action<Bitmap> BitmapSelected;

	private readonly ISettings _settings;

	public ImageConfigurationView(ISettings settings)
	{
		InitializeComponent();

		_settings = settings;

		SelectButton.Clicked += SelectButton_Clicked;
	}

	private async void SelectButton_Clicked(object sender, EventArgs e)
	{
        var bitmap = await ImageService.GetBitmapFromStorage();
        bitmap.Size = _settings.BitmapSize;

        BitmapSelected(bitmap);
    }
}