using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Andersc.CodeLib.Common
{
    public static class MathHelper
    {
        private static readonly string DigitTable = "0123456789abcdef";
        private static readonly int MinBase = 2;
        private static readonly int MaxBase = DigitTable.Length;
        private static readonly char MinusChar = '-';
        private static readonly double CalculationErrorRange = 1e-5;

        /// <summary>
        /// Use sieve of Eratosthenes to filter primes out.
        /// </summary>
        /// <param name="n">specifed upper bound to test.</param>
        /// <returns>List of primes not greater than n.</returns>
        public static List<int> GetPrimes(int n)
        {
            if (n < 1) { throw new ArgumentOutOfRangeException("n", "n should be a positive number."); }
            if (n == 1) { return new List<int>(); }

            List<int> primes = new List<int>();
            BitArray flags = new BitArray(n + 1, true);
            int firstPrime = 2;

            // TODO: Math.Sqrt(n) + 1?
            for (int i = firstPrime; i <= Math.Sqrt(n) + 1; i++)
            {
                if (flags[i])
                {
                    for (int j = i * i; j <= n; j += i)
                    {
                        flags.Set(j, false);
                    }
                }
            }

            for (int i = firstPrime; i <= n; i++)
            {
                if (flags[i])
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        /// <summary>
        /// Gets Fibonacci number of the specified position.
        /// </summary>
        /// <param name="n">The position, first value is one.</param>
        /// <returns>The Fibonacci number of the specified position</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the position is less than one.</exception>
        public static int Fib(int n)
        {
            if (n < 0) { 
                throw new ArgumentOutOfRangeException("n", "n should be a postive integer."); }
            if (n <= 1) { return n; }

            int previousButOne = 0;
            int previous = 1;
            int answer = 1;

            for (int i = 2; i < n; i++)
            {
                previousButOne = previous;
                previous = answer;
                answer = previousButOne + previous;
            }

            return answer;
        }

        /// <summary>
        /// Gets Fibonacci number of the specified position by a recursive way.
        /// </summary>
        /// <param name="n">The position, first value is zero.</param>
        /// <returns></returns>
        public static int FibRec(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n", "n should be a postive integer.");
            }
            if (n <= 1) { return n; }

            return FibRec(n - 1) + FibRec(n - 2);
        }

        public static string GetStringOfInt(int number, int toBase)
        {
            if (toBase < MinBase || toBase > MaxBase)
            {
                throw new ArgumentOutOfRangeException("toBase");
            }

            // Here uses a List<char> to hold all the chars, also can use other containers like Stack<char>.
            List<char> chars = new List<char>();
            if (number < 0)
            {
                chars.Add(MinusChar);
                number = Math.Abs(number);
            }
            GetStringOfInt(number, toBase, chars);

            return new string(chars.ToArray());
        }

        private static void GetStringOfInt(int number, int baseOfNumber, List<char> chars)
        {
            if (number >= baseOfNumber)
            {
                GetStringOfInt(number / baseOfNumber, baseOfNumber, chars);
            }
            chars.Add(DigitTable[number % baseOfNumber]);
        }

        public static int GCD(int a, int b)
        {
            if (b == 0) { return a; }

            if (IsDivisibleBy(a, b))
            {
                return b;
            }

            return GCD(b, a % b);
        }

        public static long GCD(long a, long b)
        {
            if (b == 0) { return a; }

            if (IsDivisibleBy(a, b))
            {
                return b;
            }

            return GCD(b, a % b);
        }

        public static int LCM(int a, int b)
        {
            return a * b / GCD(a, b);
        }

        public static long LCM(long a, long b)
        {
            return a * b / GCD(a, b);
        }

        /// <summary>
        /// Determines whether the first number is divisible by the second number.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>
        /// 	<c>true</c> if a is divisible by b; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDivisibleBy(int a, int b)
        {
            return (a % b == 0);
        }

        public static bool IsDivisibleBy(long a, long b)
        {
            return (a % b == 0);
        }

        /// <summary>
        /// Gets the next prime greater than or equal to n.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static int GetNextPrime(int number)
        {
            if (number % 2 == 0)
            {
                number++;
            }

            for (; !IsPrime(number); number += 2) ;

            return number;
        }

        /// <summary>
        /// Determines whether the specified number is prime.
        /// </summary>
        /// <param name="n">The number.</param>
        /// <returns>
        /// 	<c>true</c> if the number is prime; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPrime(int n)
        {
            if (n <= 0) { throw new ArgumentOutOfRangeException("n"); }

            if (n == 2 || n == 3) { return true; }

            if (n == 1 || n % 2 == 0) { return false; }

            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static Tuple<QuadraticEquation, double, double> SolveQuadraticEquation(double a, double b, double c)
        {
            if (Math.Abs(a) < CalculationErrorRange) { throw new ArgumentOutOfRangeException("a"); }

            double delta = b * b - 4 * a * c;
            if (delta < 0.0)
            {
                return new Tuple<QuadraticEquation, double, double>(QuadraticEquation.NoSolution, double.NaN, double.NaN);
            }
            else if(delta > 0.0)
            {
                double sd = Math.Sqrt(delta);
                double root1 = (-b + sd) / (2 * a);
                double root2 = (-b - sd) / (2 * a);
                return new Tuple<QuadraticEquation, double, double>(QuadraticEquation.DiffRoots, root1, root2);
            }
            else
            {
                double root = -b / (2 * a);
                return new Tuple<QuadraticEquation, double, double>(QuadraticEquation.DuplicateRoots, root, root);
            }
        }

        public enum QuadraticEquation
        {
            NoSolution,
            DuplicateRoots,
            DiffRoots
        }

        // TODO: better precision.
        public static double CalculatePI()
        {
            double pi, n, s, t;
            pi = 0; n = 1; s = 1; t = s / (2 * n - 1);
            while (Math.Abs(t) >= 1e-8)
            {
                pi += t;
                s = -s;
                n++;
                t = s / (2 * n - 1);
            }

            return 4 * pi;
        }

        public static int GetBitsOfInt32(int n)
        {
            if (n < 0) { throw new ArgumentOutOfRangeException(); }

            int count = 1;
            while (n > 1)
            {
                count++;
                n >>= 1;
            }

            return count;
        }
    }
}
