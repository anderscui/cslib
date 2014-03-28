using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Maths
{
    // TODO: members
    // Size -> Size; Capacity -> Size
    // Rows; Columns
    // index: (i by column), (i, j)
    // rows[i]; cols[i]
    // range: m((r1, r2), (c1, c2))
    // range: m((), (c1, c2))
    // range: m((r1, r2), ())
    // assignment: m(i) <- val; m(i, j) <- val; (may auto resize)
    // +, *, -, * vector, * scalar,
    // class Vector
    // scale; swap; replace; gauss; echelon
    // solve equation system; 
    public abstract class Matrix
    {
        public abstract double this[int i, int j] { get; set; }
        public abstract int Rows { get; }
        public abstract int Columns { get; }
        public abstract Matrix Transpose { get; }
        public abstract Matrix Plus(Matrix other);
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
