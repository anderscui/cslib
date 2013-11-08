using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Maths;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestNumberTheory
    {
        [Test]
        public void TestPrimesLessThan1000()
        {
            Assert.That(NumberTheory.PrimesLessThan1000.Length, Is.EqualTo(168));
        }

        [Test]
        public void TestIsPrime()
        {
            Assert.That(NumberTheory.IsPrime(2), Is.True);
            Assert.That(NumberTheory.IsPrime(3), Is.True);
            Assert.That(NumberTheory.IsPrime(4), Is.False);
            Assert.That(NumberTheory.IsPrime(5), Is.True);
            Assert.That(NumberTheory.IsPrime(10), Is.False);
            Assert.That(NumberTheory.IsPrime(17), Is.True);
            Assert.That(NumberTheory.IsPrime(641), Is.True);
            //unchecked
            //{
            //    Assert.That(NumberTheory.IsPrime(int.MaxValue + 1), Is.True);
            //}

            foreach (var i in NumberTheory.PrimesLessThan1000)
            {
                Assert.That(NumberTheory.IsPrime(i), Is.True);
            }
        }

        [Test]
        public void TestIsPrime_Int64()
        {
            Assert.That(NumberTheory.IsPrime(2), Is.True);
            Assert.That(NumberTheory.IsPrime(3), Is.True);
            Assert.That(NumberTheory.IsPrime(4), Is.False);
            Assert.That(NumberTheory.IsPrime(5), Is.True);
            Assert.That(NumberTheory.IsPrime(10), Is.False);
            Assert.That(NumberTheory.IsPrime(17), Is.True);
            Assert.That(NumberTheory.IsPrime(641), Is.True);
            Assert.That(NumberTheory.IsPrime((long)(int.MaxValue) + 1), Is.False);

            foreach (var i in NumberTheory.PrimesLessThan1000)
            {
                Assert.That(NumberTheory.IsPrime(i), Is.True);
            }
        }

        [Test]
        public void TestPrintPrimes()
        {
            int count = 0;
            for (int i = 2; i < 104800; i++)
            {
                if (NumberTheory.IsPrime(i))
                {
                    Console.Write(i + " ");

                    count++;
                    if (count % 10 == 0)
                    {
                        Console.WriteLine();
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine(count);
        }

        [Test]
        public void TestGetPrimesBelow()
        {
            int n = 10;
            List<int> primes = NumberTheory.GetPrimesBelow(n);
            Assert.That(primes.Count, Is.EqualTo(4));
            Assert.That(primes.Sum(), Is.EqualTo(17));

            n = 2000000;
            primes = NumberTheory.GetPrimesBelow(n);
            Assert.That(primes.Count, Is.EqualTo(148933));
            Assert.That(primes.Select(i => (long)i).Sum(), Is.EqualTo(17));
        }

        [Test]
        public void TestExtendedEuclid()
        {
            var result = NumberTheory.ExtendedEuclid(6, 4);
            Console.WriteLine("{0}, {1}, {2}", result.Item1, result.Item2, result.Item3);

            result = NumberTheory.ExtendedEuclid(120, 23);
            Console.WriteLine("{0}, {1}, {2}", result.Item1, result.Item2, result.Item3);
            CollectionAssert.AreEqual(new int[] { result.Item1, result.Item2, result.Item3 }, new int[] { 1, -9, 47 });

            result = NumberTheory.ExtendedEuclid(31415, 14142);
            Console.WriteLine("{0}, {1}, {2}", result.Item1, result.Item2, result.Item3);
        }

        [Test]
        public void TestIsSquare()
        {
            int n = 0;
            Assert.That(NumberTheory.IsSquare(n));

            n = 1;
            Assert.That(NumberTheory.IsSquare(n));

            n = 33;
            Assert.That(!NumberTheory.IsSquare(n));

            n = 49;
            Assert.That(NumberTheory.IsSquare(n));

            n = 123 * 123;
            Assert.That(NumberTheory.IsSquare(n));

            n = n + 1;
            Assert.That(!NumberTheory.IsSquare(n));
        }
    }
}
