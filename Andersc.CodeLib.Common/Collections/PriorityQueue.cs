using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    // TODO: Impl my IEnumerable<T> interface with other collection types.
    public class PriorityQueue<T>
    {
        private static readonly int DefaultCapacity = 16;

        private T[] array;
        private IComparer<T> comparer;

        public int Count { get; set; }

        public int Capacity
        {
            get { return array.Length - 1; }
        }

        public bool IsEmpty
        {
            get { return (Count == 0); }
        }

        /// <summary>
        /// A value determines whether it is needed to extend current array.
        /// </summary>
        private bool IsFull
        {
            get { return (Count + 1 == array.Length); }
        }

        public PriorityQueue() : this((IComparer<T>)null) { }

        public PriorityQueue(IComparer<T> comparer)
        {
            Count = 0;
            this.comparer = comparer ?? Comparer<T>.Default;
            array = new T[DefaultCapacity + 1];
        }

        public PriorityQueue(IEnumerable<T> items)
        {
            comparer = Comparer<T>.Default;
            Count = items.Count();
            array = new T[(Count + 2) * 11 / 10];

            int i = 1;
            foreach (T item in items)
            {
                array[i++] = item;
            }

            BuildHeap();
        }

        public T Element
        {
            get
            {
                if (IsEmpty)
                {
                    throw new CollectionEmptyException("Heap is empty.");
                }

                return array[1];
            }
        }

        // O(logN);
        public bool Add(T item)
        {
            if (IsFull)
            {
                DoubleArray();
            }

            // Precolate up.
            int hole = ++Count;
            array[0] = item;

            for (; comparer.Compare(item, array[hole / 2]) < 0; hole /= 2)
            {
                array[hole] = array[hole / 2];
            }
            array[hole] = item;

            return true;
        }

        public T Remove()
        {
            T min = Element;
            array[1] = array[Count--];
            PercolateDown(1);

            return min;
        }

        public void Clear()
        {
            Count = 0;
        }

        private void BuildHeap()
        {
            for (int i = Count / 2; i > 0; i--)
            {
                PercolateDown(i);
            }
        }

        private void PercolateDown(int hole)
        {
            int child;
            T temp = array[hole];

            for (; hole * 2 <= Count; hole = child)
            {
                child = hole * 2;
                if (child != Count && comparer.Compare(array[child + 1], array[child]) < 0)
                {
                    child++;
                }

                if (comparer.Compare(array[child], temp) < 0)
                {
                    array[hole] = array[child];
                }
                else
                {
                    break;
                }
            }

            array[hole] = temp;
        }

        private void DoubleArray()
        {
            T[] newArray = new T[Count * 2 + 1];
            Array.Copy(array, newArray, array.Length);

            array = newArray;
        }
    }
}
