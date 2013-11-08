using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestConversions
    {
        [Test]
        public void TestChar2Int()
        {
            char c = '7';
            Console.WriteLine(char.GetNumericValue(c));

            c = 'C';
            var res = c - 'A' + 1;
            Console.WriteLine(res);
        }
    }
}
