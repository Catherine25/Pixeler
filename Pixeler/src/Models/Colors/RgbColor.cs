namespace Pixeler.Models.Colors;

public abstract class RgbColor : Color
{
    protected RgbColor(string hex) : base(hex) { }

    public byte R => Convert.ToByte(Hex[..2], 16);
    public byte G => Convert.ToByte(Hex.Substring(2, 2), 16);
    public byte B => Convert.ToByte(Hex.Substring(4, 2), 16);

    public void SetRgb(byte r, byte g, byte b)
    {
        Hex = Convert.ToString(r, 16)
            + Convert.ToString(g, 16)
            + Convert.ToString(b, 16);
    }
}
