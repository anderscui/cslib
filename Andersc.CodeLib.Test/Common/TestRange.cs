using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestRange
    {
        [Test]
        public void TestContains()
        {
            TimeSpan step = new TimeSpan(30, 0, 0, 0);
            DateTimeRange range = new DateTimeRange(new DateTime(2009, 1, 1), new DateTime(2010, 4, 20), step);
            Assert.IsTrue(range.Contains(range.Start + step));
            Assert.IsFalse(range.Contains(range.End.AddDays(1)));
        }

        [Test]
        public void TestGetEnumerator()
        {
            int start = 0, end = 9;
            Int32Range range = new Int32Range(start, end);
            Assert.AreEqual(10, range.Count());

            Dictionary<int, string> dic = new Dictionary<int, string>();
            Hashtable ht = new Hashtable();
        }
    }
}
