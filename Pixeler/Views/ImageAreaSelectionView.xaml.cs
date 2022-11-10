using Pixeler.ExtendedViews;
using Pixeler.Models;

namespace Pixeler.Views;

public partial class ImageAreaSelectionView : ContentView
{
	private TypedGrid<Button> _grid;
	private Bitmap _bitmap;

    public ImageAreaSelectionView()
	{
		InitializeComponent();
	}

	//public void UpdateGrid(Bitmap bitmap, )
	//{
	//	_bitmap = bitmap;
	//}
}