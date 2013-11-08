using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Sorting;
using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestIntArraySorter
    {
        private int[] GetIntArray()
        {
            //return new int[] { 9, 3, 8, 7, 2, 8 };
            return RandomGenerator.GetRandomInt32Array(20, 0, 100);
        }

        private int[] GetLargeIntArrayForPerformanceTest()
        {
            // Return an array with length of 2^14.
            return RandomGenerator.GetRandomInt32Array(16384, 0, 10000);
        }

        [Test]
        public void TestPerformanceForAllSorters()
        {
            int[] original = GetLargeIntArrayForPerformanceTest();
            int[] bubbleArray = original.Clone() as int[];
            int[] selectionArray = original.Clone() as int[];
            int[] insertionArray = original.Clone() as int[];
            int[] shellArray = original.Clone() as int[];
            int[] mergeArray = original.Clone() as int[];
            int[] quickArray = original.Clone() as int[];

            int[] almostOrderedArray = original.Clone() as int[];
            IntArraySorter.QuickSort(almostOrderedArray);
            int upperBound = Math.Min(10, almostOrderedArray.Length >> 1);
            for (int i = 0; i < upperBound; i++)
            {
                almostOrderedArray.Swap(i, almostOrderedArray.Length - i - 1);
            }
            
            int iteration = 1;
            CodeTimer.Time("BubbleSorter", iteration, 
                () => IntArraySorter.BubbleSort(bubbleArray)
            );

            CodeTimer.Time("SelectionSorter", iteration,
                () => IntArraySorter.SelectionSort(selectionArray)
            );

            // Insertion is good for an almost ordered array.
            CodeTimer.Time("InsertionSort_For_Almost_Ordered_Array", iteration,
                () => IntArraySorter.SelectionSort(almostOrderedArray)
            );

            CodeTimer.Time("InsertionSort", iteration,
                () => IntArraySorter.InsertionSort(insertionArray)
            );

            CodeTimer.Time("ShellSort", iteration,
                () => IntArraySorter.ShellSort(shellArray)
            );

            CodeTimer.Time("MergeSort", iteration,
                () => IntArraySorter.MergeSort(quickArray)
            );

            // TODO: Make no sense, the array has been changed after one sorting?
            CodeTimer.Time("QuickSort", iteration,
                () => IntArraySorter.QuickSort(quickArray)
            );
        }

        [Test]
        public void TestBubbleSort()
        {
            Console.WriteLine("Testing {0} Start...", ObjectHelper.GetMethodName());

            int[] array = GetIntArray();
            int[] backup = array.Clone() as int[];

            Console.WriteLine("Original Array: ");
            backup.PrintToConsole();

            IntArraySorter.BubbleSort(array);
            CollectionAssert.AreEquivalent(backup, array);
            Assert.That(array.IsAscOrdered(), Is.True);

            Console.WriteLine("Ordered Array: ");
            array.PrintToConsole();

            Console.WriteLine("Testing {0} End...", ObjectHelper.GetMethodName());
        }

        [Test]
        public void TestSelectionSort()
        {
            Console.WriteLine("Testing {0} Start...", ObjectHelper.GetMethodName());

            int[] array = GetIntArray();
            int[] backup = array.Clone() as int[];

            Console.WriteLine("Original Array: ");
            backup.PrintToConsole();

            IntArraySorter.SelectionSort(array);
            CollectionAssert.AreEquivalent(backup, array);
            Assert.That(array.IsAscOrdered(), Is.True);

            Console.WriteLine("Ordered Array: ");
            array.PrintToConsole();

            Console.WriteLine("Testing {0} End...", ObjectHelper.GetMethodName());
        }

        [Test]
        public void TestInsertionSort()
        {
            Console.WriteLine("Testing {0} Start...", ObjectHelper.GetMethodName());

            int[] array = GetIntArray();
            int[] backup = array.Clone() as int[];

            Console.WriteLine("Original Array: ");
            backup.PrintToConsole();

            IntArraySorter.InsertionSort(array);
            CollectionAssert.AreEquivalent(backup, array);
            Assert.That(array.IsAscOrdered(), Is.True);

            Console.WriteLine("Ordered Array: ");
            array.PrintToConsole();

            Console.WriteLine("Testing {0} End...", ObjectHelper.GetMethodName());
        }

        [Test]
        public void TestInsertionSortPart()
        {
            int[] array = { 9, 3, 8, 7, 2, 8 };
            Console.WriteLine("Original Array: ");
            array.PrintToConsole();

            IntArraySorter.InsertionSort(array, 1, 4);
            Assert.AreEqual(array[0], 9);
            Assert.AreEqual(array[1], 2);
            Assert.AreEqual(array[4], 8);

            Console.WriteLine("Ordered Array: ");
            array.PrintToConsole();
        }

        [Test]
        public void TestShellSort()
        {
            Console.WriteLine("Testing {0} Start...", ObjectHelper.GetMethodName());

            int[] array = GetIntArray();
            int[] backup = array.Clone() as int[];

            Console.WriteLine("Original Array: ");
            backup.PrintToConsole();

            IntArraySorter.ShellSort(array);
            CollectionAssert.AreEquivalent(backup, array);
            Assert.That(array.IsAscOrdered(), Is.True);

            Console.WriteLine("Ordered Array: ");
            array.PrintToConsole();

            Console.WriteLine("Testing {0} End...", ObjectHelper.GetMethodName());
        }

        [Test]
        public void TestMergeSort()
        {
            Console.WriteLine("Testing {0} Start...", ObjectHelper.GetMethodName());

            int[] array = GetIntArray();
            int[] backup = array.Clone() as int[];

            Console.WriteLine("Original Array: ");
            backup.PrintToConsole();

            IntArraySorter.MergeSort(array);
            CollectionAssert.AreEquivalent(backup, array);
            Assert.That(array.IsAscOrdered(), Is.True);

            Console.WriteLine("Ordered Array: ");
            array.PrintToConsole();

            Console.WriteLine("Testing {0} End...", ObjectHelper.GetMethodName());
        }

        [Test]
        public void TestQuickSort()
        {
            Console.WriteLine("Testing {0} Start...", ObjectHelper.GetMethodName());

            int[] array = GetIntArray();
            int[] backup = array.Clone() as int[];

            Console.WriteLine("Original Array: ");
            backup.PrintToConsole();

            IntArraySorter.QuickSort(array);
            CollectionAssert.AreEquivalent(backup, array);
            Assert.That(array.IsAscOrdered(), Is.True);

            Console.WriteLine("Ordered Array: ");
            array.PrintToConsole();

            Console.WriteLine("Testing {0} End...", ObjectHelper.GetMethodName());
        }

        [Test]
        public void TestHeapSort()
        {
            Console.WriteLine("Testing {0} Start...", ObjectHelper.GetMethodName());

            int[] array = GetIntArray();
            int[] backup = array.Clone() as int[];

            Console.WriteLine("Original Array: ");
            backup.PrintToConsole();

            IntArraySorter.HeapSort(array);
            CollectionAssert.AreEquivalent(backup, array);
            Assert.That(array.IsAscOrdered(), Is.True);

            Console.WriteLine("Ordered Array: ");
            array.PrintToConsole();

            Console.WriteLine("Testing {0} End...", ObjectHelper.GetMethodName());
        }

        [Test]
        public void TestIsOrdered()
        {
            int[] array = { 2 };
            Assert.That(IntArraySorter.IsOrdered(array), Is.True);

            array = new int[] { 2, 3, 5 };
            Assert.That(IntArraySorter.IsOrdered(array), Is.True);

            array = new int[] { 2, 3, 3, 5, 5 };
            Assert.That(IntArraySorter.IsOrdered(array), Is.True);

            array = new int[] { 2, 3, 5, 4, 1 };
            Assert.That(IntArraySorter.IsOrdered(array), Is.False);
        }

        [Test]
        public void TestCountingSort()
        {
            Console.WriteLine("Testing {0} Start...", ObjectHelper.GetMethodName());

            int[] array = GetIntArray();
            int[] backup = array.Clone() as int[];

            Console.WriteLine("Original Array: ");
            backup.PrintToConsole();

            IntArraySorter.CountingSort(array);
            CollectionAssert.AreEquivalent(backup, array);
            Assert.That(array.IsAscOrdered());

            Console.WriteLine("Ordered Array: ");
            array.PrintToConsole();

            Console.WriteLine("Testing {0} End...", ObjectHelper.GetMethodName());
        }
    }
}
