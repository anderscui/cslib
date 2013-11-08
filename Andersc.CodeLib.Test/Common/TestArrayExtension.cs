/*
 * Created by: Anders Cui
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestArrayExtension
    {
        [Test]
        public void TestHasElements()
        {
            int[] array = null;
            Assert.That(array.HasElements(), Is.False);
            array = new int[] { };
            Assert.That(array.HasElements(), Is.False);
            array = new int[] { 3};
            Assert.That(array.HasElements(), Is.True);
            array = new int[] { 1, 3, 5, 7 };
            Assert.That(array.HasElements(), Is.True);
        }

        [Test]
        public void TestSwap()
        {
            int[] array = new int[] { 1, 3, 5, 7 };
            array.Swap(1, 2);
            Assert.That(array[1], Is.EqualTo(5));
            Assert.That(array[2], Is.EqualTo(3));
        }

        [Test]
        public void TestIndexOf()
        {
            int[] array = new int[] { 3, 7, 9, 12, 8 };
            Assert.That(array.IndexOf(1), Is.LessThan(0));
            Assert.That(array.IndexOf(9), Is.EqualTo(2));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfArgumentNullException()
        {
            int[] array = null;
            Assert.That(array.IndexOf(1), Is.LessThan(0));
        }

        [Test]
        public void TestContains()
        {
            int[] array = new int[] { 3, 7, 9, 12, 8 };
            Assert.That(array.Contains(1), Is.False);
            Assert.That(array.Contains(9), Is.True);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsArgumentNullException()
        {
            int[] array = null;
            array.Contains(1);
        }

        [Test]
        public void TestMin()
        {
            int[] array = new int[] { 3, 7, 9, 12, 8 };
            Assert.That(array.Min(), Is.EqualTo(3));

            array = new int[] { 1 };
            Assert.That(array.Min(), Is.EqualTo(1));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinArgumentNullException()
        {
            int[] array = null;
            array.Min();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMinArgumentException()
        {
            int[] array = new int[] { };
            array.Min();
        }

        [Test]
        public void TestMax()
        {
            int[] array = new int[] { 3, 7, 9, 12, 8 };
            Assert.That(array.Max(), Is.EqualTo(12));

            array = new int[] { 1 };
            Assert.That(array.Max(), Is.EqualTo(1));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMaxArgumentNullException()
        {
            int[] array = null;
            array.Max();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMaxArgumentException()
        {
            int[] array = new int[] { };
            array.Max();
        }

        [Test]
        public void TestBinarySearch()
        {
            int[] array = { 2, 5, 7, 10, 29 };
            int index = array.BinarySearch(2);
            Assert.That(index, Is.EqualTo(0));

            index = array.BinarySearch(20);
            Assert.That(index, Is.EqualTo(-1));

            index = array.BinarySearch(7);
            Assert.That(index, Is.EqualTo(2));

            index = array.BinarySearch(29);
            Assert.That(index, Is.EqualTo(4));
        }
    }
}
