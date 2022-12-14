using Pixeler.ExtendedViews;
using Pixeler.Extensions;
using Pixeler.Services;

namespace Pixeler.Views;

public partial class LevelSelectionView : ContentView
{
	public event Action<int> LevelSelected;

	private readonly TypedGrid<ToggleButton> _grid;
    private readonly IAudioService _audioService;

    public LevelSelectionView(IAudioService audioService)
	{
		InitializeComponent();

		_grid = new TypedGrid<ToggleButton>();
        _audioService = audioService;
    }

	public void GenerateLevelButtons(int squaredResolution)
	{
        int minimumSize = 2;
        int step = 2;
        int currentSize = minimumSize;
		List<ToggleButton> availableLevels = new();

		while (currentSize < squaredResolution)
        {
            ToggleButton button = new(false)
            {
                Text = currentSize.ToString()
            };

			button.SetClickSound(_audioService);
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