using System;
using System.Collections.Generic;
using System.Text;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Common.Sorting
{
    // TODO: Implemetns generic version, and uses extension methods.
    public class IntArraySorter
    {
        private const int QuickSortCutOff = 9;

        public static void BubbleSort(int[] array)
        {
            if (!array.HasElements())
            {
                return;
            }

            // n - 1 times(find the largest element in the sub array)
            for (int i = 1; i < array.Length; i++)
            {
                //bool ordered = true;
                for (int j = 0; j < array.Length - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        //ordered = false;
                        array.Swap(j, j + 1);
                    }
                }

                //if (ordered) { return; }
            }
        }

        public static void SelectionSort(int[] array)
        {
            if (!array.HasElements())
            {
                return;
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                // Select the minimal element in the sub array.
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }

                // Move the minimal element to the right position.
                if (minIndex != i) { array.Swap(i, minIndex); }
            }
        }

        public static void InsertionSort(int[] array)
        {
            if (!array.HasElements())
            {
                return;
            }

            int j;
            for (int i = 1; i < array.Length; i++)
            {
                int current = array[i];
                j = i;

                // Move the larger elements to right position.
                while (j > 0 && array[j - 1] > current)
                {
                    array[j] = array[j - 1];
                    j--;
                }

                // Insert the current element to the right position.
                array[j] = current;
            }
        }

        public static void InsertionSort(int[] array, int low, int high)
        {
            if (!array.HasElements() || (high - low < 1))
            {
                return;
            }

            int j;
            for (int i = low + 1; i <= high; i++)
            {
                int current = array[i];
                j = i;

                // Move the larger elements to right position.
                while (j > low && array[j - 1] > current)
                {
                    array[j] = array[j - 1];
                    j--;
                }

                // Insert the current element to the right position.
                array[j] = current;
            }
        }

        #region ShellSort

        // TODO: Analyze this method.
        //public static void ShellSort(int[] array)
        //{
        //    if (!array.HasElements())
        //    {
        //        return;
        //    }

        //    int inner, outer;
        //    int temp;
        //    int h = GetShellSortInitialValue(array);

        //    while (h > 0)
        //    {
        //        for (outer = h; outer < array.Length; outer++)
        //        {
        //            temp = array[outer];
        //            inner = outer;

        //            while (inner > h - 1 && array[inner - h] >= temp)
        //            {
        //                array[inner] = array[inner - h];
        //                inner -= h;
        //            }

        //            array[inner] = temp;
        //        }

        //        h = (h - 1) / 3;
        //    }
        //}

        //private static int GetShellSortInitialValue(int[] array)
        //{
        //    int h = 1;
        //    while (h <= array.Length / 3)
        //    {
        //        h = 3 * h + 1;
        //    }

        //    return h;
        //}

        public static void ShellSort(int[] array)
        {
            if (!array.HasElements())
            {
                return;
            }

            for (int gap = array.Length / 2; gap > 0;
                gap = (gap == 2) ? 1 : (int)(gap / 2.2))
            {
                for (int i = gap; i < array.Length; i++)
                {
                    int current = array[i];

                    int j = i;
                    for (; j >= gap && array[j - gap] > current; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }

                    array[j] = current;
                }
            }
        } 

        #endregion

        #region MergeSort

        public static void MergeSort(int[] array)
        {
            if (!array.HasElements())
            {
                return;
            }

            int[] workSpace = new int[array.Length];
            MergeSort(array, 0, array.Length - 1, workSpace);
        }

        private static void MergeSort(int[] array, int lowerBound, int upperBound, int[] workspace)
        {
            if (lowerBound < upperBound)
            {
                int center = (lowerBound + upperBound) / 2;
                MergeSort(array, lowerBound, center, workspace);
                MergeSort(array, center + 1, upperBound, workspace);

                Merge(array, workspace, lowerBound, center + 1, upperBound);
            }
        }

        private static void Merge(int[] array, int[] workspace, int leftPos, int rightPos, int rightEnd)
        {
            int tempPos = leftPos;
            int leftEnd = rightPos - 1;
            int number = rightEnd - leftPos + 1;

            while (leftPos <= leftEnd && rightPos <= rightEnd)
            {
                if (array[leftPos] < array[rightPos])
                {
                    workspace[tempPos++] = array[leftPos++];
                }
                else
                {
                    workspace[tempPos++] = array[rightPos++];
                }
            }

            while (leftPos <= leftEnd)
            {
                workspace[tempPos++] = array[leftPos++];
            }

            while (rightPos <= rightEnd)
            {
                workspace[tempPos++] = array[rightPos++];
            }

            // Copy array values back.
            for (int i = 0; i < number; i++, rightEnd--)
            {
                array[rightEnd] = workspace[rightEnd];
            }
        } 

        #endregion

        public static void QuickSort(int[] array)
        {
            if (!array.HasElements() || array.Length <= 1)
            {
                return;
            }

            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] array, int low, int high)
        {
            if (high - low <= QuickSortCutOff)
            {
                InsertionSort(array, low, high);
                return;
            }

            // Sorts low, middle, high
            int middle = (low + high) / 2;
            SortThreeElements(array, low, high, middle);

            // Places pivot at position high - 1;
            array.Swap(middle, high - 1);
            int pivot = array[high - 1];

            // Begins partitioning
            int i, j;
            for (i = low, j = high - 1; ; )
            {
                while (array[++i] < pivot) ;
                while (array[--j] > pivot) ;

                if (i >= j)
                {
                    break;
                }
                array.Swap(i, j);
            }

            // Resotres pivot
            array.Swap(i, high - 1);

            QuickSort(array, low, i - 1);
            QuickSort(array, i + 1, high);
        }

        private static void SortThreeElements(int[] array, int low, int high, int middle)
        {
            if (array[middle] < array[low])
            {
                array.Swap(middle, low);
            }
            if (array[high] < array[low])
            {
                array.Swap(high, low);
            }
            if (array[high] < array[middle])
            {
                array.Swap(middle, high);
            }
        }

        public static void HeapSort(int[] array)
        {
            if (!array.HasElements())
            {
                return;
            }

            PriorityQueue<int> pq = new PriorityQueue<int>(array);
            List<int> list = new List<int>();
            while (!pq.IsEmpty)
            {
                list.Add(pq.Remove());
            }
            Array.Copy(list.ToArray(), array, array.Length);
        }

        /// <summary>
        /// Determines whether the specified array is ordered asc.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>
        /// 	<c>true</c> if the specified array is ordered; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOrdered(int[] array)
        {
            // TODO: Use Predict to deal with four diff order options.
            if (array.IsNull()) { throw new ArgumentNullException("array"); }

            if (array.Length <= 1) { return true; }

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1]) { return false; }
            }

            return true;
        }

        // TODO: Where it comes? Java Algorithm?
        public static void Partition(int[] array, int key)
        {
            if (!array.HasElements()) { return; }

            int i = 0, j, k;
            int lower = -1, upper = -1;

            while (i < array.Length && lower < array.Length && lower <= upper)
            {
                if (lower < 0 && array[i] <= key)
                {
                    i++;
                    continue;
                }

                if (lower < 0)
                {
                    lower = i;
                    upper = lower;
                }

                bool found = false;
                for (j = upper + 1; j < array.Length; j++)
                {
                    if (array[j] <= key)
                    {
                        found = true;
                        upper = j;
                        break;
                    }
                }

                if (!found) { return; }

                int temp = array[upper];
                for (k = upper - 1; k >= lower; k--)
                {
                    array[k + 1] = array[k];
                }
                array[lower] = temp;

                lower++;
                i = upper + 1;
            }
        }

        // TODO: Where it comes? Java Algorithm?
        public static void Partition2(int[] array, int pivot)
        {
            if (!array.HasElements()) { return; }

            int left = 0;
            int right = array.Length - 1;
            int leftPtr = left - 1;
            int rightPtr = right + 1;

            while (true)
            {
                while (leftPtr < right && array[++leftPtr] < pivot) { ; }

                while (rightPtr > left && array[--rightPtr] > pivot) { ; }

                if (leftPtr >= rightPtr)
                {
                    break;
                }
                else
                {
                    array.Swap(leftPtr, rightPtr);
                }
            }
        }

        public static void CountingSort(int[] array)
        {
            if (array.IsNull() || array.IsEmpty() || array.Length == 1) { return; }

            int[] counting = new int[array.Length];
            counting.Initialize();

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] < array[j])
                    {
                        counting[j]++;
                    }
                    else
                    {
                        counting[i]++;
                    }
                }
            } // counting complete.

            int[] result = new int[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[counting[i]] = array[i];
            }

            Array.Copy(result, array, array.Length);
        }
    }
}
