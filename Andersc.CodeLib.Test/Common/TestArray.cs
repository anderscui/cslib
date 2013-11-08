using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestArray
    {
        [Test]
        public void TestArrayInit()
        {
            int[] array = new int[3];
            CollectionAssert.AreEqual(array, new int[] { 0, 0, 0 });
        }

        [Test]
        public void TestClear_Boolean_Array()
        {
            bool[] bools = { true, false, true, true };
            Array.Clear(bools, 2, 2);
            CollectionAssert.AreEqual(new bool[] { true, false, false, false }, bools);
        }
    }
}
