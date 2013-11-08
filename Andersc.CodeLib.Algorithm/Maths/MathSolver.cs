using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Andersc.CodeLib.Common.Maths;

namespace Andersc.CodeLib.Algorithm.Maths
{
    public static class MathSolver
    {
        // x > 0, y > 0
        // and x + y is square, x*x + y*y is square, x*x*x + y*y*y is square,
        public static Tuple<int, int> Q1()
        {
            int ub1 = 1000, ub2 = 1000;
            for (int i = 1; i <= ub1; i++)
            {
                for (int j = 1; j <= ub2; j++)
                {
                    if (NumberTheory.IsSquare(i + j)
                        && NumberTheory.IsSquare(i*i + j*j)
                        && NumberTheory.IsSquare(i*i*i + j*j*j))
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }
            return new Tuple<int, int>(0, 0);
        }
    }
}
