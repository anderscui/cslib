/*
 * Created by: Anders Cui
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestSortedList
    {
        [Test]
        public void TestCommonOperations()
        {
            SortedList<string, int> list = new SortedList<string, int>();
            list.Add("Anders", 30);
            list.Add("Bill", 50);
            list.Add("Steve", 40);

            Assert.AreEqual(3, list.Count);
        }
    }
}
