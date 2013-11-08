/*
 * Created by: Anders Cui
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestGenericSet
    {
        [Test]
        public void TestIsEmpty()
        {
            GenericSet<int> gs = new GenericSet<int>();
            Assert.That(gs.IsEmpty, Is.True);

            gs.Add(1);
            Assert.That(gs.IsEmpty, Is.False);
        }

        [Test]
        public void TestSize()
        {
            GenericSet<int> gs = new GenericSet<int>();
            Assert.That(gs.Size, Is.EqualTo(0));

            gs.Add(1);
            Assert.That(gs.Size, Is.EqualTo(1));
            gs.Add(2);
            Assert.That(gs.Size, Is.EqualTo(2));
        }

        [Test]
        public void TestContains()
        {
            GenericSet<int> gs = new GenericSet<int>();
            for (int i = 0; i < 10; i++)
            {
                gs.Add(i + 1);
            }
            Assert.That(gs.Contains(5), Is.True);
            Assert.That(gs.Contains(100), Is.False);
        }

        [Test]
        public void TestUnion()
        {
            GenericSet<int> gs1 = new GenericSet<int>();
            for (int i = 1; i <= 8; i++)
            {
                gs1.Add(i);
            }
            Console.WriteLine(gs1);

            GenericSet<int> gs2 = new GenericSet<int>();
            for (int i = 5; i <= 15; i++)
            {
                gs2.Add(i);
            }
            Console.WriteLine(gs2);

            GenericSet<int> union = gs1.Union(gs2);
            Assert.That(union.Size, Is.EqualTo(15));
            Assert.That(union.Contains(15), Is.True);
            Assert.That(union.Contains(1), Is.True);
        }

        [Test]
        public void TestUnion_EmptySet()
        {
            GenericSet<int> gs1 = new GenericSet<int>();
            for (int i = 1; i <= 8; i++)
            {
                gs1.Add(i);
            }
            Console.WriteLine(gs1);

            GenericSet<int> gs2 = new GenericSet<int>();
            Assert.That(gs2.IsEmpty, Is.True);
            Console.WriteLine(gs2);

            GenericSet<int> union = gs1.Union(gs2);
            Assert.That(union.Size, Is.EqualTo(gs1.Size));
            Assert.That(union.Contains(15), Is.False);
            Assert.That(union.Contains(1), Is.True);
        }

        [Test]
        public void TestIntersection()
        {
            GenericSet<int> gs1 = new GenericSet<int>();
            for (int i = 1; i <= 8; i++)
            {
                gs1.Add(i);
            }
            Console.WriteLine(gs1);

            GenericSet<int> gs2 = new GenericSet<int>();
            for (int i = 5; i <= 15; i++)
            {
                gs2.Add(i);
            }
            Console.WriteLine(gs2);

            GenericSet<int> union = gs1.Intersection(gs2);
            Assert.That(union.Size, Is.EqualTo(4));
            Assert.That(union.Contains(15), Is.False);
            Assert.That(union.Contains(1), Is.False);
            Assert.That(union.Contains(5), Is.True);
            Assert.That(union.Contains(8), Is.True);
            Console.WriteLine("Intersection: {0}", union);
        }

        [Test]
        public void TestDifference()
        {
            GenericSet<int> gs1 = new GenericSet<int>();
            for (int i = 1; i <= 8; i++)
            {
                gs1.Add(i);
            }
            Console.WriteLine(gs1);

            GenericSet<int> gs2 = new GenericSet<int>();
            for (int i = 5; i <= 15; i++)
            {
                gs2.Add(i);
            }
            Console.WriteLine(gs2);

            GenericSet<int> difference = gs1.Difference(gs2);
            Assert.That(difference.Size, Is.EqualTo(4));
            Assert.That(difference.Contains(15), Is.False);
            Assert.That(difference.Contains(5), Is.False);
            Assert.That(difference.Contains(1), Is.True);
            Assert.That(difference.Contains(4), Is.True);
            Console.WriteLine("Difference: {0}", difference);
        }

        [Test]
        public void TestEquals()
        {
            GenericSet<int> gs1 = new GenericSet<int>();
            GenericSet<int> gs2 = new GenericSet<int>();
            Assert.That(gs1.Equals(gs2), Is.True);

            for (int i = 1; i <= 5; i++)
            {
                gs1.Add(i);
            }
            Console.WriteLine("set1: {0}", gs1);

            for (int i = 5; i >= 1; i--)
            {
                gs2.Add(i);
            }
            Console.WriteLine("set2: {0}", gs2);
            Assert.That(gs1.Equals(gs2), Is.True);
            gs2.Add(10);
            Assert.That(gs1.Equals(gs2), Is.False);
        }
    }
}
