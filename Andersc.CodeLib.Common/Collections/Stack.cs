using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public class Stack<T> : IStack<T>, IEnumerable<T>
    {
        private static readonly int DefaultCapacity = 4;
        private T[] data;
        private int size;

        #region Constructors

        public Stack() : this(DefaultCapacity) 
        {}

        public Stack(int capacity)
        {
            if (capacity < 0) { throw new ArgumentOutOfRangeException("capacity must not be negative."); }

            data = new T[capacity];
            size = 0;
        }

        public Stack(IEnumerable<T> enumerable)
        {
            if (enumerable.IsNull())
            {
                throw new ArgumentNullException("enumerable");
            }

            ICollection<T> collection = enumerable as ICollection<T>;
            if (collection.IsNotNull())
            {
                int count = collection.Count;
                data = new T[count];
                collection.CopyTo(data, 0);
                size = count;
                return;
            }

            data = new T[DefaultCapacity];
            size = 0;
            foreach (T item in enumerable)
            {
                Push(item);
            }
        }

        #endregion

        public bool IsEmpty
        {
            get { return (size == 0); }
        }

        public int Count
        {
            get { return size; }
        }

        public T Top
        {
            get
            {
                if (IsEmpty)
                {
                    throw new StackUnderflowException();
                }

                return data[size - 1];
            }
        }

        public void Push(T item)
        {
            if (size == data.Length)
            {
                int newCapacity = IsEmpty ? DefaultCapacity : data.Length * 2;
                T[] array = new T[newCapacity];
                Array.Copy(data, array, size);
                data = array;
            }

            data[size++] = item;
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new StackUnderflowException();
            }

            T result = data[--size];
            data[size] = default(T);
            return result;
        }

        public void Clear()
        {
            Array.Clear(data, 0, size);
            size = 0;
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = size - 1; i >= 0; i--)
            {
                yield return data[i];
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
