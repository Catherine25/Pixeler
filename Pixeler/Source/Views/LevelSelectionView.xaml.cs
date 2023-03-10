using Pixeler.Source.Configuration;
using Pixeler.Source.ExtendedViews;
using Pixeler.Source.Extensions;
using Pixeler.Source.Services;

namespace Pixeler.Source.Views;

public partial class LevelSelectionView : ContentView
{
	public event Action<int> LevelSelected;

    private readonly TypedGrid<ToggleButton> _grid;
    private readonly IAudioService _audioService;
    private readonly ISettings _settings;

    public LevelSelectionView(IAudioService audioService, ISettings settings)
	{
		InitializeComponent();

		_grid = new TypedGrid<ToggleButton>();
        _audioService = audioService;
        _settings = settings;
    }

	public void GenerateLevelButtons(int squaredResolution)
	{
		int maximumLevelSize = Math.Min(squaredResolution, _settings.LevelSizeRange.Max);
        int step = 2;
        int currentSize = _settings.LevelSizeRange.Min;
		List<ToggleButton> availableLevels = new();

		while (currentSize <= maximumLevelSize)
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