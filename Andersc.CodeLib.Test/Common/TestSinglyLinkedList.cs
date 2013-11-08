using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestSinglyLinkedList
    {
        private SinglyLinkedList<int> GetListRangeOneToTen()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            for (int i = 1; i <= 10; i++)
            {
                list.Append(i);
            }

            return list;
        }

        private SinglyLinkedList<int> GetEmptyList()
        {
            return new SinglyLinkedList<int>();
        }

        [Test]
        public void TestIsEmpty()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            Assert.IsTrue(list.IsEmpty);

            list.Append(1);
            Assert.IsFalse(list.IsEmpty);

            list.Clear();
            Assert.IsTrue(list.IsEmpty);
        }

        [Test]
        public void TestHead()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            Assert.That(list.Head, Is.Null);

            list.Append(1);
            Assert.That(list.Head, Is.Not.Null);
        }

        [Test]
        public void TestTail()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            Assert.That(list.Tail, Is.Null);

            list.Append(1);
            Assert.That(list.Tail, Is.Not.Null);
        }

        [Test]
        public void TestCount()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            Assert.That(list.Count, Is.EqualTo(0));

            list.Append(1);
            Assert.That(list.Count, Is.EqualTo(1));

            list.Prepend(2);
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void TestFirst_EmptyException()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            int val = list.First;
        }

        [Test]
        public void TestFirst()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            list.Prepend(1);
            list.Append(2);
            list.Prepend(3);
            Assert.That(list.First, Is.EqualTo(3));
        }

        [Test]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void TestLast_EmptyException()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            int val = list.Last;
        }

        [Test]
        public void TestLast()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            list.Prepend(1);
            list.Append(2);
            list.Prepend(3);
            Assert.That(list.Last, Is.EqualTo(2));
        }

        [Test]
        public void TestPrepend()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            Assert.That(list.Count, Is.EqualTo(0));
            list.Prepend(1);
            Assert.That(list.Count, Is.EqualTo(1));
            list.Prepend(2);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.First, Is.EqualTo(2));
        }

        [Test]
        public void TestAppend()
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            Assert.That(list.Count, Is.EqualTo(0));
            list.Append(1);
            Assert.That(list.Count, Is.EqualTo(1));
            list.Append(2);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.Last, Is.EqualTo(2));
        }

        [Test]
        public void TestRemove()
        {
            var list = GetListRangeOneToTen();
            Assert.That(list.Count, Is.EqualTo(10));

            Assert.That(list.Find(10), Is.EqualTo(list.Tail));
            list.Remove(10);
            Assert.That(list.Find(10), Is.Null);
            Assert.That(list.Count, Is.EqualTo(9));
        }

        [Test]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void TestRemove_FromEmptyList()
        {
            var list = GetEmptyList();
            list.Remove(1);
        }

        [Test]
        [ExpectedException(typeof(ElementNotFoundException))]
        public void TestRemove_NotExistingElement()
        {
            var list = GetListRangeOneToTen();
            list.Remove(100);
        }

        [Test]
        public void TestClear()
        {
            var list = GetListRangeOneToTen();
            Assert.That(list, Is.Not.Empty);

            list.Clear();
            Assert.That(list, Is.Empty);
        }

        [Test]
        public void TestFind()
        {
            var list = GetListRangeOneToTen();
            Assert.That(list.Count, Is.EqualTo(10));

            Assert.That(list.Find(5).Next.Data, Is.EqualTo(6));
            Assert.That(list.Find(11), Is.Null);
        }

        [Test]
        public void TestCopyFrom()
        {
            var list = GetEmptyList();
            Assert.That(list, Is.Empty);
            list.CopyFrom(GetListRangeOneToTen());
            Assert.That(list, Is.Not.Empty);
            Assert.That(list.Count, Is.EqualTo(10));
        }

        [Test]
        public void TestIEnumerableImpl()
        {
            var list = GetListRangeOneToTen();

            int[] array = new int[10];
            for (int i = 0; i < 10; i++)
            {
                array[i] = i + 1;
            }

            CollectionAssert.AreEqual(array, list.ToArray());
        }
    }
}
