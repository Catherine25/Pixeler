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

        _imageConfigurationPage.ColoringConfigurationCompleted += GameConfigurationCompleted;

		Navigation.PushAsync(_imageConfigurationPage);
	}

    private void GameConfigurationCompleted(GameConfiguration gameConfiguration)
    {
        Navigation.PopAsync();
        _drawingPage.SetGameConfiguration(gameConfiguration);
        Navigation.PushAsync(_drawingPage);
    }
}
