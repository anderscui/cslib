using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestObjectDumper
    {
        [Test]
        public void TestWrite()
        {
            Stack<int> s = new Stack<int>();
            s.Push(1);
            s.Push(2);
            s.Push(3);
            s.Push(4);
            s.Push(5);

            ObjectDumper.Write(s);

            Dictionary<Stack<int>, string> dict = new Dictionary<Stack<int>, string>();
            dict[s] = "first stack";

            Stack<int> s2 = new Stack<int>();
            s2.Push(10);
            s2.Push(11);
            dict[s2] = "second";

            ObjectDumper.Write(dict);
            ObjectDumper.Write(dict, 3);
        }
    }
}
