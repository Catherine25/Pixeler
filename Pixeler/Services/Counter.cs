namespace Pixeler.Services;

public class Counter
{
    public event Action CountedToNull;

    private int _value { get; set; }

    public void Increase() => _value++;

    public void Decrease()
    {
        _value--;

        if (_value == 0)
            CountedToNull();
    }
}
