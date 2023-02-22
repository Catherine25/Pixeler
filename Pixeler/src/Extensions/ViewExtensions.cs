namespace Pixeler.Extensions;

public static class ViewExtensions
{
    public static Rect GetGridPosition(this View view) => new()
    {
        X = Grid.GetColumn(view),
        Y = Grid.GetRow(view),
        Width = Grid.GetColumnSpan(view),
        Height = Grid.GetRowSpan(view)
    };

    public static void SetGridPosition(this View view, Rect rect)
    {
        Grid.SetColumn(view, (int)rect.X);
        Grid.SetRow(view, (int)rect.Y);
        Grid.SetColumnSpan(view, (int)rect.Width);
        Grid.SetRowSpan(view, (int)rect.Height);
    }
}
