/*
 * Created by: Anders Cui
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestBitOperators
    {
        [Test]
        public void TestShiftLeft()
        {
            int i = 3; // 0000 0011
            Assert.That(i << 1, Is.EqualTo(6)); // 0110
            Assert.That(i << 2, Is.EqualTo(12)); // 1100

            Console.WriteLine(1 << 31);
        }

        [Test]
        public void TestShiftRight()
        {
            int i = 6; // 0000 0110
            Assert.That(i >> 1, Is.EqualTo(3)); // 0011
            Assert.That(i >> 2, Is.EqualTo(1)); // 0001
        }

        [Test]
        public void TestBitComplement()
        {
            int i = 4; // 0000 0100
            Assert.That(~i, Is.EqualTo(-5));

            i = -5; // 0000 0100
            Assert.That(~i, Is.EqualTo(4));

            i = 5; // 0000 0100
            Assert.That(~i, Is.EqualTo(-6));
        }
    }
}
