using Pixeler.Models.Colors;
using Pixeler.Views;

namespace Pixeler.Services
{
    public static class ColoringFuncService
    {
        /// <summary>
        /// 1st - extisting
        /// 2nd - current selected
        /// 3rd - resulting
        /// </summary>
        /// <param name="modes"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static Func<ColorData, ColorData, ColorData> GetForMode(Modes modes)
        {
            if (modes == Modes.Direct)
                return (extisting, selected) => selected;

            if(modes == Modes.LayeredBigToSmall_Acryllic) // todo check
                return (extisting, selected) => extisting.L >= selected.L ? selected : null;

            throw new NotSupportedException();
        }
    }
}