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
    public class TestString
    {
        [Test]
        public void TestModifyStringPara()
        {
            string input = "hello";
            string original = input;
            ModifyStringPara(input);
            Assert.IsTrue(object.ReferenceEquals(input, original));
            Console.WriteLine(original);
            Console.WriteLine(input);
        }

        private void ModifyStringPara(string input)
        {
            string original = input;
            input = input + "a";
            Console.WriteLine(object.ReferenceEquals(original, input));
        }

        [Test]
        public void TestSplit()
        {
            // TODO: use simple codes;
            string s = "Hello   world    ";
            Console.WriteLine(string.Join(" ", s.Split(' ').Where(str => str.Trim().Length > 0).Select(str => str.Trim())));
        }
    }
}
