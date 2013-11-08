using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestDateTimeHelper
    {
        [Test]
        [ExpectedException("System.ArgumentException")]
        public void GetDateTimeUnitException()
        {
            // unit string is null or empty.
            string unit = string.Empty;
            DateTimeUnit dtu = DateTimeHelper.GetDateTimeUnit(unit);
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void GetDateTimeUnitInvalidUnit()
        {
            // unit string is invalid.
            string unit = "dday";
            DateTimeUnit day = DateTimeHelper.GetDateTimeUnit(unit);
        }

        [Test]
        public void GetDateTimeUnit()
        {
            string unit = "s";
            DateTimeUnit s = DateTimeHelper.GetDateTimeUnit(unit);
            Assert.AreEqual(DateTimeUnit.Second, s);

            // not case sensitive.
            unit = "SECONd";
            DateTimeUnit second = DateTimeHelper.GetDateTimeUnit(unit);
            Assert.AreEqual(DateTimeUnit.Second, second);
        }

        [Test]
        public void AddDateTime()
        {
            string unit = "S";
            DateTimeUnit second = DateTimeHelper.GetDateTimeUnit(unit);

            DateTime dt = new DateTime(1981, 11, 6);
            Console.WriteLine("Original value: " + dt.ToString());

            DateTime dt2007 = DateTimeHelper.AddDateTime(dt, DateTimeUnit.Year, 26);
            Console.WriteLine("New value: " + dt2007.ToString());
            Assert.AreEqual(2007, dt2007.Year);

            DateTime dt20 = DateTimeHelper.AddDateTime(dt, DateTimeUnit.Hour, 20);
            Console.WriteLine("New value: " + dt20.ToString());
            Assert.AreEqual(20, dt20.Hour);

            DateTime dt1127 = DateTimeHelper.AddDateTime(dt, DateTimeUnit.Week, 3);
            Console.WriteLine("New value: " + dt1127.ToString());
            Assert.AreEqual(27, dt1127.Day);
        }

        [Test]
        public void TestDateFormat()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine("Universal Time: {0}", now.ToUniversalTime());
            Console.WriteLine("Long Date: {0}", now.ToLongDateString());
            //Tue, 17 Feb 2009 00:00:00 +0800
            Console.WriteLine("Full: {0}", now.ToString("ddd, dd MMM yyyy HH:mm:ss K"));
        }
    }
}
