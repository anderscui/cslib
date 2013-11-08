using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Algorithm;
using Andersc.CodeLib.Common;
using Andersc.CodeLib.Algorithm.Sorting;

namespace Andersc.CodeLib.Tester.Algorithm
{
    [TestFixture]
    public class TestSorters
    {
        [Test]
        public void TestQuickSort()
        {
            int[] a = { 4, 6, 3, 9, 5 };
            int[] backup = new int[a.Length];
            Array.Copy(a, backup, a.Length);

            Sorters.MyQuickSort(a);
            a.PrintToConsole();
            backup.PrintToConsole();
        }

        [Test]
        public void TestQuickSort2()
        {
            int[] a = { 8, 1, 4, 9, 0, 3, 5, 2, 7, 6 };
            a.PrintToConsole();

            Sorters.MyQuickSort(a);
            a.PrintToConsole();
        }

        [Test]
        public void TestQuickSort3()
        {
            int[] a = { 0, 1 };
            a.PrintToConsole();

            Sorters.MyQuickSort(a);
            a.PrintToConsole();
        }
    }
}
