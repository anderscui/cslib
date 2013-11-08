using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestString
    {
        [Test]
        public void TestIndexOf_Diff_Patterns()
        {
            var t = "hello, world!";
            string p = null;
            //Console.WriteLine(t.IndexOf(p)); // ex thrown
            p = string.Empty;
            Console.WriteLine(t.IndexOf(p)); // 0

            t = string.Empty;
            Console.WriteLine(t.IndexOf(p)); // 0
        }
    }
}
