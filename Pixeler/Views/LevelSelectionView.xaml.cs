using Pixeler.ExtendedViews;

namespace Pixeler.Views;

public partial class LevelSelectionView : ContentView
{
	public event Action<int> LevelSelected;

	private readonly TypedGrid<ToggleButton> _grid;

	public LevelSelectionView()
	{
		InitializeComponent();

		_grid = new TypedGrid<ToggleButton>();
	}

	public void GenerateLevels(int squaredResolution)
	{
        int minimumSize = 2;
        int step = 2;
        int currentSize = minimumSize;
		List<ToggleButton> availableLevels = new();

		while (currentSize < squaredResolution)
        {
            ToggleButton button = new()
            {
                Text = currentSize.ToString()
            };

            button.Clicked += (_,_) => LevelButton_Clicked(button);

            availableLevels.Add(button);

			currentSize *= step;
        }

		_grid.Columns = availableLevels.Count;

        for (int i = 0; i < availableLevels.Count; i++)
			_grid.Add(availableLevels[i], new Point(i, 0));

		Content = _grid.Grid;
    }

	private void LevelButton_Clicked(ToggleButton button)
	{
		foreach (var item in _grid.Children)
			item.Enabled = true;

		button.Enabled = false;

		int size = int.Parse(button.Text);

		LevelSelected(size);
	}
}