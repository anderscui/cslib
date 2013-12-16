using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestRandomGenerator
    {
        [Test]
        public void TestGetRandomInt32()
        {
            List<int> list = new List<int>();
            Enumerable.Range(0, 10).ToList().ForEach(i => list.Add(RandomGenerator.GetRandomInt32(0, 100)));

            Assert.That(list.Distinct().Count(), Is.GreaterThanOrEqualTo(list.Count / 2));
            Console.WriteLine(list.Distinct().Count());
            list.ForEach(i => Console.WriteLine(i));
        }

        [Test]
        public void TestGetRandomInt32Array()
        {
            int[] array = RandomGenerator.GetRandomInt32Array(10, 0, 100);
            Assert.That(array.Distinct().Count(), Is.GreaterThanOrEqualTo(array.Length / 2));
            Console.WriteLine(array.Distinct().Count());
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetUniqueRandomInt32ArrayException()
        {
            RandomGenerator.GetUniqueRandomInt32Array(5, 1, 5);
        }

        [Test]
        public void TestGetUniqueRandomInt32Array()
        {
            int arrayLength = 5;
            int[] array = RandomGenerator.GetUniqueRandomInt32Array(arrayLength, 1, 10);
            array.PrintToConsole();
            Assert.That(array.Distinct().Count(), Is.EqualTo(arrayLength));

            arrayLength = 10;
            array = RandomGenerator.GetUniqueRandomInt32Array(arrayLength, 10, 20);
            Assert.That(array.Distinct().Count(), Is.EqualTo(arrayLength));
            array.PrintToConsole();
        }

        [Test]
        public void TestGetRandomChar()
        {
            var list = new List<char>();
            Enumerable.Range(0, 30).ToList().ForEach(i => list.Add(RandomGenerator.GetRandomChar()));

            Assert.That(list.Distinct().Count(), Is.GreaterThanOrEqualTo(list.Count / 2));
            Console.WriteLine(list.Distinct().Count());
            list.ForEach(Console.WriteLine);
        }
    }
}
