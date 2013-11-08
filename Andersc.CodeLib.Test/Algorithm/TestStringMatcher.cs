using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;
using NUnit.Framework;

using Andersc.CodeLib.Algorithm;

namespace Andersc.CodeLib.Tester.Algorithm
{
    [TestFixture]
    public class TestStringMatcher
    {
        [Test]
        public void TestBruteForce()
        {
            string t = "abcdefghijk";
            string p = "cde";
            Assert.That(StringMatcher.BruteForce(t, p), Is.EqualTo(2));

            p = "abc";
            Assert.That(StringMatcher.BruteForce(t, p), Is.EqualTo(0));

            p = "aabc";
            Assert.That(StringMatcher.BruteForce(t, p), Is.LessThan(0));

            p = string.Empty;
            Assert.That(StringMatcher.BruteForce(t, p), Is.EqualTo(0));

            t = "one";
            p = "bigger";
            Assert.That(StringMatcher.BruteForce(t, p), Is.LessThan(0));
        }

        [Test]
        public void TestBruteForce2()
        {
            string t = "abcdefghijk";
            string p = "cde";
            Assert.That(StringMatcher.BruteForce2(t, p), Is.EqualTo(2));

            p = "abc";
            Assert.That(StringMatcher.BruteForce2(t, p), Is.EqualTo(0));

            p = "aabc";
            Assert.That(StringMatcher.BruteForce2(t, p), Is.LessThan(0));

            p = string.Empty;
            Assert.That(StringMatcher.BruteForce2(t, p), Is.EqualTo(0));

            t = "one";
            p = "bigger";
            Assert.That(StringMatcher.BruteForce2(t, p), Is.LessThan(0));
        }

        [Test]
        public void TestBruteForceAll()
        {
            string t = "zabcdefghijkabc";
            string p = "cde";
            CollectionAssert.AreEqual(StringMatcher.BruteForceAll(t, p), new [] { 3 });

            p = "abc";
            CollectionAssert.AreEqual(StringMatcher.BruteForceAll(t, p), new[] { 1, 12 });

            p = "aabc";
            Assert.That(StringMatcher.BruteForceAll(t, p), Is.Empty);

            p = string.Empty;
            CollectionAssert.AreEqual(StringMatcher.BruteForceAll(t, p), new[] { 0 });

            t = "one";
            p = "bigger";
            Assert.That(StringMatcher.BruteForceAll(t, p), Is.Empty);
        }

        [Test]
        public void TestShiftTable()
        {
            string pat = "shift";
            StringMatcher.ShiftTable(pat).PrintToConsole();
        }

        [Test]
        public void TestHorspool()
        {
            string t = "abcdefghijk";
            string p = "cde";
            Assert.That(StringMatcher.Horspool(t, p), Is.EqualTo(2));

            p = "abc";
            Assert.That(StringMatcher.Horspool(t, p), Is.EqualTo(0));

            p = "aabc";
            Assert.That(StringMatcher.Horspool(t, p), Is.LessThan(0));

            p = string.Empty;
            Assert.That(StringMatcher.Horspool(t, p), Is.EqualTo(0));

            t = "one";
            p = "bigger";
            Assert.That(StringMatcher.Horspool(t, p), Is.LessThan(0));

            t = "hello, world!";
            p = "world!";
            Assert.That(StringMatcher.Horspool(t, p), Is.EqualTo(7));
        }

        [Test]
        public void TestGetNexts()
        {
            string pat = "ABCDABD";
            StringMatcher.GetNexts(pat).PrintToConsole();

            pat = "ababc";
            StringMatcher.GetNexts(pat).PrintToConsole();
        }

        [Test]
        public void TestKmp()
        {
            string t = "abcdefghijk";
            string p = "hij";
            Assert.That(StringMatcher.KMP(t, p), Is.EqualTo(7));

            p = "abc";
            Assert.That(StringMatcher.KMP(t, p), Is.EqualTo(0));

            p = "aabc";
            Assert.That(StringMatcher.KMP(t, p), Is.LessThan(0));

            p = string.Empty;
            Assert.That(StringMatcher.KMP(t, p), Is.EqualTo(0));

            t = "one";
            p = "bigger";
            Assert.That(StringMatcher.KMP(t, p), Is.LessThan(0));

            t = "hello, world!";
            p = "world!";
            Assert.That(StringMatcher.KMP(t, p), Is.EqualTo(7));
        }
    }
}
