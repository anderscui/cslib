using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common
{
    public static class ConversionExtension
    {
        public static int Int(this char b)
        {
            return Convert.ToInt32(b);
        }
    }
}
