namespace Pixeler.Extensions;

public static class GridExtensions
{
    public static void SetColumns(this Grid grid, int count)
    {
        grid.ColumnDefinitions.Clear();
        for (int i = 0; i < count; i++)
            grid.ColumnDefinitions.Add(new ColumnDefinition());
    }

    public static void SetRows(this Grid grid, int count)
    {
        grid.RowDefinitions.Clear();
        for (int i = 0; i < count; i++)
            grid.RowDefinitions.Add(new RowDefinition());
    }

    public static IList<T> ChildrenAs<T>(this Grid grid) =>
        grid.Children.Select(x => (T)x).ToList();

    public static void ReplaceChild(this Grid grid, View oldView, View newView)
    {
        var locationConfiguration = oldView.GetGridPosition();

        grid.Remove(oldView);

        newView.SetGridPosition(locationConfiguration);

        grid.Add(newView);
    }
}
