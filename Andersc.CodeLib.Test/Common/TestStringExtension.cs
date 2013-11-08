using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestStringExtension
    {
        [Test]
        public void TestIsNullOrEmpty()
        {
            string s = null;
            Assert.IsTrue(s.IsNullOrEmpty());

            s = string.Empty;
            Assert.IsTrue(s.IsNullOrEmpty());

            s = "  ";
            Assert.IsFalse(s.IsNullOrEmpty());

            s = "hello";
            Assert.IsFalse(s.IsNullOrEmpty());
        }

        [Test]
        public void TestIsBlank()
        {
            string s = null;
            Assert.IsTrue(s.IsBlank());

            s = string.Empty;
            Assert.IsTrue(s.IsBlank());

            s = "  ";
            Assert.IsTrue(s.IsBlank());

            s = "\t\t ";
            Assert.IsTrue(s.IsBlank());

            s = "hi";
            Assert.IsFalse(s.IsBlank());
        }

        [Test]
        public void TestIsPalindromic()
        {
            string s = string.Empty;
            Assert.That(s.IsPalindromic());

            s = "a";
            Assert.That(s.IsPalindromic());

            s = "aa";
            Assert.That(s.IsPalindromic());

            s = "ab";
            Assert.That(s.IsPalindromic(), Is.False);

            s = "abcba";
            Assert.That(s.IsPalindromic());

            s = "abcda";
            Assert.That(s.IsPalindromic(), Is.False);

            s = 1221.ToString();
            Assert.That(s.IsPalindromic());
        }
    }
}
