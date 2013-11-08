using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Algorithm
{
    internal class StringPrefixComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x.StartsWith(y))
            {
                return 0;
            }
            else
            {
                return x.CompareTo(y);
            }
        }
    }
}
