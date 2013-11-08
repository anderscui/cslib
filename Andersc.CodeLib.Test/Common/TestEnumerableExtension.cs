using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Tester.Helpers;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestEnumerableExtension
    {
        [Test]
        public void TestAggregate()
        {
            int[] array = { 1, 3, 5, 7 };
            int initValue = 10;

            Assert.AreEqual(26, array.Aggregate(initValue, (a, b) => a + b));
        }

        [Test]
        public void TestAll()
        {
            int[] array = { 2, 4, 6, 8 };

            // Is all even?
            Assert.IsTrue(array.All(i => (i % 2 == 0)));

            // Is all greater than zero?
            Assert.IsFalse(array.All(i => i > 5));
        }

        [Test]
        public void TestAny()
        {
            int[] array = { 2, 4, 6, 8 };

            // Has elements? yes
            Assert.IsTrue(array.Any());

            // Has even number? yes
            Assert.IsTrue(array.Any(i => i % 2 == 0));

            // Has odd number? no
            Assert.IsFalse(array.Any(i => i % 2 == 1));
        }

        [Test]
        public void TestAverage()
        {
            int[] array = { 2, 4, 6, 8, 10 };
            Assert.AreEqual(6, array.Average());

            int?[] nullableArray = new int?[] { 2, null, 3, null, 10 };
            // It should be 5 not 3.
            Assert.AreEqual(5, nullableArray.Average());

            // Process before average action.
            Assert.AreEqual(3, array.Average(i => i / 2));
        }

        [Test]
        public void TestConcat()
        {
            int[] array1 = { 1, 3, 5, 7, 9 };
            int[] array2 = { 2, 4, 6, 8, 10 };

            Assert.AreEqual(array1.Length + array2.Length, array1.Concat(array2).Count());
        }

        [Test]
        public void TestDefaultIfEmpty()
        {
            int[] array = { 2, 4, 6, 8, 10 };
            IEnumerable<int> result = array.DefaultIfEmpty();

            Assert.AreEqual(array.Length, result.Count());
            Assert.AreEqual(array.First(), result.First());
            Assert.AreEqual(array.Last(), result.Last());

            int[] array2 = new int[10];
            IEnumerable<int> result2 = array2.DefaultIfEmpty();
            Assert.AreEqual(10, result2.Count());
            Assert.AreEqual(default(int), result2.First());
            Assert.AreEqual(default(int), result2.Last());
        }

        [Test]
        public void TestDistinctWithDefaultEqulityComparer()
        {
            int[] array = { 2, 4, 4, 2, 8 };
            IEnumerable<int> result = array.Distinct();
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(2, result.First());
            Assert.AreEqual(8, result.Last());
        }

        [Test]
        public void TestDistinctWithSpecifiedEqulityComparer()
        {
            var list = new List<Box>()
            {
                new Box(1, 2, 3),
                new Box(1, 2, 3),
                new Box(3, 4, 5)
            };

            IEnumerable<Box> result = list.Distinct(new BoxEqualityComparer());
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.First().Height);
            Assert.AreEqual(5, result.Last().Width);
        }

        [Test]
        public void TestElementAt()
        {
            int[] array = { 2, 3, 4, 5, 6 };
            Assert.AreEqual(3, array.ElementAt(1));
            Assert.AreEqual(5, array.ElementAt(3));
        }

        [Test]
        public void TestElementAtOrDefault()
        {
            int[] array = { 2, 3, 4, 5, 6 };
            Assert.AreEqual(3, array.ElementAtOrDefault(1));
            Assert.AreEqual(5, array.ElementAtOrDefault(3));
            Assert.AreEqual(default(int), array.ElementAtOrDefault(array.Length));
        }

        [Test]
        public void TestExcept()
        {
            int[] array1 = { 2, 3, 4, 5, 6 };
            int[] array2 = { 4, 5, 6, 7, 8 };
            IEnumerable<int> result = array1.Except(array2);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result.First());
            Assert.AreEqual(3, result.Last());
        }

        [Test]
        public void TestFirstOrDefault()
        {
            int[] array = { 2, 3, 4, 5, 6 };
            // first even number.
            int firstEven = array.FirstOrDefault(i => i % 2 == 0);
            Assert.AreEqual(2, firstEven);

            // first negative number.
            int firstNegative = array.FirstOrDefault(i => i < 0);
            Assert.AreEqual(default(int), firstNegative);
        }

        [Test]
        public void TestGroupBy()
        {
            int[] array = { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var result = array.GroupBy(i => i % 3);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(2, result.First().Key);
            Assert.AreEqual(3, result.First(g => g.Key == 0).Count());
        }

        [Test]
        public void TestIntersect()
        {
            int[] array1 = { 2, 3, 4, 5, 6 };
            int[] array2 = { 4, 5, 6, 7, 8 };
            var result = array1.Intersect(array2);

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(4, result.First());
            Assert.AreEqual(6, result.Last());
        }

        [Test]
        public void TestJoin()
        {
            int[] array1 = { 2, 3, 4, 5, 6 };
            int[] array2 = { 4, 5, 6, 7, 8 };
            var both = array1.Intersect(array2);
            var joined = array1.Join(array2, i => i % 2, j => j % 3, (i, j) => string.Format("({0}, {1})", i, j));

            foreach (var item in joined)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void TestOfType()
        {
            object[] array = { 1, "2", 3.0, true, 5 };
            var integers = array.OfType<int>();
            Assert.AreEqual(2, integers.Count());
        }

        [Test]
        public void TestRange()
        {
            var oneToTen = Enumerable.Range(1, 10);
            Assert.AreEqual(10, oneToTen.Count());
            Assert.AreEqual(1, oneToTen.First());
            Assert.AreEqual(10, oneToTen.Last());
        }

        [Test]
        public void TestToDictionary()
        {
            var oneToTen = Enumerable.Range(1, 10);

            // Doesn't work, duplicate keys.
            //Dictionary<int, int> result = oneToTen.ToDictionary(i => i % 5);
            Dictionary<int, int> result = oneToTen.GroupBy(i => i % 5).Select(g => g.Last()).ToDictionary(key => key);
            Assert.AreEqual(5, result.Count);

            foreach (int key in result.Keys)
            {
                Console.WriteLine("({0}, {1})", key, result[key]);
            }
        }

        [Test]
        public void TestImplicitLocalVar()
        {
            var collection = Enumerable.Range(0, 10)
                .Where(x => x % 2 != 0)
                .Reverse()
                .Select(x => new { Original = x, SquareRoot = Math.Sqrt(x) });

            foreach (var element in collection)
            {
                Console.WriteLine("sqrt of {0} is {1}", element.Original, element.SquareRoot);
            }
        }
    }
}
