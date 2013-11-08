using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Andersc.CodeLib.Common.Extension
{
    // TODO: Better impl.
    public static class ConsoleExtension
    {
        public static int GetInt32(this TextReader c)
        {
            return Convert.ToInt32(c.ReadLine());
        }
    }
}
