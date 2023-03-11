using Pixeler.Source.Colors;
using Pixeler.Source.Configuration.Coloring;

namespace Pixeler.Source.Drawing.Pixels;

public interface IColoringFuncService
{
    public Func<ColorData, ColorData, ColorData, MixingResult> GetForMode(ColoringConfiguration coloringConfiguration);
}