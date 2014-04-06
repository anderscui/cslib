using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Maths
{
    public class Vector<T>
    {
        private bool autoScale;
        private T[] data;

        public Vector(int size, bool autoScale = true)
        {
            this.autoScale = autoScale;
            this.data = new T[size];
        }

        public Vector(IEnumerable<T> data, bool autoScale = true)
        {
            this.autoScale = autoScale;
            data.ToArray().CopyTo(this.data, 0);
        }

        public Size Size
        {
            get { return new Size(){ First = data.Length, Second = 1 };}
        }
    }
}
