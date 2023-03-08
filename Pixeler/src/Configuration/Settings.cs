using Pixeler.Models.Colors;
using MColor = Microsoft.Maui.Graphics.Colors;

namespace Pixeler.Configuration;

public class Settings : ISettings
{
    public ColorData AccentColor => new(MColor.BlueViolet.ToHex());
    public Size BitmapSize => new(10, 10);
    public int PaletteSize => 10;
    public string SoundPath => "demo-2.wav";
    public (int Min, int Max) LevelSizeRange { get => (2, 16); }
    public int PixelHighlightThickness { get => 2; }
    public int PixelMargin { get => 2; }
}