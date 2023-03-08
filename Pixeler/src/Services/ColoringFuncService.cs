using Pixeler.Models.Colors;
using Pixeler.Views;

namespace Pixeler.Services
{
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

    public static class ColoringFuncService
    {
        private static readonly Dictionary<Modes, Func<ColorData, ColorData, ColorData, MixingResult>> ModeToColorerFunc = new()
        {
            {
                Modes.Direct, Direct_ColorerFunc
            },
            {
                Modes.Layered_Acryllic, LayeredBigToSmallAcryllic_ColorerFunc
            }
        };

        /// <summary>
        /// 1st - Color of the pixel in drawing area.
        /// 2nd - Current selected brush color.
        /// 3rd - Color of the pixel in the original image.
        /// </summary>
        /// <returns>The resulting color.</returns>
        public static Func<ColorData, ColorData, ColorData, MixingResult> GetForMode(Modes modes) => ModeToColorerFunc[modes];

        private static MixingResult Direct_ColorerFunc(ColorData extisting, ColorData selected, ColorData original)
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
        private static MixingResult LayeredBigToSmallAcryllic_ColorerFunc(ColorData extisting, ColorData selected, ColorData original)
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
            if((int)candidateColor.H != (int)original.H)
                return null;

            return new MixingResult(candidateColor, original == candidateColor);
        }

        private static ColorData SumColors(ColorData c1, ColorData c2)
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
}