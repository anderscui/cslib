using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Maths
{
    // TODO: Unit Test Cases
    public abstract class Matrix
    {
        public abstract double this[int i, int j] { get; set; }
        public abstract int Rows { get; }
        public abstract int Columns { get; }
        public abstract Matrix Transpose { get; }
        public abstract Matrix Plus(Matrix other);
        // TODO: 
        public abstract Matrix Times(Matrix other);
        
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return m1.Plus(m2);
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            return m1.Times(m2);
        }
    }
}
