using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.DataStructure
{
    [TestFixture]
    public class TestPriorityQueue
    {
        [Test]
        public void TestIsEmpty()
        {
            PriorityQueue<int> pq = new PriorityQueue<int>();
            Assert.That(pq.IsEmpty, Is.True);
            Assert.That(pq.Count, Is.EqualTo(0));

            int[] initValues = new int[] { 8, 5, 10, 9 };
            pq = new PriorityQueue<int>(initValues);
            Assert.That(pq.IsEmpty, Is.False);
            Assert.That(pq.Count, Is.EqualTo(4));
            Assert.That(pq.Element, Is.EqualTo(initValues.Min()));
        }

        [Test]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void TestElement()
        {
            PriorityQueue<int> pq = new PriorityQueue<int>();
            Assert.That(pq.IsEmpty, Is.True);
            Console.WriteLine(pq.Element);
        }

        [Test]
        public void TestCapacity()
        {
            PriorityQueue<int> pq = new PriorityQueue<int>();
            Assert.That(pq.Capacity, Is.EqualTo(16));

            int[] initValues = new int[] { 8, 5, 10, 9 };
            pq = new PriorityQueue<int>(initValues);
            Assert.That(pq.Capacity, Is.EqualTo(5));
            pq.Add(11);
            Assert.That(pq.Capacity, Is.EqualTo(5));
            pq.Add(12);
            Assert.That(pq.Count, Is.EqualTo(6));
            Assert.That(pq.Capacity, Is.EqualTo(10));
        }

        [Test]
        public void TestRemove()
        {
            int[] initValues = new int[] { 8, 5, 10, 9 };
            PriorityQueue<int> pq = new PriorityQueue<int>(initValues);
            Assert.That(pq.Remove(), Is.EqualTo(initValues.Min()));
            Assert.That(pq.Count, Is.EqualTo(initValues.Length - 1));
            Assert.That(pq.Element, Is.EqualTo(initValues.OrderBy(i => i).Skip(1).First()));
        }

        [Test]
        public void TestClear()
        {
            int[] initValues = new int[] { 8, 5, 10, 9 };
            PriorityQueue<int> pq = new PriorityQueue<int>(initValues);
            Assert.That(pq.Count, Is.EqualTo(initValues.Length));
            pq.Clear();
            Assert.That(pq.IsEmpty, Is.True);
        }
    }
}
