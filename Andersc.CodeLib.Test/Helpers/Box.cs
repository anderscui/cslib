using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Tester.Helpers
{
    public class Box
    {
        public int Height { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        
        public Box(int h, int l, int w)
        {
            this.Height = h;
            this.Length = l;
            this.Width = w;
        }
    }

    internal class BoxEqualityComparer : IEqualityComparer<Box>
    {
        #region IEqualityComparer<Box> Members

        public bool Equals(Box x, Box y)
        {
            if (x.Height == y.Height 
                && x.Length == y.Length
                && x.Width == y.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Box box)
        {
            int hCode = box.Height ^ box.Length ^ box.Width;
            return hCode.GetHashCode();
        }

        #endregion
    }
}
