using Pixeler.ExtendedViews;

namespace Pixeler.Views;

public enum Modes
{
	Direct,
	LayeredBigToSmall,
	LayeredSmallToBig
}

public enum LayeredModes
{
	Acryllic,
	BlackBoard,
	Oil
}

public partial class ModeSelectionView : ContentView
{
	private readonly AutoExtendableTypedGrid<Button> _modesGrid;
	private readonly AutoExtendableTypedGrid<Button> _layeredModesGrid;

	public ModeSelectionView()
	{
		InitializeComponent();

        GenerateModes<Modes>(_modesGrid, 0);
		GenerateModes<LayeredModes>(_layeredModesGrid, 1);
    }

	private void GenerateModes<EnumType>(AutoExtendableTypedGrid<Button> grid, int row)
	{
        string[] modes = Enum.GetNames(typeof(EnumType));

		grid = new AutoExtendableTypedGrid<Button>();

		for (int i = 0; i < modes.Length; i++)
			grid.AddToRight(new Button { Text = modes[i] });

        Body.Add(grid.Grid, 0, row);
    }
}