using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common
{
    public class FclHashSet<T>
    {
        internal struct ElementCount
        {
            internal int uniqueCount;
            internal int unfoundCount;
        }

        internal struct Slot
        {
            internal int hashCode;
            internal T value;
            internal int next;

            public override string ToString()
            {
                return string.Format("hashCode: {0}; value: {1}; next: {2}", hashCode, value, next);
            }
        }

        private const int Lower31BitMask = 2147483647;
        private const int GrowthFactor = 2;
        private const int StackAllocThreshold = 100;
        private const int ShrinkThreshold = 3;
        private const string CapacityName = "Capacity";
        private const string ElementsName = "Elements";
        private const string ComparerName = "Comparer";
        private const string VersionName = "Version";

        public int Count
        {
            get { return this.m_count; }
        }

        private int[] m_buckets;
        private FclHashSet<T>.Slot[] m_slots;

        private int m_count;
        /// <summary>
        /// the free size.
        /// </summary>
        private int m_lastIndex;
        private int m_freeList;
        private IEqualityComparer<T> m_comparer;
        private int m_version;

        public FclHashSet() : this(EqualityComparer<T>.Default)
        { }

        public FclHashSet(IEqualityComparer<T> comparer)
        {
            if (comparer.IsNull())
            {
                comparer = EqualityComparer<T>.Default;
            }

            this.m_comparer = comparer;
            this.m_lastIndex = 0;
            this.m_count = 0;
            this.m_freeList = -1;
            this.m_version = 0;
        }

        public bool Add(T item)
        {
            return this.AddIfNotPresent(item);
        }

        public bool Remove(T item)
        {
            if (this.m_buckets != null)
            {
                int num = this.InternalGetHashCode(item);
                int num2 = num % this.m_buckets.Length;
                int num3 = -1;
                for (int i = this.m_buckets[num2] - 1; i >= 0; i = this.m_slots[i].next)
                {
                    // If find the item.
                    if (this.m_slots[i].hashCode == num && this.m_comparer.Equals(this.m_slots[i].value, item))
                    {
                        if (num3 < 0)
                        {
                            this.m_buckets[num2] = this.m_slots[i].next + 1;
                        }
                        else
                        {
                            this.m_slots[num3].next = this.m_slots[i].next;
                        }
                        this.m_slots[i].hashCode = -1;
                        this.m_slots[i].value = default(T);
                        this.m_slots[i].next = this.m_freeList;
                        this.m_count--;
                        this.m_version++;
                        if (this.m_count == 0)
                        {
                            this.m_lastIndex = 0;
                            this.m_freeList = -1;
                        }
                        else
                        {
                            this.m_freeList = i;
                        }
                        return true;
                    }
                    num3 = i;
                }
            }

            return false;
        }

        public bool Contains(T item)
        {
            if (this.m_buckets != null)
            {
                int num = this.InternalGetHashCode(item);
                for (int i = this.m_buckets[num % this.m_buckets.Length] - 1; i >= 0; i = this.m_slots[i].next)
                {
                    if (this.m_slots[i].hashCode == num && this.m_comparer.Equals(this.m_slots[i].value, item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool AddIfNotPresent(T value)
        {
            if (this.m_buckets == null)
            {
                this.Initialize(0);
            }

            foreach (Slot s in m_slots)
            {
                Console.WriteLine(s);
            }
            ObjectDumper.Write(m_buckets, 2);

            int hash = this.InternalGetHashCode(value);
            int index = hash % this.m_buckets.Length; // Get Hash Index

            for (int i = this.m_buckets[hash % this.m_buckets.Length] - 1; i >= 0; i = this.m_slots[i].next)
            {
                if (this.m_slots[i].hashCode == hash && this.m_comparer.Equals(this.m_slots[i].value, value))
                {
                    return false;
                }
            }

            int freeList;
            if (this.m_freeList >= 0)
            {
                freeList = this.m_freeList;
                this.m_freeList = this.m_slots[freeList].next;
            }
            else
            {
                if (this.m_lastIndex == this.m_slots.Length)
                {
                    this.IncreaseCapacity();
                    index = hash % this.m_buckets.Length; // recalculate;
                }
                freeList = this.m_lastIndex;
                this.m_lastIndex++;
            }

            this.m_slots[freeList].hashCode = hash;
            this.m_slots[freeList].value = value;
            this.m_slots[freeList].next = this.m_buckets[index] - 1;
            this.m_buckets[index] = freeList + 1;
            this.m_count++;
            this.m_version++;

            foreach (Slot s in m_slots)
            {
                Console.WriteLine(s);
            }
            ObjectDumper.Write(m_buckets, 2);
            Console.WriteLine();

            return true;
        }

        private void Initialize(int capacity)
        {
            int prime = MathHelper.GetNextPrime(capacity);
            this.m_buckets = new int[prime];
            this.m_slots = new FclHashSet<T>.Slot[prime];
        }

        private int InternalGetHashCode(T item)
        {
            if (item == null)
            {
                return 0;
            }

            return this.m_comparer.GetHashCode(item) & Lower31BitMask;
        }

        private void IncreaseCapacity()
        {
            int num = this.m_count * 2;
            if (num < 0)
            {
                num = this.m_count;
            }
            
            int prime = MathHelper.GetNextPrime(num); // first is 3;
            if (prime <= this.m_count)
            {
                throw new ArgumentException("CapacityOverflow");
            }

            FclHashSet<T>.Slot[] array = new FclHashSet<T>.Slot[prime];
            if (this.m_slots != null)
            {
                Array.Copy(this.m_slots, 0, array, 0, this.m_lastIndex);
            }

            int[] array2 = new int[prime];
            for (int i = 0; i < this.m_lastIndex; i++)
            {
                int num2 = array[i].hashCode % prime;
                array[i].next = array2[num2] - 1;
                array2[num2] = i + 1;
            }

            this.m_slots = array;
            this.m_buckets = array2;
        }
    }
}
