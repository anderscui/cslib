using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public class Queue<T> : IQueue<T>, IEnumerable<T>
    {
        private static readonly int DefaultCapacity = 4;
        private static readonly int MinimumGrow = 4;

        private T[] data;
        /// <summary>
        /// Index of front element.
        /// </summary>
        private int front;
        /// <summary>
        /// Next index to enqueue.
        /// </summary>
        private int end;
        private int size;

        //private static T[] emptyArray = new T[0];

        #region Constructors

        public Queue()
            : this(0)
        { }

        public Queue(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity must not be negative.");
            }

            data = new T[capacity];
            front = end = 0;
            size = 0;
        }

        public Queue(IEnumerable<T> enumerable)
        {
            if (enumerable.IsNull())
            {
                throw new ArgumentNullException("enumerable");
            }

            data = new T[DefaultCapacity];
            size = 0;
            foreach (T item in enumerable)
            {
                Enqueue(item);
            }
        }

        #endregion

        #region IQueue<T> Members

        public void Enqueue(T item)
        {
            if (size == data.Length)
            {
                int newCapacity = data.Length * 2;
                if (newCapacity < data.Length + MinimumGrow)
                {
                    newCapacity = data.Length + MinimumGrow;
                }
                SetCapacity(newCapacity);
            }

            data[end] = item;
            end = Wraparound(end);
            size++;
        }

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new CollectionEmptyException();
            }

            T result = data[front];
            data[front] = default(T);
            size--;
            front = Wraparound(front);

            return result;
        }

        public T Front
        {
            get
            {
                if (IsEmpty)
                {
                    throw new CollectionEmptyException();
                }

                return data[front];
            }
        }

        public void Clear()
        {
            if (front < end)
            {
                Array.Clear(data, front, size);
            }
            else
            {
                // TODO: if empty, process all elements?
                Array.Clear(data, front, data.Length - front);
                Array.Clear(data, 0, end);
            }

            front = end = 0;
            size = 0;
        }

        public int Count
        {
            get { return size; }
        }

        public bool IsEmpty
        {
            get { return (size == 0); }
        }

        /// <summary>
        /// Increate an index with wraparound.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int Wraparound(int index)
        {
            return (index + 1) % data.Length;
        }

        private void SetCapacity(int newCapacity)
        {
            T[] array = new T[DefaultCapacity];
            if (size > 0)
            {
                if (front < end)
                {
                    Array.Copy(data, front, array, 0, size);
                }
                else
                {
                    Array.Copy(data, front, array, 0, data.Length - front);
                    Array.Copy(data, 0, array, data.Length - front, end);
                }
            }

            data = array;
            front = 0;
            end = (size == newCapacity) ? 0 : size;
        }

        private T GetElement(int offset)
        {
            return data[(front + offset) % data.Length];
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            for (int offset = 0; offset < size; offset++)
            {
                yield return GetElement(offset);
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
