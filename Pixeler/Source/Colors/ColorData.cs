using MColor = Microsoft.Maui.Graphics.Color;

namespace Pixeler.Source.Colors;

public sealed class ColorData : HslColor
{
    public ColorData(string hex) : base(hex) { }

    public MColor MColor => MColor.FromArgb(Hex);

    public const double StepH = 10;
    public const double StepL = 0.1;
    public const double StepS = 0.1;

    public static bool operator ==(ColorData x, ColorData y) => x?.R == y?.R && x?.G == y?.G && x?.B == y?.B;
    public static bool operator !=(ColorData x, ColorData y) => !(x == y);

    public override bool Equals(object obj) => ToString() == obj.ToString();

    public override int GetHashCode() => Hex.GetHashCode();

    public override string ToString() => Hex;

    public void MakeLighter(bool lighter)
    {
        if (lighter)
        {
            //yellower => 60
            double h = MoveToNumber(value: H, needed: 60, StepH);
            double l = MoveToNumber(value: L, needed: 1, StepL);
            double s = MoveToNumber(value: S, needed: 0, StepS);

            SetHsl(h, l, s);
        }
        else
        {
            //bluer => 240
            double h = MoveToNumber(value: H, needed: 240, StepH);
            double l = MoveToNumber(value: L, needed: 0, StepL);
            double s = MoveToNumber(value: S, needed: 1, StepS);

            SetHsl(h, l, s);
        }
    }

    private static double MoveToNumber(double value, double needed, double step)
    {
        // copy value
        if (value == needed)
            return needed;
        else if (value > needed)
        {
            value -= step;
            if (value < needed)
                return needed;
        }
        else
        {
            value += step;
            if (value > needed)
                return needed;
        }

        return value;
    }
}
