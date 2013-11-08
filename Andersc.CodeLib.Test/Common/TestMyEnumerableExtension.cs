using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestMyEnumerableExtension
    {
        [Test]
        public void TestForEach()
        {
            int[] array = { 1, 4, 7, 9, 15 };
            var result = from i in array
                         select i;
            result.ForEach(i => Console.WriteLine(i));

            int counter = 0;
            result.ForEach(i => counter++);
            Assert.AreEqual(array.Length, counter);
        }

        [Test]
        public void TestIsEmpty()
        {
            List<int> list = new List<int> { 1, 4, 7, 9, 15 };
            Assert.IsTrue(list.IsNotEmpty());
            Assert.IsFalse(list.IsEmpty());

            list = new List<int>();
            Assert.IsFalse(list.IsNotEmpty());
            Assert.IsTrue(list.IsEmpty());
        }
    }
}
