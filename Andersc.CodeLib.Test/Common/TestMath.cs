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
    public class TestMath
    {
        [Test]
        public void TestModularOperator()
        {
            int actual = 3 % 5;
            Assert.That(actual, Is.EqualTo(3));

            actual = 5 % 3;
            Assert.That(actual, Is.EqualTo(2));

            actual = 0 % 5;
            Assert.That(actual, Is.EqualTo(0));

            actual = 6 % 2;
            Assert.That(actual, Is.EqualTo(0));

            actual = -2 % 3;
            Assert.That(actual, Is.EqualTo(-2));

            actual = -5 % 3;
            Assert.That(actual, Is.EqualTo(-2));

            actual = 5 % -3;
            Assert.That(actual, Is.EqualTo(2));

            actual = 3 % -5;
            Assert.That(actual, Is.EqualTo(3));
        }

        [Test]
        public void TestDivisionOperator()
        {
            int actual = 3 / 5;
            Assert.That(actual, Is.EqualTo(0));

            actual = 5 / 3;
            Assert.That(actual, Is.EqualTo(1));

            actual = 0 / 5;
            Assert.That(actual, Is.EqualTo(0));

            actual = 6 / 2;
            Assert.That(actual, Is.EqualTo(3));

            actual = -2 / 3;
            Assert.That(actual, Is.EqualTo(0));

            actual = -5 / 3;
            Assert.That(actual, Is.EqualTo(-1));

            actual = -3 / 5;
            Assert.That(actual, Is.EqualTo(0));

            actual = 5 / -3;
            Assert.That(actual, Is.EqualTo(-1));

            actual = 3 / -5;
            Assert.That(actual, Is.EqualTo(0));
        }
    }
}
