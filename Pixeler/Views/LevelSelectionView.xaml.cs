using Pixeler.ExtendedViews;

namespace Pixeler.Views;

public partial class LevelSelectionView : ContentView
{
	public event Action<int> LevelSelected;

	private readonly TypedGrid<Button> _grid;

	public LevelSelectionView()
	{
		InitializeComponent();

		_grid = new TypedGrid<Button>();
	}

	public void GenerateLevels(int squaredResolution)
	{
        int minimumSize = 2;
        int step = 2;
        int currentSize = minimumSize;
		List<Button> availableLevels = new();

		while (currentSize < squaredResolution)
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

		Content = _grid.Grid;
    }

	private void LevelButton_Clicked(object sender, EventArgs e)
	{
        foreach (var item in _grid.Children)
        	item.IsEnabled = true; // MAUI issue here

        var button = (Button)sender;
		button.IsEnabled = false;

        int size = int.Parse(button.Text);

		LevelSelected(size);
    }
}