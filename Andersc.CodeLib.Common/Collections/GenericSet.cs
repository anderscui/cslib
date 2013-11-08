using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public class GenericSet<T>
    {
        private Dictionary<T, T> data;

        public GenericSet()
        {
            data = new Dictionary<T, T>();
        }

        public void Add(T item)
        {
            if (!Contains(item))
            {
                data.Add(item, item);
            }
        }

        public void Remove(T item)
        {
            data.Remove(item);
        }

        public int Size
        {
            get { return data.Count; }
        }

        public bool IsEmpty
        {
            get { return (Size == 0); }
        }

        public bool Contains(T item)
        {
            return data.ContainsKey(item);
        }

        public GenericSet<T> Union(GenericSet<T> other)
        {
            GenericSet<T> newSet = new GenericSet<T>();
            newSet.AddItemsFromSet(this);
            newSet.AddItemsFromSet(other);

            return newSet;
        }

        public GenericSet<T> Intersection(GenericSet<T> other)
        {
            GenericSet<T> newSet = new GenericSet<T>();
            
            if (other.IsNull()) { return null; }

            foreach (T key in data.Keys)
            {
                if (other.Contains(key))
                {
                    newSet.Add(key);
                }
            }

            return newSet;
        }

        public bool IsSubset(GenericSet<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            if (this.Size > other.Size)
            {
                return false;
            }
            return data.Keys.All(item => other.Contains(item));
        }

        public GenericSet<T> Difference(GenericSet<T> other)
        {
            if (other.IsNull()) { throw new ArgumentNullException("other"); }

            GenericSet<T> newSet = new GenericSet<T>();
            foreach (T item in data.Keys)
            {
                if (!other.Contains(item))
                {
                    newSet.Add(item);
                }
            }

            return newSet;
        }

        private void AddItemsFromSet(GenericSet<T> other)
        {
            if (other.IsNotNull())
            {
                foreach (T item in other.data.Keys)
                {
                    if (!Contains(item))
                    {
                        Add(item);
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in data.Keys)
            {
                sb.AppendFormat("{0}, ", item);
            }

            string result = sb.ToString();
            if (!IsEmpty)
            {
                result = result.Remove(result.Length - 2, 2);
            }

            return String.Format("{{{0}}}", result);
        }

        public override bool Equals(object obj)
        {
            if (obj.IsNull())
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            GenericSet<T> other = obj as GenericSet<T>;
            return this.IsSubset(other) && other.IsSubset(this);
        }

        public static bool operator ==(GenericSet<T> gs1, GenericSet<T> gs2)
        {
            if (gs1.IsNull()) { return false; }
            return gs1.Equals(gs2);
        }

        public static bool operator !=(GenericSet<T> gs1, GenericSet<T> gs2)
        {
            return !(gs1 == gs2);
        }
    }
}
