using Pixeler.Extensions;

namespace Pixeler.Models.Colors;

public abstract class Color
{
    protected Color(string hex)
    {
        if (hex == null)
            Hex = "00000000";
        else
        {
            Hex = hex.Remove("#");

            if (Hex.Length > 6)
                Hex = Hex.Substring(2, 6);
        }
    }

    public string Hex { get; protected set; }
}
