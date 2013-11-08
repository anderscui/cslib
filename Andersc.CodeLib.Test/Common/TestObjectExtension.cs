using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestObjectExtension
    {
        [Test]
        public void TestValueTypeIsNull()
        {
            int i = 1;
            Assert.IsFalse(i.IsNull());
        }

        [Test]
        public void TestRefTypeIsNull()
        {
            string s = null;
            Assert.IsTrue(s.IsNull());

            s = string.Empty;
            Assert.IsFalse(s.IsNull());
        }

        [Test]
        public void TestIn()
        {
            List<string> list = new List<string>() { "Hello", "World", "Anders" };
            Assert.That("Anders".In(list), Is.True);
            Assert.That("Cui".In(list), Is.False);
        }

        [Test]
        public void TestDoesHaveValue()
        {
            int? ni = null;
            Assert.IsTrue(ni.DoesNotHaveValue());

            ni = 10;
            Assert.IsTrue(ni.HasValue);
        }
    }
}
