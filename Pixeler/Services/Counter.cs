namespace Pixeler.Services;

public class Counter
{
    public event Action CountedToNull;

    private int Value { get; set; }

    public void Increase() => Value++;

    public void Decrease()
    {
        Value--;

        if (Value == 0)
            CountedToNull();
    }
}
