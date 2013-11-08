using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Maths
{
    // TODO: Unit Test Cases
    public static class Maths
    {
        /// <summary>
        /// Evaluates a polynomial.
        /// </summary>
        /// <param name="a">Array of factors.</param>
        /// <param name="n"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Horner(double[] a, int n, double x)
        {
            double result = 0.0;

            result = a[n];
            for (int i = n - 1; i >= 0; --i)
            {
                result = result * x + a[i];
            }

            return result;
        }
    }
}
