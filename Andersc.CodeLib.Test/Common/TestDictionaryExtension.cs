using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.Extension;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestDictionaryExtension
    {
        [Test]
        public void TestIDictionaryCustomString()
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < 5; i++)
            {
                ht.Add(i, i.ToString());
            }
            Console.WriteLine(ht.ToCustomString());
        }

        [Test]
        public void TestGenericDicCustomString()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            for (int i = 0; i < 5; i++)
            {
                dic.Add(i, i.ToString());
            }
            Console.WriteLine(dic.ToCustomString());
        }
    }
}
