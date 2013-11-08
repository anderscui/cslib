using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.DataStructure
{
    [TestFixture]
    public class TestDynamicArray
    {
        [Test]
        public void TestBuiltInArray()
        {
            int[] a = { 3, 6, 8, 10 };
            Assert.That(a.GetLowerBound(0), Is.EqualTo(0));

            int[] lowerBounds = { 2005, 1 };
            int[] lengths = { 5, 4 };
            double[,] da = Array.CreateInstance(typeof(double), lengths, lowerBounds) as double[,];
            Assert.That(da.GetLowerBound(1), Is.EqualTo(lowerBounds[1]));
            Assert.That(da.GetUpperBound(0), Is.EqualTo(lowerBounds[0] + lengths[0] - 1));
        }

        [Test]
        public void TestCopyFrom()
        {
            DynamicArray<int> array = new int[] { 1, 2, 3, 4, 5 };
            DynamicArray<int> array2 = new DynamicArray<int>();
            array2.CopyFrom(array);

            Assert.That(object.ReferenceEquals(array.Values, array2.Values), Is.False);
            CollectionAssert.AreEquivalent(array.Values, array2.Values);
        }

        [Test]
        public void TestRank()
        {
            DynamicArray<int> array = new DynamicArray<int>(10);
            Assert.That(array.Rank, Is.EqualTo(1));
        }

        [Test]
        public void TestLowerBound()
        {
            DynamicArray<int> array = new DynamicArray<int>(10, 5);
            Assert.That(array.LowerBound, Is.EqualTo(5));
        }

        [Test]
        public void TestLengthGetter()
        {
            DynamicArray<int> array = new DynamicArray<int>(10);
            Assert.That(array.Length, Is.EqualTo(10));
        }

        [Test]
        public void TestLengthSetter()
        {
            DynamicArray<int> array = new DynamicArray<int>(10);
            Assert.That(array.Length, Is.EqualTo(10));
            array[0] = 10;

            int firstElem = array[0];
            array.Length = 20;
            Assert.That(array.Length, Is.EqualTo(20));
            Assert.That(array[0], Is.EqualTo(firstElem));
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestLengthSetterException()
        {
            DynamicArray<int> array = new DynamicArray<int>(10);
            Assert.That(array.Length, Is.EqualTo(10));
            array.Length = 5;

            int elem = array[array.Length];
        }

        [Test]
        public void TestImplicitConversionFromIntArray()
        {
            DynamicArray<int> array = new int[] { };
            Assert.That(array.Length, Is.EqualTo(0));

            array = new int[] { 1, 2, 3, 4, 5 };
            Assert.That(array.Length, Is.EqualTo(5));
            Assert.That(array[0], Is.EqualTo(1));
            Assert.That(array[array.Length - 1], Is.EqualTo(5));
        }

        [Test]
        public void TestImplicitConversionToIntArray()
        {
            DynamicArray<int> array = new int[] { 1, 2, 3, 4, 5 };
            Assert.That(array.Length, Is.EqualTo(5));

            int[] intArray = array;
            Assert.That(intArray[intArray.Length - 1], Is.EqualTo(5));
        }
    }
}
