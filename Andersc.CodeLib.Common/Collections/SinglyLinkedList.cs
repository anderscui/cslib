using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        protected SinglyLinkedListNode<T> head;
        protected SinglyLinkedListNode<T> tail;

        public SinglyLinkedListNode<T> Head
        {
            get { return head; }
            set { head = value; }
        }

        public SinglyLinkedListNode<T> Tail
        {
            get { return tail; }
            set { tail = value; }
        }

        public T First
        {
            get
            {
                if (IsEmpty)
                {
                    throw new CollectionEmptyException();
                }

                return head.Data;
            }
        }

        public T Last
        {
            get
            {
                if (IsEmpty)
                {
                    throw new CollectionEmptyException();
                }

                return tail.Data;
            }
        }

        public bool IsEmpty
        {
            get { return (head == null); }
        }

        public int Count
        {
            get { return this.Count(); }
        }

        public void Prepend(T item)
        {
            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(this, item, head);
            if (IsEmpty)
            {
                tail = newNode;
            }
            head = newNode;
        }

        public void Append(T item)
        {
            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(this, item, null);

            if (IsEmpty)
            {
                head = newNode;
            }
            else
            {
                tail.Next = newNode;
            }

            tail = newNode;
        }

        public void Remove(T item)
        {
            // TODO: 
            if (IsEmpty) { throw new CollectionEmptyException(); }

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            SinglyLinkedListNode<T> current = head;
            SinglyLinkedListNode<T> previous = null;
            while (current.IsNotNull() 
                && !comparer.Equals(current.Data, item))
            {
                previous = current;
                current = current.Next;
            }

            if (current.IsNull()) { throw new ElementNotFoundException(); }

            if (current == head)
            {
                head = current.Next;
            }
            else
            {
                previous.Next = current.Next;
            }

            if (current == tail)
            {
                tail = previous;
            }
        }

        public void Clear()
        {
            head = tail = null;
        }

        public SinglyLinkedListNode<T> Find(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            SinglyLinkedListNode<T> current = head;
            while (current.IsNotNull() 
                && !comparer.Equals(current.Data, item))
            {
                current = current.Next;
            }

            return current;
        }

        public void CopyFrom(SinglyLinkedList<T> from)
        {
            if (from == this) { return; }

            Clear();
            SinglyLinkedListNode<T> current = from.head;
            while (current.IsNotNull())
            {
                Append(current.Data);
                current = current.Next;
            }
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            SinglyLinkedListNode<T> iter = head;
            while (iter.IsNotNull())
            {
                yield return iter.Data;
                iter = iter.Next;
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
