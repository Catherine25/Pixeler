﻿using Pixeler.Source.Colors;
using Pixeler.Source.Configuration.Coloring;

namespace Pixeler.Source.Drawing.Pixels;

public class MixingResult
{
    public ColorData Result;
    public bool IsFinal;
    public MixingResult(ColorData result, bool isFinal = true)
    {
        Result = result;
        IsFinal = isFinal;
    }
}

public class ColoringFuncService : IColoringFuncService
{
    private readonly Dictionary<ColoringConfiguration, Func<ColorData, ColorData, ColorData, MixingResult>> ColorerFuncs = new();

    public ColoringFuncService()
    {
        ColorerFuncs.Add(new(PixelGrouping.None, Layoring.Oil), Direct_ColorerFunc);
        ColorerFuncs.Add(new(PixelGrouping.None, Layoring.Acryllic), DirectAcryllic_ColorerFunc);
    }

    /// <summary>
    /// 1st - Color of the pixel in drawing area.
    /// 2nd - Current selected brush color.
    /// 3rd - Color of the pixel in the original image.
    /// </summary>
    /// <returns>The resulting color.</returns>
    public Func<ColorData, ColorData, ColorData, MixingResult> GetForMode(ColoringConfiguration coloringConfiguration) => ColorerFuncs[coloringConfiguration];
    private MixingResult Direct_ColorerFunc(ColorData extisting, ColorData selected, ColorData original)
    {
        // skip already colored
        if (extisting == original)
            return null;

        // skip if selected color is not same as original
        if (selected != original)
            return null;

        return new MixingResult(original);
    }

    /// <summary>
    /// Gets difference of lightness between selected and existing.
    /// If lightness of the selected color is less than lightness of the pixel - returns the selected color.
    /// </summary>
    private MixingResult DirectAcryllic_ColorerFunc(ColorData extisting, ColorData selected, ColorData original)
    {
        // skip already colored
        if (extisting == original)
            return null;

        // if selected color is same to original - just apply it
        if (selected == original)
            return new MixingResult(selected);

        // apply color
        ColorData candidateColor = SumColors(extisting, selected);

        // skip if the candidate color is darker than needed
        if (candidateColor.IsDarkerThan(original))
            return null;

        // skip if the candidate color has different hue
        if ((int)candidateColor.H != (int)original.H)
            return null;
        return new MixingResult(candidateColor, original == candidateColor);
    }

    private ColorData SumColors(ColorData c1, ColorData c2)
    {
        int r = Math.Min(c1.R + c2.R, 255);
        int g = Math.Min(c1.G + c2.G, 255);
        int b = Math.Min(c1.B + c2.B, 255);
        var color = System.Drawing.Color.FromArgb(Convert.ToByte(r),
                         Convert.ToByte(g),
                         Convert.ToByte(b));
        return new ColorData(color.Name);
    }
}
