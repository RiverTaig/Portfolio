using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PuzzlePalServer
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// returns int.minValue if the string is not a valid integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            int retInt = -1;
            if (int.TryParse(value, out retInt))
            {
                return retInt;
            }
            else
            {
                return int.MinValue;
            }
        }
        /// <summary>
        /// Returns white if there is an error
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color ToColor(this string value)
        {
            try
            {
                return System.Drawing.ColorTranslator.FromHtml(value);
            }
            catch
            {
                return Color.White;
            }
        }
    }
}
