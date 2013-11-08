using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    /// <summary>
    /// Provides an implementation of set using hashing.
    /// </summary>
    /// <typeparam name="T">The type of elements in the set.</typeparam>
    public class HashSet<T> : IEnumerable<T>, ISet<T>
    {
        private static readonly int DEFAULT_TABLE_SIZE = 101;

        private int currentSize = 0;
        private int occupied = 0;
        // TODO: Remove this?
        private int modCount = 0;
        private HashEntry<T>[] array;
        private IEqualityComparer<T> _comparer;

        #region Properties

        public int Count
        {
            get { return currentSize; }
        }

        public bool IsEmpty
        {
            get { return (Count == 0); }
        }

        public IEqualityComparer<T> Comparer
        {
            get { return _comparer; }
        }
        #endregion

        #region Constructors

        public HashSet()
            : this((IEqualityComparer<T>)null)
        { }

        public HashSet(IEqualityComparer<T> comparer)
        {
            if (comparer.IsNull())
            {
                comparer = EqualityComparer<T>.Default;
            }
            this._comparer = comparer;
            AllocateArray(DEFAULT_TABLE_SIZE);
            Clear();
        }

        public HashSet(IEnumerable<T> other)
            : this(other, null)
        { }

        public HashSet(IEnumerable<T> other, IEqualityComparer<T> comparer)
        {
            if (comparer.IsNull())
            {
                comparer = EqualityComparer<T>.Default;
            }
            this._comparer = comparer;
            AllocateArray(other.Count() * 2);
            Clear();

            foreach (T item in other)
            {
                Add(item);
            }
        }

        #endregion

        #region Container Methods

        public void Clear()
        {
            currentSize = occupied = 0;
            modCount++;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = null;
            }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The element to add to the set.</param>
        /// <returns>
        /// true if the element is added to the set; false if the element is already in the set.
        /// </returns>
        public bool Add(T item)
        {
            int pos = FindPos(item);
            if (IsActive(pos))
            {
                // Item exists, just return.
                return false;
            }

            array[pos] = new HashEntry<T>(item, true);
            currentSize++;
            // TODO: 
            occupied++;
            modCount++;

            if (occupied > array.Length / 2)
            {
                Rehash();
            }

            return true;
        }

        /// <summary>
        /// Removes the specified item from set.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     <c>true</c> if the item is in set; otherwise, <c>false</c>.
        /// </returns>
        public bool Remove(T item)
        {
            int pos = FindPos(item);
            if (!IsActive(pos))
            {
                return false;
            }

            array[pos].IsActive = false;
            currentSize--;
            modCount++;

            CheckRehashAfterRemoval();

            return true;
        }

        public bool Contains(T item)
        {
            return IsActive(FindPos(item));
        }

        #endregion

        #region Private Helper Methods

        private void AllocateArray(int length)
        {
            array = new HashEntry<T>[length];
        }

        private bool IsActive(int pos)
        {
            return array[pos].IsNotNull() && array[pos].IsActive;
        }

        // Core method.
        private int FindPos(T item)
        {
            int collisionNum = 0;
            // TODO: Use bit mask; test null ref objects;
            int pos = (item.IsNull()) ? 0 : Math.Abs(item.GetHashCode() % array.Length);

            while (array[pos].IsNotNull())
            {
                if (item.IsNull())
                {
                    if (array[pos].Item.IsNull())
                    {
                        break;
                    }
                }
                else if (_comparer.Equals(item, array[pos].Item))
                {
                    break;
                }

                pos += 2 * (++collisionNum) - 1;
                if (pos >= array.Length)
                {
                    pos -= array.Length;
                }
            }

            return pos;
        }

        private void Rehash()
        {
            HashEntry<T>[] oldArray = array;

            // Create a new, empty table.
            AllocateArray(MathHelper.GetNextPrime(4 * Count));
            currentSize = occupied = 0;

            for (int i = 0; i < oldArray.Length; i++)
            {
                if (oldArray[i].IsNotNull() && oldArray[i].IsActive)
                {
                    Add(oldArray[i].Item);
                }
            }
        }

        /// <summary>
        /// Checks rehash necessity after removal.
        /// </summary>
        private void CheckRehashAfterRemoval()
        {
            if (currentSize < array.Length / 8)
            {
                Rehash();
            }
        }

        private void IntersectWithEnumerable(IEnumerable<T> other)
        {
            int currentPos = -1;
            int visited = 0;
            int originalCount = Count;

            while (visited != originalCount)
            {
                do
                {
                    currentPos++;
                } while (currentPos < array.Length && !IsActive(currentPos));

                T value = array[currentPos].Item;
                if (!other.Contains(value, Comparer))
                {
                    // Cannot call Remove method directly, Remove() may cause rehash.
                    array[currentPos].IsActive = false;
                    currentSize--;
                    modCount++;
                }

                visited++;
            }

            CheckRehashAfterRemoval();
        }

        private void IntersectWithHashSetWithSameEC(HashSet<T> other)
        {
            int currentPos = -1;
            int visited = 0;
            int originalCount = Count;

            while (visited != originalCount)
            {
                do
                {
                    currentPos++;
                } while (currentPos < array.Length && !IsActive(currentPos));

                T value = array[currentPos].Item;
                if (!other.Contains(value))
                {
                    array[currentPos].IsActive = false;
                    currentSize--;
                    modCount++;
                }

                visited++;
            }

            CheckRehashAfterRemoval();
        }

        private bool IsSubsetOfHashSetWithSameEC(HashSet<T> other)
        {
            foreach (T current in this)
            {
                if (!other.Contains(current))
                {
                    return false;
                }
            }

            return true;
        }

        private HashSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
        {
            HashSet<T>.ElementCount result;
            if (this.IsEmpty)
            {
                result.UniqueCount = 0;
                result.UnfoundCount = other.Count();

                return result;
            }

            int uniqueCount = 0;
            int unfoundCount = 0;
            foreach (T current in other)
            {
                if (Contains(current))
                {
                    uniqueCount++;
                }
                else
                {
                    unfoundCount++;
                    if (returnIfUnfound)
                    {
                        break;
                    }
                }
            }
            result.UniqueCount = uniqueCount;
            result.UnfoundCount = unfoundCount;

            return result;
        }

        private bool ContainsAllElements(IEnumerable<T> other)
        {
            foreach (T current in other)
            {
                if (!this.Contains(current))
                {
                    return false;
                }
            }

            return true;
        }

        private void SymmetricExceptWithEnumerable(IEnumerable<T> other)
        {
            foreach (T item in other)
            {
                if (Contains(item))
                {
                    Remove(item);
                }
                else
                {
                    Add(item);
                }
            }
        }

        private void SymmetricExceptWithUniqueHashSet(HashSet<T> hashSet)
        {
            foreach (T item in hashSet)
            {
                if (!Remove(item))
                {
                    Add(item);
                }
            }
        }

        #endregion

        #region Static Methods

        private static bool AreEqualityComparersEqual(HashSet<T> set1, HashSet<T> set2)
        {
            return set1.Comparer.Equals(set2.Comparer);
        }

        #endregion

        #region Nested Types

        // TODO: ISerializable
        internal class HashEntry<TItem>
        {
            public TItem Item { get; set; }

            /// <summary>
            /// Determins whether the item is active in a collection.
            /// </summary>
            public bool IsActive { get; set; }

            public HashEntry(TItem e)
                : this(e, true)
            { }

            public HashEntry(TItem item, bool active)
            {
                Item = item;
                IsActive = active;
            }
        }

        internal struct ElementCount
        {
            internal int UniqueCount;
            internal int UnfoundCount;
        } 

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            int currentPos = -1;
            int visited = 0;

            while (visited != Count)
            {
                do
                {
                    currentPos++;
                } while (currentPos < array.Length && !IsActive(currentPos));

                visited++;
                yield return array[currentPos].Item;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region ISet<T> Members

        /// <summary>
        /// Removes all elements in the specified collection from the current set.
        /// </summary>
        /// <param name="other">The collection of items to remove from the set.</param>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            if (IsEmpty) { return; }

            if (other == this)
            {
                Clear();
                return;
            }

            foreach (T item in other)
            {
                Remove(item);
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are also in a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            if (IsEmpty) { return; }

            ICollection<T> collection = other as ICollection<T>;
            if (collection.IsNotNull())
            {
                if (collection.Count == 0)
                {
                    Clear();
                    return;
                }

                HashSet<T> hashSet = other as HashSet<T>;
                if (hashSet.IsNotNull() && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
                {
                    IntersectWithHashSetWithSameEC(hashSet);
                    return;
                }
            }

            IntersectWithEnumerable(other);
        }

        /// <summary>
        /// Determines whether the current set is a property (strict) subset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// 	<c>true</c> if the current set is a correct subset of other; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            ICollection<T> collection = other as ICollection<T>;
            if (collection.IsNotNull())
            {
                if (IsEmpty) { return collection.Count > 0; }

                HashSet<T> hashSet = other as HashSet<T>;
                if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
                {
                    return this.Count < hashSet.Count && this.IsSubsetOfHashSetWithSameEC(hashSet);
                }
            }

            HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
            return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount > 0;
        }

        /// <summary>
        /// Determines whether the current set is a correct superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// 	<c>true</c> if the set is a correct superset of other; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            if (IsEmpty) { return false; }

            ICollection<T> collection = other as ICollection<T>;
            if (collection.IsNotNull())
            {
                if (collection.IsEmpty())
                {
                    return true;
                }

                HashSet<T> hashSet = other as HashSet<T>;
                if (hashSet.IsNotNull() && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
                {
                    return hashSet.Count < this.Count && ContainsAllElements(hashSet);
                }
            }

            HashSet<T>.ElementCount elementCount = CheckUniqueAndUnfoundElements(other, true);
            return elementCount.UniqueCount < this.Count && elementCount.UnfoundCount == 0;
        }

        /// <summary>
        /// Determines whether a set is a subset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// 	<c>true</c> if the current set is a subset of other; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            if (IsEmpty) { return true; }

            HashSet<T> hashSet = other as HashSet<T>;
            if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
            {
                return this.Count <= hashSet.Count && this.IsSubsetOfHashSetWithSameEC(hashSet);
            }

            HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
            return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount >= 0;
        }

        /// <summary>
        /// Determines whether the current set is a superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// 	<c>true</c> if the current set is a superset of other; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            ICollection<T> collection = other as ICollection<T>;
            if (collection.IsNotNull())
            {
                if (collection.IsEmpty())
                {
                    return true;
                }

                HashSet<T> hashSet = other as HashSet<T>;
                if (hashSet.IsNotNull() && HashSet<T>.AreEqualityComparersEqual(this, hashSet) && hashSet.Count > this.Count)
                {
                    return false;
                }
            }

            return ContainsAllElements(other);
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        ///     <c>true</c> if the current set and other share at least one common element; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            if (IsEmpty) { return false; }

            foreach (T item in other)
            {
                if (this.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the current set and the specified collection contain the same elements.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        ///     <c>true</c> if the current set is equal to other; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            HashSet<T> hashSet = other as HashSet<T>;
            if (hashSet.IsNotNull() && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
            {
                return this.Count == hashSet.Count && this.ContainsAllElements(hashSet);
            }

            ICollection<T> collection = other as ICollection<T>;
            if (collection.IsNotNull() && this.IsEmpty && (collection.Count > 0))
            {
                return false;
            }

            HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
            return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount == 0;
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are present 
        /// either in the current set or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            if (IsEmpty)
            {
                UnionWith(other);
                return;
            }

            if (other == this)
            {
                Clear();
                return;
            }

            HashSet<T> hashSet = other as HashSet<T>;
            if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
            {
                this.SymmetricExceptWithUniqueHashSet(hashSet);
                return;
            }

            this.SymmetricExceptWithEnumerable(other);
        }

        /// <summary>
        /// Modifies the current set so that it contains all elements that are present 
        /// in both the current set and in the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public void UnionWith(IEnumerable<T> other)
        {
            if (other.IsNull())
            {
                throw new ArgumentNullException("other");
            }

            foreach (T item in other)
            {
                Add(item);
            }
        }

        #endregion

        #region ICollection<T> Members

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        public void CopyTo(T[] copyTo, int arrayIndex)
        {
            if (array.IsNull()) { throw new ArgumentNullException("array"); }

            int copyLength = Math.Min(this.Count, copyTo.Length - arrayIndex);
            int currentPos = -1;
            for (int i = 0; i < copyLength; i++)
            {
                do
                {
                    currentPos++;
                } while (currentPos < array.Length && !IsActive(currentPos));
                copyTo[arrayIndex + i] = array[currentPos].Item;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion
    }
}
