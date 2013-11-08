using System;
using System.Linq;
using System.Text;
using FclGeneric = System.Collections.Generic;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.DataStructure
{
    [TestFixture]
    public class TestHashSet
    {
        private HashSet<int> GetEmptyIntSet()
        {
            return (new HashSet<int>());
        }

        private HashSet<int> GetIntSetFromOneToFive()
        {
            HashSet<int> set = new HashSet<int>();
            for (int i = 1; i <= 5; i++)
            {
                set.Add(i);
            }

            return set;
        }

        [Test]
        public void TestCount()
        {
            HashSet<char> charSet = new HashSet<char>();
            Assert.That(charSet.Count, Is.EqualTo(0));

            charSet.Add('a');
            Assert.That(charSet.Count, Is.EqualTo(1));

            charSet.Add('b');
            Assert.That(charSet.Count, Is.EqualTo(2));

            charSet.Add('a');
            Assert.That(charSet.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestAdd_Without_Collision()
        {
            HashSet<char> charSet = new HashSet<char>();
            Assert.That(charSet.IsEmpty, Is.True);

            Assert.IsTrue(charSet.Add('a'));
            Assert.That(charSet.Count, Is.EqualTo(1));

            Assert.IsFalse(charSet.Add('a'));
            Assert.That(charSet.Count, Is.EqualTo(1));

            Assert.IsTrue(charSet.Add('b'));
            Assert.That(charSet.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestAdd_With_Collision()
        {
            HashSet<int> set = new HashSet<int>();
            Assert.That(set.IsEmpty, Is.True);

            set.Add(1);
            set.Add(2);

            set.Add(102);
            set.Add(203);
        }

        [Test]
        public void TestRemove()
        {
            HashSet<int> set = new HashSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(102);
            set.Add(203);
            Assert.That(set.Count, Is.EqualTo(4));

            Assert.IsFalse(set.Remove(111));
            Assert.That(set.Count, Is.EqualTo(4));

            Assert.IsTrue(set.Remove(102));
            Assert.That(set.Count, Is.EqualTo(3));

            Assert.IsTrue(set.Remove(203));
            Assert.That(set.Count, Is.EqualTo(2));

            Assert.IsTrue(set.Remove(2));
            Assert.That(set.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestContains()
        {
            HashSet<int> set = new HashSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(102);
            set.Add(203);
            Assert.IsTrue(set.Contains(2));
            Assert.IsFalse(set.Contains(200));
        }

        [Test]
        public void TestGetEnumerator()
        {
            HashSet<int> set = new HashSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(102);
            set.Add(203);
            set.Add(103);

            CollectionAssert.AreEqual(set, new int[] { 1, 2, 103, 102, 203 });
        }

        #region Test ICollection<T> Impl

        [Test]
        public void TestCopyTo()
        {
            HashSet<int> set = new HashSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(102);
            set.Add(203);
            set.Add(103);

            int[] copyTo = new int[set.Count];
            set.CopyTo(copyTo, 0);
            CollectionAssert.AreEqual(set.ToArray(), new int[] { 1, 2, 103, 102, 203 });
        }

        [Test]
        public void TestIsReadOnly()
        {
            HashSet<char> set = new HashSet<char>();
            Assert.That(set.IsReadOnly, Is.False);
            set.Add('a');
            set.Add('b');
            Assert.That(set.IsReadOnly, Is.False);
        }

        #endregion

        #region Test ISet Impl

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptWith_Null()
        {
            (new HashSet<char>()).ExceptWith(null);
        }

        [Test]
        public void TestExceptWith()
        {
            var set = GetEmptyIntSet();
            Assert.IsTrue(set.IsEmpty);

            var other = new int[] { 1, 2, 4 };
            set.ExceptWith(other);
            Assert.IsTrue(set.IsEmpty);

            set.Add(2);
            set.Add(3);
            set.Add(4);
            set.ExceptWith(other);
            Assert.That(set.Count, Is.EqualTo(1));
            Assert.IsTrue(set.Contains(3));

            set.ExceptWith(set);
            Assert.IsTrue(set.IsEmpty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectWith_Null()
        {
            (new HashSet<char>()).IntersectWith(null);
        }

        [Test]
        public void TestIntersectWith()
        {
            HashSet<int> set = GetIntSetFromOneToFive();

            set.IntersectWith(new int[] { 1, 3, 5 });
            Assert.That(set.Count, Is.EqualTo(3));
            Assert.That(set.Contains(1), Is.True);
            Assert.That(set.Contains(2), Is.False);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsProperSubsetOf_Null()
        {
            (new HashSet<char>()).IsProperSubsetOf(null);
        }

        [Test]
        public void TestIsProperSubsetOf()
        {
            HashSet<int> set = GetEmptyIntSet();
            Assert.IsTrue(set.IsProperSubsetOf(new int[] { 1, 2, 3 }));
            Assert.IsFalse(set.IsProperSubsetOf(new FclGeneric.List<int>()));

            set.Add(1);
            set.Add(2);
            set.Add(3);
            Assert.IsTrue(set.IsProperSubsetOf(GetIntSetFromOneToFive()));
            Assert.IsFalse(set.IsProperSubsetOf(new int[] { 1, 2, 3 }));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsProperSupersetOf_Null()
        {
            (new HashSet<char>()).IsProperSupersetOf(null);
        }

        [Test]
        public void TestIsProperSupersetOf()
        {
            HashSet<int> set = GetEmptyIntSet();
            Assert.IsFalse(set.IsProperSupersetOf(new int[] { 1 }));
            Assert.IsFalse(set.IsProperSupersetOf(new FclGeneric.List<int>()));

            set.Add(1);
            set.Add(2);
            set.Add(3);
            var set2 = GetIntSetFromOneToFive();
            Assert.IsTrue(set2.IsProperSupersetOf(set));
            Assert.IsFalse(set.IsProperSupersetOf(new int[] { 1, 2, 3 }));
            Assert.IsTrue(set.IsProperSupersetOf(new FclGeneric.List<int>()));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubsetOf_Null()
        {
            (new HashSet<char>()).IsSubsetOf(null);
        }

        [Test]
        public void TestIsSubsetOf()
        {
            HashSet<int> set = GetEmptyIntSet();
            Assert.IsTrue(set.IsSubsetOf(new int[] { 1, 2, 3 }));
            Assert.IsTrue(set.IsSubsetOf(new FclGeneric.List<int>()));

            set.Add(1);
            set.Add(2);
            set.Add(3);
            Assert.IsTrue(set.IsSubsetOf(GetIntSetFromOneToFive()));
            Assert.IsTrue(set.IsSubsetOf(new int[] { 1, 2, 3 }));
            Assert.IsFalse(set.IsSubsetOf(new int[] { 1, 2 }));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSupersetOf_Null()
        {
            (new HashSet<char>()).IsSupersetOf(null);
        }

        [Test]
        public void TestIsSupersetOf()
        {
            HashSet<int> set = GetEmptyIntSet();
            Assert.IsFalse(set.IsSupersetOf(new int[] { 1 }));
            Assert.IsTrue(set.IsSupersetOf(new FclGeneric.List<int>()));

            set.Add(1);
            set.Add(2);
            set.Add(3);
            var set2 = GetIntSetFromOneToFive();
            Assert.IsTrue(set2.IsSupersetOf(set));
            Assert.IsTrue(set.IsSupersetOf(new int[] { 1, 2, 3 }));
            Assert.IsTrue(set.IsSupersetOf(new FclGeneric.List<int>()));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverlaps_Null()
        {
            (new HashSet<char>()).Overlaps(null);
        }

        [Test]
        public void TestOverlaps()
        {
            HashSet<int> set = GetEmptyIntSet();
            Assert.IsFalse(set.Overlaps(new int[] { 1 }));
            Assert.IsFalse(set.Overlaps(new FclGeneric.List<int>()));

            set.Add(1);
            set.Add(2);
            var set2 = GetIntSetFromOneToFive();
            Assert.IsTrue(set.Overlaps(set2));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetEquals_Null()
        {
            (new HashSet<char>()).SetEquals(null);
        }

        [Test]
        public void TestSetEquals()
        {
            HashSet<int> set = GetEmptyIntSet();
            Assert.IsFalse(set.SetEquals(new int[] { 1 }));
            Assert.IsTrue(set.SetEquals(new FclGeneric.List<int>()));

            for (int i = 1; i <= 5; i++)
            {
                set.Add(i);
            }
            var set2 = GetIntSetFromOneToFive();
            Assert.IsTrue(set.SetEquals(set2));

            set.Remove(1);
            Assert.IsFalse(set.SetEquals(set2));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptWith_Null()
        {
            (new HashSet<char>()).SymmetricExceptWith(null);
        }

        [Test]
        public void TestSymmetricExceptWith()
        {
            HashSet<int> set = GetEmptyIntSet();
            Assert.IsTrue(set.IsEmpty);
            set.SymmetricExceptWith(new FclGeneric.List<int>());
            Assert.IsTrue(set.IsEmpty);
            set.SymmetricExceptWith(new int[] { 1, 3 });
            Assert.IsTrue(set.SetEquals(new int[] { 1, 3 }));

            set.SymmetricExceptWith(set);
            Assert.IsTrue(set.IsEmpty);

            for (int i = 0; i < 5; i++)
            {
                set.Add(i * 2 + 1);
            }
            var set2 = GetIntSetFromOneToFive().ToArray();
            set.SymmetricExceptWith(set2);
            Assert.IsTrue(set.SetEquals(new int[] { 2, 4, 7, 9 }));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionWith_Null()
        {
            (new HashSet<char>()).UnionWith(null);
        }

        [Test]
        public void TestUnionWith()
        {
            HashSet<int> set = GetEmptyIntSet();
            Assert.IsTrue(set.IsEmpty);
            set.UnionWith(new FclGeneric.List<int>());
            Assert.IsTrue(set.IsEmpty);
            set.UnionWith(new int[] { 1, 3 });
            Assert.That(set.Count, Is.EqualTo(2));

            var set2 = GetIntSetFromOneToFive().ToArray();
            Assert.IsTrue(set.IsProperSubsetOf(set2));
            set.UnionWith(set2);
            Assert.IsTrue(set.SetEquals(set2));

            set.Remove(4);
            set.Remove(5);
            Assert.IsTrue(set.SetEquals(new int[] { 1, 2, 3 }));
            set.UnionWith(new int[] { 1, 3, 5, 7, 9 });
            Assert.IsTrue(set.SetEquals(new int[] { 1, 2, 3, 5, 7, 9 }));
        }

        #endregion
    }
}
