using System;
using Generic = System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestQueue
    {
        private Queue<int> GetQueue(int capacity)
        {
            return new Queue<int>(capacity);
        }

        private Queue<int> GetEmptyQueue()
        {
            return new Queue<int>(10);
        }

        private Queue<int> GetQueueRangeFromOneToTen()
        {
            int count = 10;
            Queue<int> queue = new Queue<int>(count);
            for (int i = 1; i <= count; i++)
            {
                queue.Enqueue(i);
            }

            return queue;
        }

        [Test]
        public void TestIsEmpty()
        {
            Queue<int> queue = GetEmptyQueue();
            Assert.That(queue, Is.Empty);

            queue.Enqueue(1);
            Assert.That(queue, Is.Not.Empty);
        }

        [Test]
        public void TestCount()
        {
            Queue<int> queue = GetEmptyQueue();
            Assert.That(queue.Count, Is.EqualTo(0));

            queue.Enqueue(0);
            Assert.That(queue.Count, Is.EqualTo(1));

            for (int i = 1; i < 10; i++)
            {
                queue.Enqueue(i);
            }
            Assert.That(queue.Count, Is.EqualTo(10));
            Assert.That(queue.Front, Is.EqualTo(0));
        }

        [Test]
        public void TestClear()
        {
            Queue<int> queue = GetQueueRangeFromOneToTen();
            Assert.That(queue.Count, Is.EqualTo(10));

            queue.Clear();
            Assert.That(queue.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestEnqueue()
        {
            Queue<int> queue = GetEmptyQueue();
            Assert.That(queue, Is.Empty);

            queue.Enqueue(1);
            Assert.That(queue.Count, Is.EqualTo(1));
            Assert.That(queue.Front, Is.EqualTo(1));
        }

        [Test]
        public void TestEnqueue_Wraparound()
        {
            int capacity = 5;
            Queue<int> queue = GetQueue(capacity);
            Assert.That(queue, Is.Empty);
            for (int i = 0; i < capacity; i++)
            {
                queue.Enqueue(i);
            }
            Assert.That(queue.Count, Is.EqualTo(capacity));
            Assert.That(queue.Dequeue(), Is.EqualTo(0));
            Assert.That(queue.Dequeue(), Is.EqualTo(1));
            Assert.That(queue.Count, Is.EqualTo(capacity - 2));
            
            queue.Enqueue(0);
            Assert.That(queue.Count, Is.EqualTo(capacity - 1));
            Assert.That(queue.Front, Is.EqualTo(2));
        }

        [Test]
        public void TestDequeue()
        {
            Queue<int> queue = GetQueueRangeFromOneToTen();
            Assert.That(queue.Count, Is.EqualTo(10));

            queue.Dequeue();
            Assert.That(queue.Count, Is.EqualTo(9));
            Assert.That(queue.Front, Is.EqualTo(2));

            for (int i = 0; i < 9; i++)
            {
                queue.Dequeue();
            }
            Assert.That(queue, Is.Empty);
        }

        [Test]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void TestDequeue_EmptyQueue()
        {
            Queue<int> queue = GetQueueRangeFromOneToTen();
            Assert.That(queue.Count, Is.EqualTo(10));

            for (int i = 0; i < 20; i++)
            {
                queue.Dequeue();
            }
        }

        [Test]
        public void TestFront()
        {
            Queue<int> queue = GetQueueRangeFromOneToTen();
            Assert.That(queue.Front, Is.EqualTo(1));
        }

        [Test]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void TestFront_EmptyQueue()
        {
            Queue<int> queue = GetEmptyQueue();
            int front = queue.Front;
        }

        [Test]
        public void TestGetEnumerator()
        {
            int[] array = new int[10];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }

            Queue<int> queue = GetQueueRangeFromOneToTen();
            CollectionAssert.AreEqual(array, queue.ToArray());
        }
    }
}
