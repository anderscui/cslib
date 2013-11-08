using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestFclHashSet
    {
        [Test]
        public void TestAdd()
        {
            FclHashSet<int> set = new FclHashSet<int>();
            set.Add(10);
            set.Add(11);
            set.Add(13);
            set.Add(14);
            set.Add(10);
            set.Add(17);
            set.Add(24);
            set.Add(18);
        }
    }
}
