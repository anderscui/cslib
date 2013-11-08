using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Algorithm.Sorting
{
    public static class Sorters
    {
        public static void MyQuickSort(int[] array)
        {
            if (array == null || array.Length <= 1) { return; }

            //MyQuickSort(array, 0, array.Length - 1);
            QuickSort(array, 0, array.Length - 1);
        }

        private static void MyQuickSort(int[] array, int low, int high)
        {
            if (low >= high) { return; }

            int pivot = array[low];
            // Begins partitioning
            int i = low + 1, j = high;
            while (i < j)
            {
                while (j >= i && array[j] > pivot)
                    j--;
                while (i <= j && array[i] < pivot)
                    i++;
                if (i < j)
                {
                    Helper.Swap(array, i, j);
                }
            }
            // Resotres pivot
            if (i > j)
            {
                Helper.Swap(array, low, j);
            }

            MyQuickSort(array, low, j - 1);
            MyQuickSort(array, j + 1, high);
        }

        public static void QuickSort(int[] array, int low, int high)
        {
            int i = 0, j = 0, s;
            if (low < high)
            {
                i = low - 1;
                j = high + 1;
                s = array[(low + high) / 2];
                while (true)
                {
                    while (array[++i] < s) ;
                    while (array[--j] > s) ;
                    if (i >= j)
                    {
                        break;
                    }
                    Helper.Swap(array, i, j);
                }
            }
            QuickSort(array, low, i - 1);
            QuickSort(array, j + 1, high);
        }
    }
}
