using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.DataStructure
{
    [TestFixture]
    public class TestBinaryTree
    {
        private BinaryTree<int> GetTreeRangeOneToSeven()
        {
            BinaryTree<int> tree = new BinaryTree<int>(1);
            tree.Root.Left = new BinaryTreeNode<int>(2);
            tree.Root.Right = new BinaryTreeNode<int>(3);

            var left = tree.Root.Left;
            left.Left = new BinaryTreeNode<int>(4);
            left.Right = new BinaryTreeNode<int>(5);

            var right = tree.Root.Right;
            right.Left = new BinaryTreeNode<int>(6);
            right.Right = new BinaryTreeNode<int>(7);

            return tree;
        }

        [Test]
        public void TestRoot()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.That(tree.Root, Is.Null);

            tree = new BinaryTree<int>(1);
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Item, Is.EqualTo(1));
            Assert.That(tree.Root.IsLeaf, Is.True);

            tree.Root.Left = new BinaryTreeNode<int>(2, null, null);
            tree.Root.Right = new BinaryTreeNode<int>(3, null, null);
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Item, Is.EqualTo(1));
            Assert.That(tree.Root.IsLeaf, Is.False);
            Assert.That(tree.Size, Is.EqualTo(3));
        }

        [Test]
        public void TestIsEmpty()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.That(tree.IsEmpty, Is.True);

            tree = new BinaryTree<int>(1);
            Assert.That(tree.IsEmpty, Is.False);
        }

        [Test]
        public void TestSize()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.That(tree.Size, Is.EqualTo(0));

            tree = new BinaryTree<int>(1);
            Assert.That(tree.Size, Is.EqualTo(1));
            tree.Root.Left = new BinaryTreeNode<int>(2, null, null);
            tree.Root.Right = new BinaryTreeNode<int>(3, null, null);
            Assert.That(tree.Size, Is.EqualTo(3));
        }

        [Test]
        public void TestHeight()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.That(tree.Height, Is.EqualTo(-1));

            tree = new BinaryTree<int>(1);
            Assert.That(tree.Height, Is.EqualTo(0));
            tree.Root.Left = new BinaryTreeNode<int>(2, null, null);
            tree.Root.Right = new BinaryTreeNode<int>(3, null, null);
            Assert.That(tree.Height, Is.EqualTo(1));

            tree.Root.Left.Left = new BinaryTreeNode<int>(4);
            Assert.That(tree.Height, Is.EqualTo(2));
        }

        [Test]
        public void TestClear()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree = new BinaryTree<int>(1);
            tree.Root.Left = new BinaryTreeNode<int>(2);
            tree.Root.Left.Left = new BinaryTreeNode<int>(4);
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.IsEmpty, Is.False);
            Assert.That(tree.Size, Is.EqualTo(3));
            Assert.That(tree.Height, Is.EqualTo(2));

            tree.Clear();
            Assert.That(tree.Root, Is.Null);
            Assert.That(tree.IsEmpty, Is.True);
            Assert.That(tree.Size, Is.EqualTo(0));
            Assert.That(tree.Height, Is.EqualTo(-1));
        }

        [Test]
        public void TestPreOrderTraverse()
        {
            var tree = GetTreeRangeOneToSeven();
            tree.PreOrderTraverse(i => Console.Write(i + " "));
            // 1 2 4 5 3 6 7 
        }

        [Test]
        public void TestInOrderTraverse()
        {
            var tree = GetTreeRangeOneToSeven();
            tree.InOrderTraverse(i => Console.Write(i + " "));
            // 4 2 5 1 6 3 7 
        }

        [Test]
        public void TestPostOrderTraverse()
        {
            var tree = GetTreeRangeOneToSeven();
            tree.PostOrderTraverse(i => Console.Write(i + " "));
            // 4 5 2 6 7 3 1 
        }

        [Test]
        public void TestLevelOrderTraverse()
        {
            var tree = GetTreeRangeOneToSeven();
            tree.LevelOrderTraverse(i => Console.Write(i + " "));
            // 1 2 3 4 5 6 7
        }
    }
}
