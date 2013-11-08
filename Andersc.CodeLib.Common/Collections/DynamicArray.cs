using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    /// <summary>
    /// In Book1, this class is used to provide two features for built-in Array: 
    /// Non-zero base index and Resizing. Actually, it's too simple, in practice, 
    /// you can use Array.Resize() and Array.CreateInstance() methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicArray<T>
    {
        protected T[] array;
        protected int baseIndex;

        #region Properties and Indexers

        public int Rank
        {
            get { return 1; }
        }

        public T[] Values
        {
            get { return array; }
        }

        public int LowerBound
        {
            get { return baseIndex; }
        }

        public int Length
        {
            get { return array.Length; }
            set
            {
                if (array.Length != value)
                {
                    T[] newArray = new T[value];
                    int min = array.Length < value ? array.Length : value;
                    for (int i = 0; i < min; i++)
                    {
                        newArray[i] = array[i];
                    }
                    array = newArray;
                }
            }
        }

        public T this[int pos]
        {
            get { return array[pos - baseIndex]; }
            set { array[pos - baseIndex] = value; }
        } 

        #endregion

        #region Constructors

        public DynamicArray(int length, int baseIndex)
        {
            this.array = new T[length];
            this.baseIndex = baseIndex;
        }

        public DynamicArray() : this(0, 0) { }

        public DynamicArray(int length) :
            this(length, 0) { } 

        #endregion

        public void CopyFrom(DynamicArray<T> from)
        {
            if (from != this)
            {
                if (array.Length != from.array.Length)
                {
                    array = new T[from.array.Length];
                }
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = from.array[i];
                }
                baseIndex = from.baseIndex;
            }
        }

        #region Custom Conversions

        public static implicit operator DynamicArray<T>(T[] array)
        {
            if (array == null) { return null; }

            DynamicArray<T> newArray = new DynamicArray<T>(array.Length);
            Array.Copy(array, newArray.Values, array.Length);

            return newArray;
        }

        public static implicit operator T[](DynamicArray<T> array)
        {
            if (array == null) { return null; }

            T[] newArray = new T[array.Length];
            Array.Copy(array.Values, newArray, newArray.Length);

            return newArray;
        }

        #endregion
    }
}
