using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.Common.WinForm
{
    public static class WinFormExtension
    {
        public static char Char(this Keys key)
        {
            return (char) key;
        }
    }
}
