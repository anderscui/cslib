using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestGenerics
    {
        List<string> stringList = new List<string>()
        {
            "One", "Two", "Three"
        };

        string[] stringArray = new string[]
        {
            "One", "Two", "Three"
        };

        [Test]
        public void TestPredicate()
        {
            string[] lengthNotGreaterThan3 =
                Array.FindAll(stringArray, (str) => { return str.Length <= 3; });
            CollectionAssert.Contains(lengthNotGreaterThan3, "One");
            CollectionAssert.DoesNotContain(lengthNotGreaterThan3, "Three");
        }

        [Test]
        public void TestFunc()
        {
            Func<string, string> toUpper = UppercaseString;
            Assert.AreEqual(null, toUpper(null));
            Assert.AreEqual("ONE", toUpper("one"));
        }

        [Test]
        public void TestAction()
        {
            List<Action> actions = new List<Action>();
            for (int i = 0; i < 5; i++)
            {
                actions.Add(() => Console.WriteLine(i));
            }

            foreach (Action action in actions)
            {
                action();
            }
        }

        [Test]
        public void TestAction2()
        {
            List<Action> actions = new List<Action>();
            for (int i = 0; i < 5; i++)
            {
                int temp = i;
                actions.Add(() => Console.WriteLine(temp));
            }

            foreach (Action action in actions)
            {
                action();
            }
        }

        private string UppercaseString(string input)
        {
            return (input == null) ? null : input.ToUpper();
        }
    }
}
