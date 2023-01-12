using Pixeler.Models.Colors;
using Pixeler.Views;

namespace Pixeler.Services
{
    public static class ColoringFuncService
    {
        private static Dictionary<Modes, Func<ColorData, ColorData, ColorData>> ModeToColorerFunc = new Dictionary<Modes, Func<ColorData, ColorData, ColorData>>
        {
            {
                Modes.Direct,
                (extisting, selected) => DirectColorerFunc(extisting, selected)
            },
            {
                Modes.LayeredBigToSmall_Acryllic,
                (extisting, selected) => LayeredBigToSmallAcryllic_ColorerFunc(extisting, selected)
            }
        };

        /// <summary>
        /// 1st - extisting
        /// 2nd - current selected
        /// 3rd - resulting
        /// </summary>
        /// <param name="modes"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static Func<ColorData, ColorData, ColorData> GetForMode(Modes modes) => ModeToColorerFunc[modes];

        private static ColorData DirectColorerFunc(ColorData extisting, ColorData selected) => selected;

        private static ColorData LayeredBigToSmallAcryllic_ColorerFunc(ColorData extisting, ColorData selected) => extisting.L >= selected.L ? selected : null;
    }
}