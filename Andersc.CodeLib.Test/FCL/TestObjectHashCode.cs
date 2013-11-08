using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Tester.Helpers;
using System.Drawing;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestObjectHashCode
    {
        [Test]
        public void TestGetHashCode_object()
        {
            object book1 = new Book()
            {
                Publisher = "1",
                Title = "book1"
            };

            object book2 = new Book()
            {
                Publisher = "2",
                Title = "book2"
            };

            Console.WriteLine(book1.GetHashCode());
            Console.WriteLine(book2.GetHashCode());

            Console.WriteLine(RuntimeHelpers.GetHashCode(book1));
        }

        [Test]
        public void TestGetHashCode_valuetype()
        {
            MyStruct s = new MyStruct();
            s.SetMsg("Hello");
            s.ID = 1;
            s.Birthday = new DateTime(1981, 1, 1);

            Console.WriteLine(s.GetHashCode());
            Console.WriteLine(s.GetMsg().GetHashCode());
            //s.SetMsg("NewMsg");
            s.ID = 2;
            s.Birthday = new DateTime(1981, 11, 11);
            Console.WriteLine(s.GetHashCode());
            Console.WriteLine(s.GetMsg().GetHashCode());

            MyStruct s2 = new MyStruct();
            s2.SetMsg(s.GetMsg());
            s2.ID = 3;
            Assert.AreEqual(s.GetHashCode(), s2.GetHashCode());
        }

        [Test]
        public void TestHashCodeStruct()
        {
            BoolHashCodeStruct[] sa = new BoolHashCodeStruct[10];
            for (int i = 0; i < sa.Length; i++)
            {
                sa[i].ID = i;
                sa[i].Msg = "msg" + i;
            }

            foreach (var s in sa)
            {
                Console.WriteLine(s.GetHashCode());
            }
        }

        [Test]
        public void TestGetHashCode_int()
        {
            int i = 111;
            Assert.AreEqual(i, i.GetHashCode());

            i = -111;
            Assert.AreEqual(i, i.GetHashCode());
        }

        [Test]
        public void TestGetHashCode_float()
        {
            float f = 111;
            Assert.AreEqual(f, f.GetHashCode());

            f = -111;
            Assert.AreEqual(f, f.GetHashCode());
        }

        [Test]
        public void TestGetHashCode_Point()
        {
            Point p1 = new Point(3, 4);
            Console.WriteLine(p1.GetHashCode());

            p1.X = 6;
            p1.Y = 8;
            Console.WriteLine(p1.GetHashCode());
        }
    }
}
