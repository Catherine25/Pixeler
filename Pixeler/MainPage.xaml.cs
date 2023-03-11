using Pixeler.Source.Configuration;
using Pixeler.Source.Views;

namespace Pixeler;

public partial class MainPage : ContentPage
{
	private readonly DrawingPage _drawingPage;
	private readonly ImageConfigurationPage _imageConfigurationPage;

    public MainPage(DrawingPage drawingPage,
        ImageConfigurationPage imageConfigurationPage)
	{
		InitializeComponent();

        _drawingPage = drawingPage;
        _imageConfigurationPage = imageConfigurationPage;

        _imageConfigurationPage.ConfigurationCompleted += ConfigurationCompleted;

		Navigation.PushAsync(_imageConfigurationPage);
    }

    private void ConfigurationCompleted(GameConfiguration gameConfiguration)
    {
        _drawingPage.SetConfiguration(gameConfiguration);

        Navigation.PushAsync(_drawingPage);
    }
}
