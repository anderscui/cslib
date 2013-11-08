using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestEnvironmentHelper
    {
        [Test]
        public void TestGetEnvironmentVariables()
        {
            IDictionary vars = EnvironmentHelper.GetEnvironmentVariables();
            Assert.IsTrue(vars.Count > 0);
            foreach (object key in vars.Keys)
            {
                Console.WriteLine("Key: {0}, Value: {1}", key, vars[key]);
            }
        }

        [Test]
        public void TestHasEnvironmentVar()
        {
            string key = "Path";
            Assert.IsTrue(EnvironmentHelper.HasEnvironmentVar(key));
            key = (new Guid()).ToString();
            Assert.IsFalse(EnvironmentHelper.HasEnvironmentVar(key));
        }
    }
}
