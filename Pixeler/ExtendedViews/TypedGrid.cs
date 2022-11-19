using Pixeler.Extensions;

namespace Pixeler.ExtendedViews;

public class TypedGrid<T>
    where T : IView
{
    public Grid Grid { get; }

    public TypedGrid() => Grid = new Grid();

    public T this[int index]
    {
        get => Grid.ChildrenAs<T>()[index];
        set => Grid.ChildrenAs<T>()[index] = value;
    }

    public Size Size
    {
        set
        {
            Columns = (int)value.Width;
            Rows = (int)value.Height;
        }
    }

    public int Columns
    {
        get => Grid.GetColumns();
        set => Grid.SetColumns(value);
    }

    public int Rows
    {
        get => Grid.GetRows();
        set => Grid.SetRows(value);
    }

    public IList<T> Children => Grid.ChildrenAs<T>();
    public int Count => Grid.Count;

    public void Add(T item, Point point) => Add(item, (int)point.X, (int)point.Y);
    public void Add(T item, int x, int y) => Grid.Add(item, x, y);
    public void Remove(T item) => Grid.Remove(item);
}
