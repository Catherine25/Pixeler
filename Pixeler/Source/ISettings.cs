using Pixeler.Source.Colors;

namespace Pixeler.Source;

public interface ISettings
{
    public ColorData AccentColor { get; }
    public Size BitmapSize { get; }
    public int PaletteSize { get; }
    public string SoundPath { get; }
    public (int Min, int Max) LevelSizeRange { get; }
    public int PixelHighlightThickness { get; }
    public int PixelMargin { get; }
}