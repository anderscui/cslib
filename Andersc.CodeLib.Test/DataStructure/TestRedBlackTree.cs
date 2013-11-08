using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.DataStructure
{
    [TestFixture]
    public class TestRedBlackTree
    {
        [Test]
        public void TestAdd()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.Add(50);
            tree.Add(25);
            tree.Add(68);
            tree.Add(10);
            tree.Add(76);
            tree.Add(8);
            tree.Add(82);
            tree.Add(20);

            tree.Print();
        }

        public void TestRemove()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.Add(50);
            tree.Add(25);
            tree.Add(68);
            tree.Add(10);
            tree.Add(76);
            tree.Add(8);
            tree.Add(82);
            tree.Add(20);
            tree.Print();

            Assert.IsFalse(tree.Remove(100));
            Assert.IsTrue(tree.Remove(25));
            Assert.IsTrue(tree.Remove(82));
            Assert.IsTrue(tree.Remove(10));
            tree.Print();
        }
    }
}
