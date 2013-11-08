using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestMathHelper
    {
        [Test]
        public void TestGetPrimes()
        {
            int n = 1;
            Assert.That(MathHelper.GetPrimes(n).Count, Is.EqualTo(0));

            n = 2;
            Assert.That(MathHelper.GetPrimes(n).Count, Is.EqualTo(1));
            n = 3;
            Assert.That(MathHelper.GetPrimes(n).Count, Is.EqualTo(2));
            n = 8;
            Assert.That(MathHelper.GetPrimes(n).Count, Is.EqualTo(4));
            n = 30;
            Assert.That(MathHelper.GetPrimes(n).Count, Is.EqualTo(10));

            n = 1000;
            Assert.That(MathHelper.GetPrimes(n).Count, Is.EqualTo(168));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetPrimesException()
        {
            MathHelper.GetPrimes(0);
        }

        [Test]
        public void TestFib()
        {
            int position = 0;
            Assert.That(MathHelper.Fib(position), Is.EqualTo(0));

            position = 1;
            Assert.That(MathHelper.Fib(position), Is.EqualTo(1));

            position = 2;
            Assert.That(MathHelper.Fib(position), Is.EqualTo(1));

            position = 3;
            Assert.That(MathHelper.Fib(position), Is.EqualTo(2));

            position = 6;
            Assert.That(MathHelper.Fib(position), Is.EqualTo(8));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFibException()
        {
            MathHelper.Fib(-1);
        }

        [Test]
        public void TestFibRec()
        {
            int position = 0;
            Assert.That(MathHelper.FibRec(position), Is.EqualTo(0));

            position = 1;
            Assert.That(MathHelper.FibRec(position), Is.EqualTo(1));

            position = 2;
            Assert.That(MathHelper.FibRec(position), Is.EqualTo(1));

            position = 3;
            Assert.That(MathHelper.FibRec(position), Is.EqualTo(2));

            position = 6;
            Assert.That(MathHelper.FibRec(position), Is.EqualTo(8));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFibRecException()
        {
            MathHelper.FibRec(-1);
        }

        [Test]
        public void TestGetStringOfInt_PositiveNumber()
        {
            int number = 20;
            int baseOfNumber = 10;
            Assert.That(MathHelper.GetStringOfInt(number, baseOfNumber), Is.EqualTo(number.ToString()));

            baseOfNumber = 16;
            Assert.That(MathHelper.GetStringOfInt(number, baseOfNumber), Is.EqualTo("14"));

            baseOfNumber = 8;
            Assert.That(MathHelper.GetStringOfInt(number, baseOfNumber), Is.EqualTo("24"));

            baseOfNumber = 2;
            Assert.That(MathHelper.GetStringOfInt(number, baseOfNumber), Is.EqualTo("10100"));
        }

        [Test]
        public void TestGetStringOfInt_Zero()
        {
            int number = 0;
            int baseOfNumber = 8;
            Assert.That(MathHelper.GetStringOfInt(number, baseOfNumber), Is.EqualTo(number.ToString()));
        }

        [Test]
        public void TestGetStringOfInt_NegativeNumber()
        {
            int number = -20;
            int baseOfNumber = 10;
            Assert.That(MathHelper.GetStringOfInt(number, baseOfNumber), Is.EqualTo(number.ToString()));

            baseOfNumber = 16;
            Assert.That(MathHelper.GetStringOfInt(number, baseOfNumber), Is.EqualTo("-14"));

            baseOfNumber = 8;
            Assert.That(MathHelper.GetStringOfInt(number, baseOfNumber), Is.EqualTo("-24"));

            baseOfNumber = 2;
            Assert.That(MathHelper.GetStringOfInt(number, baseOfNumber), Is.EqualTo("-10100"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetStringOfInt_Exception()
        {
            int number = 20;
            int baseOfNumber = 1;
            MathHelper.GetStringOfInt(number, baseOfNumber);
        }

        [Test]
        public void TestGCD()
        {
            int a = 4, b = 2;
            Assert.That(MathHelper.GCD(a, b), Is.EqualTo(2));

            a = 2;
            b = 4;
            Assert.That(MathHelper.GCD(a, b), Is.EqualTo(2));

            a = 4;
            b = 18;
            Assert.That(MathHelper.GCD(a, b), Is.EqualTo(2));

            a = 10;
            b = 25;
            Assert.That(MathHelper.GCD(a, b), Is.EqualTo(5));

            a = 10;
            b = 11;
            Assert.That(MathHelper.GCD(a, b), Is.EqualTo(1));

            a = 100;
            b = 1;
            Assert.That(MathHelper.GCD(a, b), Is.EqualTo(1));
        }

        [Test]
        public void TestIsDivisibleBy()
        {
            int a = 12, b = 4;
            Assert.That(MathHelper.IsDivisibleBy(a, b), Is.True);

            a = 12;
            b = 1;
            Assert.That(MathHelper.IsDivisibleBy(a, b), Is.True);

            a = 5;
            b = 2;
            Assert.That(MathHelper.IsDivisibleBy(a, b), Is.False);
        }

        [Test]
        public void TestLCM()
        {
            int a = 4, b = 2;
            Assert.That(MathHelper.LCM(a, b), Is.EqualTo(4));

            a = 6; b = 8;
            Assert.That(MathHelper.LCM(a, b), Is.EqualTo(24));

            a = 6; b = 7;
            Assert.That(MathHelper.LCM(a, b), Is.EqualTo(42));
        }

        [Test]
        public void TestSolveQuadraticEquation()
        {
            double a = 1.0, b = 1.0, c = -6.0;
            var sln = MathHelper.SolveQuadraticEquation(a, b, c);
            Assert.That(sln.Item1 == MathHelper.QuadraticEquation.DiffRoots);
            Assert.That(sln.Item2, Is.EqualTo(2.0));
            Assert.That(sln.Item3, Is.EqualTo(-3.0));

            a = 1.0; b = -4.0; c = 4.0;
            sln = MathHelper.SolveQuadraticEquation(a, b, c);
            Assert.That(sln.Item1 == MathHelper.QuadraticEquation.DuplicateRoots);
            Assert.That(sln.Item2, Is.EqualTo(2.0));
            Assert.That(sln.Item3, Is.EqualTo(2.0));

            a = 2.0; b = 3.0; c = 4.0;
            sln = MathHelper.SolveQuadraticEquation(a, b, c);
            Assert.That(sln.Item1 == MathHelper.QuadraticEquation.NoSolution);
            Assert.That(sln.Item2, Is.EqualTo(double.NaN));
            Assert.That(sln.Item3, Is.EqualTo(double.NaN));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSolveQuadraticEquation_Exception()
        {
            double a = 0.0, b = 1.0, c = -6.0;
            var sln = MathHelper.SolveQuadraticEquation(a, b, c);
        }

        [Test]
        public void TestPI()
        {
            Console.WriteLine(Math.PI);
            
            Console.WriteLine(MathHelper.CalculatePI());
        }

        [Test]
        public void TestGetBitsOfInt32()
        {
            int n = 0;
            Assert.That(MathHelper.GetBitsOfInt32(n), Is.EqualTo(1));

            n = 1;
            Assert.That(MathHelper.GetBitsOfInt32(n), Is.EqualTo(1));

            n = 3;
            Assert.That(MathHelper.GetBitsOfInt32(n), Is.EqualTo(2));

            n = 8;
            Assert.That(MathHelper.GetBitsOfInt32(n), Is.EqualTo(4));
        }
    }
}
