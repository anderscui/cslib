using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestYield
    {
        private IEnumerable<int> GetOneTwoThree()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        [Test]
        public void TestYieldReturn()
        {
            foreach (var item in GetOneTwoThree())
            {
                Console.WriteLine(item);
            }
        }
    }
}
