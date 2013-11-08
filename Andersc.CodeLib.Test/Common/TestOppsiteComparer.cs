using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestOppsiteComparer
    {
        [Test]
        public void TestComparer()
        {
            OppsiteComparer<int> comparer = new OppsiteComparer<int>(Comparer<int>.Default);
            Assert.That(comparer.Compare(1, 2), Is.GreaterThan(0));
            Assert.That(comparer.Compare(3, 2), Is.LessThan(0));
            Assert.That(comparer.Compare(5, 5), Is.EqualTo(0));
        }
    }
}
