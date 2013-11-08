using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common
{
    public static class ArrayExtension
    {
        private static readonly int NotFound = -1;

        // TODO: How to deal with null array, throw an exception or return false?
        public static bool HasElements(this Array array)
        {
            if (array == null || array.Length == 0) { return false; }

            return true;
        }

        /// <summary>
        /// Swaps two elements of the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="index1">The index1.</param>
        /// <param name="index2">The index2.</param>
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            if (index1 == index2) { return; }

            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        /// <summary>
        /// Finds the index of given value in an array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="value">The value.</param>
        /// <returns>If the value is found in the array, return its index; otherwise, -1.</returns>
        /// <remarks>Also can use Array.IndexOf method.</remarks>
        public static int IndexOf<T>(this T[] array, T value)
        {
            if (array.IsNull()) { throw new ArgumentNullException("array"); }

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < array.Length; i++)
            {
                if (comparer.Equals(array[i], value))
                {
                    return i;
                }
            }

            return NotFound;
        }

        /// <summary>
        /// Determines whether an array contains the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the array contains the specified value; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains<T>(this T[] array, T value) where T : IComparable<T>
        {
            return (IndexOf(array, value) >= 0);
        }

        public static int BinarySearch<T>(this T[] array, T key) where T : IComparable<T>
        {
            if (!HasElements(array)) { return -1; }

            return BinarySearch(array, key, 0, array.Length - 1);
        }

        private static int BinarySearch<T>(this T[] array, T key, int lowerBound, int upperBound)
            where T : IComparable<T>
        {
            int currentIndex;

            currentIndex = (lowerBound + upperBound) / 2;
            if (array[currentIndex].CompareTo(key) == 0)
            {
                return currentIndex;
            }
            else if (lowerBound > upperBound)
            {
                return NotFound;
            }
            else
            {
                if (array[currentIndex].CompareTo(key) < 0)
                {
                    return BinarySearch(array, key, currentIndex + 1, upperBound);
                }
                else
                {
                    return BinarySearch(array, key, lowerBound, currentIndex - 1);
                }
            }
        }

        public static T Min<T>(this T[] array) where T : IComparable<T>
        {
            if (array.IsNull()) { throw new ArgumentNullException("array"); }
            if (array.IsEmpty()) { throw new ArgumentException("array", "array must have at least one element."); }

            T min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(min) < 0)
                {
                    min = array[i];
                }
            }

            return min;
        }

        public static T Max<T>(this T[] array) where T : IComparable<T>
        {
            if (array.IsNull()) { throw new ArgumentNullException("array"); }
            if (array.IsEmpty()) { throw new ArgumentException("array", "array must have at least one element."); }

            T max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(max) > 0)
                {
                    max = array[i];
                }
            }

            return max;
        }

        public static void Resize<T>(this T[] array, int newSize)
        {
            Array.Resize(ref array, newSize);
        }

        public static void Init<T>(this T[] array, T initValue)
        {
            if (array.IsEmpty()) { return; }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = initValue;
            }
        }

        public static bool IsAscOrdered<T>(this T[] array) where T : IComparable<T>
        {
            if (array.IsNull()) { throw new ArgumentNullException("array"); }

            if (array.Length <= 1)
            {
                return true;
            }

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1].CompareTo(array[i]) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsDescOrdered<T>(this T[] array) where T : IComparable<T>
        {
            if (array.IsNull()) { throw new ArgumentNullException("array"); }

            if (array.Length <= 1)
            {
                return true;
            }

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1].CompareTo(array[i]) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
        }

        public static void Clear(Array array, int startIndex, int length)
        {
            Array.Clear(array, startIndex, length);
        }

        // TODO: refactor this, use a delegate like Action<T>?
        public static void PrintToConsole<T>(this T[] array)
        {
            Console.WriteLine("************");

            Console.WriteLine("Elements: ");
            foreach (T elem in array)
            {
                Console.Write("{0}  ", elem);
            }

            Console.WriteLine();
            Console.WriteLine("************");
            Console.WriteLine();
        }
    }
}
