using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.DataStructure
{
    [TestFixture]
    public class TestAvlTree
    {
        private AvlTree<int> GetEmptyTree()
        {
            return new AvlTree<int>();
        }

        private AvlTree<int> GetFilledTree()
        {
            AvlTree<int> at = GetEmptyTree();
            at.Insert(12);
            at.Insert(8);
            at.Insert(10);
            at.Insert(6);
            at.Insert(2);
            at.Insert(7);

            return at;
        }

        [Test]
        public void TestIsEmpty()
        {
            AvlTree<int> at = GetEmptyTree();
            Assert.That(at, Is.Empty);

            at.Insert(8);
            Assert.That(at, Is.Not.Empty);
            Assert.That(at.Size, Is.EqualTo(1));
        }

        [Test]
        public void TestSize()
        {
            AvlTree<int> at = GetEmptyTree();
            Assert.That(at.Size, Is.EqualTo(0));
            at.Insert(8);
            Assert.That(at.Size, Is.EqualTo(1));
            at.Insert(10);
            Assert.That(at.Size, Is.EqualTo(2));
            at.Insert(15);
            Assert.That(at.Size, Is.EqualTo(3));
        }

        [Test]
        public void TestInsert_LeftLeft()
        {
            AvlTree<int> at = GetEmptyTree();
            Assert.That(at, Is.Empty);
            at.Insert(12);
            Assert.That(at.Root.Item, Is.EqualTo(12));

            at.Insert(8);
            AvlNode<int> firstLeft = at.Root.Left;
            Assert.That(firstLeft.Item, Is.EqualTo(8));
            Assert.That(at.Height, Is.EqualTo(1));

            at.Insert(4);
            Assert.That(at.Root.Item, Is.EqualTo(8));
            Assert.That(at.Root.Left.Item, Is.EqualTo(4));
            Assert.That(at.Root.Right.Item, Is.EqualTo(12));
            Assert.That(at.Height, Is.EqualTo(1));

            at.Insert(6);
            at.Insert(2);
            Assert.That(at.Height, Is.EqualTo(2));

            at.Insert(1);
            Assert.That(at.Height, Is.EqualTo(2));
            Assert.That(at.Root.Item, Is.EqualTo(4));
            Assert.That(at.Root.Left.Item, Is.EqualTo(2));
            Assert.That(at.Root.Right.Item, Is.EqualTo(8));
        }

        [Test]
        public void TestInsert_LeftRight()
        {
            AvlTree<int> at = GetEmptyTree();
            Assert.That(at, Is.Empty);
            at.Insert(12);
            Assert.That(at.Root.Item, Is.EqualTo(12));

            at.Insert(8);
            AvlNode<int> firstLeft = at.Root.Left;
            Assert.That(firstLeft.Item, Is.EqualTo(8));
            Assert.That(at.Height, Is.EqualTo(1));

            at.Insert(10);
            Assert.That(at.Root.Item, Is.EqualTo(10));
            Assert.That(at.Root.Left.Item, Is.EqualTo(8));
            Assert.That(at.Root.Right.Item, Is.EqualTo(12));
            Assert.That(at.Height, Is.EqualTo(1));

            at.Insert(6);
            at.Insert(2);
            Assert.That(at.Height, Is.EqualTo(2));
            Assert.That(at.Root.Left.Item, Is.EqualTo(6));

            at.Insert(7);
            Assert.That(at.Height, Is.EqualTo(2));
            Assert.That(at.Root.Item, Is.EqualTo(8));
            Assert.That(at.Root.Left.Item, Is.EqualTo(6));
            Assert.That(at.Root.Right.Item, Is.EqualTo(10));
        }

        [Test]
        public void TestInOrderTraverse()
        {
            AvlTree<int> at = GetFilledTree();
            at.InOrderTraverse(i => Console.Write(i + " ")); // 2 6 7 8 10 12 
        }

        [Test]
        public void TestLevelOrderTraverse()
        {
            AvlTree<int> at = GetFilledTree();
            at.LevelOrderTraverse(i => Console.Write(i + " ")); // 8 6 10 2 7 12 
        }
    }
}
