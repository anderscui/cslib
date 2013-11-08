using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common
{
    public static class BooleanExtension
    {
        public static bool IsTrue(this bool b)
        {
            return b;
        }

        public static bool IsFalse(this bool b)
        {
            return !b;
        }
    }
}
