using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestNumbers
    {
        [Test]
        public void TestModular()
        {
            Assert.That(6 % 4, Is.EqualTo(2));
            Assert.That(-6 % 4, Is.EqualTo(-2));
            Assert.That(6 % -4, Is.EqualTo(2));
            Assert.That(-6 % -4, Is.EqualTo(-2));
        }
    }
}
