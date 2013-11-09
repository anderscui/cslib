using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Andersc.CodeLib.Common
{
    public static class EnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return (enumerable.IsNull() || (enumerable.Count() == 0));
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
        {
            return (enumerable.IsNotNull() && (enumerable.Count() > 0));
        }

        public static bool Contains<T>(this IEnumerable enumerable, T value, Func<T, T, bool> areEqual)
        {
            foreach (T item in enumerable)
            {
                if (areEqual(item, value))
                {
                    return true;
                }
            }

            return false;
        }

        // TODO: Impl.
        //public static bool IsAscOrdered<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
        //{
        //    if (enumerable.IsNull()) { throw new ArgumentNullException("enumerable"); }

        //    // Avoid unnecessary counting.
        //    int count = 0;
        //    foreach (T item in enumerable)
        //    {
        //        count++;
        //        if (count >= 2)
        //        {
        //            break;
        //        }
        //    }

        //    if (count <= 1)
        //    {
        //        return true;
        //    }

        //    IEnumerator<T> enumerator = enumerable.GetEnumerator();
        //    T a, b;
        //    while (enumerator.MoveNext())
        //    {
        //        a = enumerator.Current;
        //        if (enumerator.MoveNext())
        //        {
        //            b = enumerator.Current;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }

        //    return true;
        //}

        //public static bool IsDescOrdered<T>(this T[] array) where T : IComparable<T>
        //{
        //    if (array.IsNull()) { throw new ArgumentNullException("array"); }

        //    if (array.Length <= 1)
        //    {
        //        return true;
        //    }

        //    for (int i = 1; i < array.Length; i++)
        //    {
        //        if (array[i - 1].CompareTo(array[i]) < 0)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }
}
