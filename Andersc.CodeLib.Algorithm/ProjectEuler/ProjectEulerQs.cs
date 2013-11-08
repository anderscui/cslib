using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.Maths;

namespace Andersc.CodeLib.Algorithm.ProjectEuler
{
    public static class ProjectEulerQs
    {
        /// <summary>
        /// Finds greatest prime factor.
        /// </summary>
        public static long Q003(long n)
        {
            // n > 1;
            List<long> factors = new List<long>();
            factors.Add(n);

            long sqrt = (long)Math.Floor(Math.Sqrt(n));
            for (long i = 2; i <= sqrt; i++)
            {
                if (n % i == 0)
                {
                    factors.Add(i);
                    factors.Add(n / i);
                }
            }

            factors.Sort();
            for (int i = factors.Count - 1; i >= 0; i--)
            {
                if (NumberTheory.IsPrime(factors[i]))
                {
                    return factors[i];
                }
            }

            return -1;
        }

        /// <summary>
        /// Finds the largest palindrome made from the product of two 3-digit numbers.
        /// </summary>
        /// <returns></returns>
        public static int Q004()
        {
            int result = -1;
            int current = -1;
            for (int i = 100; i < 1000; i++)
            {
                for (int j = 100; j < 1000; j++)
                {
                    current = i * j;
                    if (current.ToString().IsPalindromic()
                        && (current > result))
                    {
                        result = current;
                        Console.WriteLine("{0} * {1}", i, j);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// What is the smallest number divisible by each of the numbers 1 to 20?
        /// </summary>
        /// <returns></returns>
        public static long Q005()
        {
            long result = 2;
            for (long i = 3; i <= 20; i++)
            {
                result = MathHelper.LCM(result, i);
            }

            return result;
        }

        public static int Q008()
        {
            string numStr = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
            int len = numStr.Length;
            int max = 0;
            int current = 0;
            for (int i = 0; i <= len - 5; i++)
            {
                current = (int)char.GetNumericValue(numStr, i)
                         * (int)char.GetNumericValue(numStr, i + 1)
                         * (int)char.GetNumericValue(numStr, i + 2)
                         * (int)char.GetNumericValue(numStr, i + 3)
                         * (int)char.GetNumericValue(numStr, i + 4);
                if (current > max)
                {
                    max = current;
                }
            }

            return max;
        }

        // Q010: 142913828922;
        public static long Q010()
        {
            int n = 2000000;
            List<int> primes = NumberTheory.GetPrimesBelow(n);
            // 148933 primes;
            return primes.Select(i => (long)i).Sum();
        }

        public static int Q016(int n)
        {
            int digitCount = 0;
            byte[] digitArray = new byte[1001];

            digitArray.Init((byte)0);
            digitArray[1] = 1;
            digitCount = 1;

            byte digit = 0;
            byte c = 0, s = 0;
            int i, j;
            for (i = 1; i <= n; i++)
            {
                c = 0;
                for (j = 1; j <= digitCount; j++)
                {
                    digit = (byte)(digitArray[j] * 2 + c);
                    s = (byte)(digit % 10);
                    c = (byte)(digit / 10);
                    digitArray[j] = s;
                }
                if (c > 0)
                {
                    digitCount++;
                    digitArray[digitCount] += c;
                }
            }

            return digitArray.Skip(1).Take(digitCount).Select(b => (int)b).Sum();
        }
    }
}
