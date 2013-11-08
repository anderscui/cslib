using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester
{
    [TestFixture]
    public class TestNumbers
    {
        [Test]
        public void TestFloatingPointNumber()
        {
            float f1 = 1.333f;
            float f2 = 1.333f;
            //Assert.AreEqual(2.666000f, f1 + f2);
            Assert.That(f1 + f2, Is.EqualTo(2.666).Within(0.0000001));
        }
    }
}
