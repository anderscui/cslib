using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Algorithm.ProjectEuler;

namespace Andersc.CodeLib.Tester.Algorithm
{
    [TestFixture]
    public class TestProjectEuler
    {
        [Test]
        public void TestQ003()
        {
            Assert.That(ProjectEulerQs.Q003(6), Is.EqualTo(3));
            Assert.That(ProjectEulerQs.Q003(13195), Is.EqualTo(29));
            Console.WriteLine(ProjectEulerQs.Q003(600851475143));
        }

        [Test]
        public void TestQ004()
        {
            Console.WriteLine(ProjectEulerQs.Q004());
        }

        [Test]
        public void TestQ005()
        {
            Console.WriteLine(ProjectEulerQs.Q005());
        }

        [Test]
        public void TestQ016()
        {
            Console.WriteLine(ProjectEulerQs.Q016(1000));
        }
    }
}
