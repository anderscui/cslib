using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Maths
{
    public static class NumberTheory
    {
        // TODO: more numbers
        public static int[] PrimesLessThan1000 = new int[] 
                      { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 
                        31,   37,   41,   43,   47,   53,   59,   61,   67,   71, 
                        73,   79,   83,   89,   97,  101,  103,  107,  109,  113, 
                        127,  131,  137,  139,  149,  151,  157,  163,  167,  173, 
                        179,  181,  191,  193,  197,  199,  211,  223,  227,  229, 
                        233,  239,  241,  251,  257,  263,  269,  271,  277,  281, 
                        283,  293,  307,  311,  313,  317,  331,  337,  347,  349, 
                        353,  359,  367,  373,  379,  383,  389,  397,  401,  409, 
                        419,  421,  431,  433,  439,  443,  449,  457,  461,  463, 
                        467,  479,  487,  491,  499,  503,  509,  521,  523,  541, 
                        547,  557,  563,  569,  571,  577,  587,  593,  599,  601, 
                        607,  613,  617,  619,  631,  641,  643,  647,  653,  659, 
                        661,  673,  677,  683,  691,  701,  709,  719,  727,  733, 
                        739,  743,  751,  757,  761,  769,  773,  787,  797,  809, 
                        811,  821,  823,  827,  829,  839,  853,  857,  859,  863, 
                        877,  881,  883,  887,  907,  911,  919,  929,  937,  941, 
                        947,  953,  967,  971,  977,  983,  991,  997 };

        public static bool IsPrime(int n)
        {
            if (n <= 0) { throw new ArgumentOutOfRangeException("n"); }

            if (n == 1) { return false; }
            if (n == 2 || n == 3) { return true; }

            if (n % 2 == 0) { return false; }

            int upper = (int)Math.Ceiling(Math.Sqrt(n));
            for (int i = 3; i <= upper; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsPrime(long n)
        {
            if (n <= 0) { throw new ArgumentOutOfRangeException("n"); }

            if (n == 1 || n % 2 == 0) { return false; }
            if (n == 2 || n == 3) { return true; }

            int upper = (int)Math.Ceiling(Math.Sqrt(n));
            for (int i = 3; i <= upper; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static List<int> GetPrimesBelow(int n)
        {
            if (n <= 0) { throw new ArgumentOutOfRangeException("n"); }

            List<int> result = new List<int>();
            if (n == 1) { return result; }

            result.Add(2);
            if (n == 2) { return result; }

            for (int i = 3; i <= n; i += 2)
            {
                if (IsPrime(i))
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public static Tuple<int, int, int> ExtendedEuclid(int a, int b)
        {
            int c, d, e;
            int i, j, k;
            c = 1; d = 0; i = 0; j = 1; e = 0; k = 0;

            int m = a, n = b;
            int q = m / n;
            int r = m % n;
            while (r > 0)
            {
                e = c - q * d;
                k = i - q * j;

                m = n;
                n = r;
                q = m / n;
                r = m % n;

                c = d;
                d = e;
                i = j;
                j = k;
            }

            return new Tuple<int, int, int>(e * a + k * b, e, k);
        }

        public static int GetFactorial(int n)
        {
            if (n < 0) { throw new ArgumentOutOfRangeException("n must be zero or positive."); }
            return Factorial(n);
        }

        private static int Factorial(int n)
        {
            if (n == 0) { return 1; }
            return n * Factorial(n - 1);
        }

        public static bool IsSquare(long n)
        {
            // TODO: this is a common exception
            if (n < 0) { throw new ArgumentOutOfRangeException("n should be zero or positive."); }

            if (n <= 1) { return true; }

            long sqrt = (long)Math.Floor(Math.Sqrt(n));
            return sqrt * sqrt == n;
        }
    }
}
