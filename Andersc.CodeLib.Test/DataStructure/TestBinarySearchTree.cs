using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.DataStructure
{
    [TestFixture]
    public class TestBinarySearchTree
    {
        private int[] GetRandomArray()
        {
            return new int[] { 29, 37, 23, 10, 33, 44, 13, 49, 28, 7 };
        }

        private BinarySearchTree<int> GetRandomTree()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            foreach (var i in GetRandomArray())
            {
                bst.Insert(i);
            }

            return bst;
        }

        [Test]
        public void TestInsert()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            Assert.That(bst, Is.Empty);

            int[] array = GetRandomArray();
            foreach (var i in array)
            {
                bst.Insert(i);
            }
            Assert.That(bst.Size, Is.EqualTo(array.Length));

            bst.InOrderTraverse(i => Console.Write(i + ", "));
        }

        [Test]
        [ExpectedException(typeof(DuplicateItemException))]
        public void TestInsert_DuplicateItemException()
        {
            BinarySearchTree<int> bst = GetRandomTree();
            bst.Insert(GetRandomArray().First());
        }

        [Test]
        public void TestFindMin()
        {
            BinarySearchTree<int> bst = GetRandomTree();
            Assert.That(bst.Min, Is.EqualTo(GetRandomArray().Min()));
        }

        [Test]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void TestFindMin_EmptyException()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            int min = bst.Min;
        }

        [Test]
        public void TestFindMax()
        {
            BinarySearchTree<int> bst = GetRandomTree();
            Assert.That(bst.Max, Is.EqualTo(GetRandomArray().Max()));
        }

        [Test]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void TestFindMax_EmptyException()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            int max = bst.Max;
        }

        [Test]
        public void TestFind()
        {
            BinarySearchTree<int> bst = GetRandomTree();
            Assert.That(bst.Size, Is.EqualTo(GetRandomArray().Length));

            Assert.That(bst.Find(10), Is.Not.Null);
            bst.Remove(10);
            Assert.That(bst.Find(10), Is.Null);
        }

        [Test]
        public void TestRemoveMin()
        {
            var seq = from i in GetRandomArray()
                      orderby i
                      select i;
            BinarySearchTree<int> bst = GetRandomTree();
            Assert.That(bst.Min, Is.EqualTo(seq.First()));

            for (int i = 1; i < seq.Count(); i++)
            {
                bst.RemoveMin();
                Assert.That(bst.Min, Is.EqualTo(seq.Skip(i).First()));
            }            
        }

        [Test]
        [ExpectedException(typeof(ElementNotFoundException))]
        public void TestRemoveMin_ElementNotFoundException()
        {
            BinarySearchTree<int> emptyTree = new BinarySearchTree<int>();
            emptyTree.RemoveMin();
        }

        [Test]
        public void TestRemove()
        {
            var data = from i in GetRandomArray()
                      orderby i
                      select i;

            BinarySearchTree<int> bst = GetRandomTree();
            Assert.That(bst.Size, Is.EqualTo(data.Count()));

            int item = data.Skip(3).First();
            Assert.That(bst.Find(item), Is.Not.Null);
            bst.Remove(item);
            Assert.That(bst.Find(item), Is.Null);
            Assert.That(bst.Size, Is.EqualTo(data.Count() - 1));
        }

        [Test]
        [ExpectedException(typeof(ElementNotFoundException))]
        public void TestRemove_ElementNotFoundException()
        {
            var data = from i in GetRandomArray()
                       orderby i
                       select i;

            BinarySearchTree<int> bst = GetRandomTree();
            bst.Remove(data.Max() + 1);
        }
    }
}
