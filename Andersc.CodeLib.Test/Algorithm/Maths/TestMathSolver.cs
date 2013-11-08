using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Andersc.CodeLib.Algorithm.Maths;

namespace Andersc.CodeLib.Tester.Algorithm.Maths
{
    [TestFixture]
    public class TestMathSolver
    {
        [Test]
        public void TestQ1()
        {
            var res = MathSolver.Q1();
            Console.WriteLine("{0}, {1}", res.Item1, res.Item2);
        }
    }
}
