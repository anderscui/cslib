using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.DataStructure
{
    [TestFixture]
    public class TestATree
    {
        [Test]
        public void TestAdd()
        {
            ATree<int> tree = new ATree<int>();
            tree.Add(50);
            tree.Add(25);
            tree.Add(39);
            tree.Add(15);
            tree.Add(10);
            tree.Add(70);
            tree.Add(60);

            tree.Print();

            //Assert.AreEqual(15, tree.Head.Left.Item);
            //Assert.AreEqual(25, tree.Head.Left.Right.Item);

            //Console.WriteLine(tree.Head.BalanceFactor);
        }

        [Test]
        public void TestRemove()
        {
            ATree<int> tree = new ATree<int>();
            tree.Add(50);
            tree.Add(25);
            tree.Add(39);
            tree.Add(15);
            tree.Add(10);
            tree.Add(70);
            tree.Add(60);
            tree.Add(8);

            tree.Print();

            tree.Remove(70);
            tree.Print();

            tree.Remove(15);
            tree.Print();
        }
    }
}
