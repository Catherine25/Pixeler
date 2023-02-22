namespace Pixeler.ExtendedViews;

public class AutoExtendableTypedGrid<T> : TypedGrid<T>
    where T : IView
{
    public void AddToRight(T view)
    {
        Add(view, new Point(Columns, Rows));
        Columns++;
    }

    public void AddToBottom(T view)
    {
        Add(view, new Point(Columns, Rows));
        Rows++;
    }
}
