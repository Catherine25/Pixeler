using Pixeler.Models.Colors;

namespace Pixeler.Models;

public interface ISettings
{
    public ColorData AccentColor { get; }
    public Size BitmapSize { get; }
    public int PaletteSize { get; }
    public string SoundPath { get; }
    public (int Min, int Max) LevelSizeRange { get; }
}