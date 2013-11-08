using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Algorithm;

namespace Andersc.CodeLib.Tester.Algorithm
{
    [TestFixture]
    public class TestQueens
    {
        [Test]
        public void TestSolve()
        {
            Queens q = new Queens(6);
            q.Solve(0);
        }
    }
}
