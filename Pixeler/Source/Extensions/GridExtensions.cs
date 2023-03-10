using Pixeler.Source.Extensions;

namespace Pixeler.Source.Extensions;

public static class GridExtensions
{
    public static int GetColumns(this Grid grid) => grid.ColumnDefinitions.Count;
    public static void SetColumns(this Grid grid, int count)
    {
        int current = grid.GetColumns();
        int difference = count - current;

        if (difference == 0)
            return;

        for (int i = 0; i < difference; i++)
            if (difference > 0)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            else
                grid.ColumnDefinitions.RemoveAt(grid.ColumnDefinitions.Count - 1);
    }

    public static int GetRows(this Grid grid) => grid.RowDefinitions.Count;
    public static void SetRows(this Grid grid, int count)
    {
        int current = grid.GetRows();
        int difference = count - current;

        if (difference == 0)
            return;

        for (int i = 0; i < difference; i++)
            if (difference > 0)
                grid.RowDefinitions.Add(new RowDefinition());
            else
                grid.RowDefinitions.RemoveAt(grid.RowDefinitions.Count - 1);
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

    public static void Add(this Grid grid, View view, Point point) =>
        grid.Add(view, (int)point.X, (int)point.Y);
}
