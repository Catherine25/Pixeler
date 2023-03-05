namespace Pixeler.Services;

public class Counter
{
    public event Action CountedToNull;

    public int Value => _value;
    private int _value;

    public void Increase() => _value++;

    public void Decrease()
    {
        _value--;

        if (_value == 0)
            CountedToNull();
    }
}
