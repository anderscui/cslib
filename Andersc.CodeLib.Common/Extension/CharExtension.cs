using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Extension
{
    public static class CharExtension
    {
        private static int MinChineseCode = 19968;
        private static int MaxChineseCode = 40869;
        private static int ChineseCharsCount = 20902;

        public static bool IsChineseChar(this char ch)
        {
            var code = Convert.ToInt32(ch);
            return (code >= MinChineseCode && code <= MaxChineseCode);
        }
    }
}
