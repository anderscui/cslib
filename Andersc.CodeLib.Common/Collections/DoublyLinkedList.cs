using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public class DoublyLinkedList<T> : ICollection<T>, IEnumerable<T>
    {
        internal DoublyLinkedListNode<T> head;
        internal int size;

        public int Count
        {
            get { return size; }
        }

        public bool IsEmpty
        {
            get { return (size == 0); }
        }

        public DoublyLinkedListNode<T> First
        {
            get { return head; }
        }

        public DoublyLinkedListNode<T> Last
        {
            get
            {
                if (head != null)
                {
                    return head.prev;
                }

                return null;
            }
        }

        public DoublyLinkedList()
        {
        }

        public DoublyLinkedList(IEnumerable<T> enumerable)
        {
            if (enumerable.IsNull())
            {
                throw new ArgumentNullException("enumerable");
            }

            foreach (T item in enumerable)
            {
                Prepend(item);
            }
        }

        public DoublyLinkedListNode<T> AddAfter(DoublyLinkedListNode<T> node, T item)
        {
            ValidateNode(node);

            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(this, item);
            InsertNodeBefore(node.next, newNode);

            return newNode;
        }

        public void AddAfter(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNewNode(newNode);
            InsertNodeBefore(node.next, newNode);
            newNode.list = this;
        }

        public DoublyLinkedListNode<T> AddBefore(DoublyLinkedListNode<T> node, T item)
        {
            ValidateNode(node);

            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(this, item);
            InsertNodeBefore(node, newNode);
            if (node == head)
            {
                head = newNode;
            }

            return newNode;
        }

        public void AddBefore(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNewNode(newNode);
            InsertNodeBefore(node, newNode);
            newNode.list = this;
            if (node == head)
            {
                head = newNode;
            }
        }

        public DoublyLinkedListNode<T> Prepend(T item)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(item);
            if (IsEmpty)
            {
                InsertNodeIntoEmptyList(newNode);
            }
            else
            {
                InsertNodeBefore(head, newNode);
                head = newNode;
            }

            return newNode;
        }

        public void Prepend(DoublyLinkedListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            if (IsEmpty)
            {
                InsertNodeIntoEmptyList(newNode);
            }
            else
            {
                InsertNodeBefore(head, newNode);
                head = newNode;
            }

            newNode.list = this;
        }

        public DoublyLinkedListNode<T> Append(T item)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(item);
            if (IsEmpty)
            {
                InsertNodeIntoEmptyList(newNode);
            }
            else
            {
                InsertNodeBefore(head, newNode);
            }

            return newNode;
        }

        public void Append(DoublyLinkedListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            if (IsEmpty)
            {
                InsertNodeIntoEmptyList(newNode);
            }
            else
            {
                InsertNodeBefore(head, newNode);
            }

            newNode.list = this;
        }

        public void Clear()
        {
            DoublyLinkedListNode<T> current = head;
            while (current != null)
            {
                DoublyLinkedListNode<T> node = current;
                current = current.next;
                InvalidateNode(node);
            }

            head = null;
            size = 0;
        }

        public bool Contains(T value)
        {
            return (Find(value) != null);
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (index < 0 || index > array.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (array.Length - index < size)
            {
                throw new ArgumentException("Insufficient space.");
            }
            DoublyLinkedListNode<T> current = this.head;
            if (current != null)
            {
                do
                {
                    array[index++] = current.item;
                    current = current.next;
                }
                while (current != this.head);
            }
        }

        public DoublyLinkedListNode<T> Find(T value)
        {
            DoublyLinkedListNode<T> current = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            while (current != null && !comparer.Equals(current.item, value))
            {
                current = current.next;
                if (current == head)
                {
                    return null;
                }
            }
            return current;
        }

        public DoublyLinkedListNode<T> FindLast(T value)
        {
            if (IsEmpty)
            {
                return null;
            }

            DoublyLinkedListNode<T> current = head.prev;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            while (current != null && !comparer.Equals(current.item, value))
            {
                current = current.prev;
                if (current == head)
                {
                    return null;
                }
            }
            return current;
        }

        public bool Remove(T item)
        {
            DoublyLinkedListNode<T> toBeRemoved = Find(item);
            if (toBeRemoved != null)
            {
                RemoveNode(toBeRemoved);
                return true;
            }
            return false;
        }

        public void Remove(DoublyLinkedListNode<T> node)
        {
            ValidateNode(node);
            RemoveNode(node);
        }

        public void RemoveFirst()
        {
            if (IsEmpty)
            {
                throw new CollectionEmptyException();
            }
            RemoveNode(head);
        }

        public void RemoveLast()
        {
            if (IsEmpty)
            {
                throw new CollectionEmptyException();
            }
            RemoveNode(head.prev);
        }

        private void ValidateNode(DoublyLinkedListNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node.list != this)
            {
                throw new InvalidOperationException("External linked list node.");
            }
        }

        private void ValidateNewNode(DoublyLinkedListNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node.list != null)
            {
                throw new InvalidOperationException("LinkedList node is attached.");
            }
        }

        private void RemoveNode(DoublyLinkedListNode<T> node)
        {
            if (node.next == node)
            {
                head = null;
            }
            else
            {
                node.next.prev = node.prev;
                node.prev.next = node.next;
                if (head == node)
                {
                    head = node.next;
                }
            }
            InvalidateNode(node);
            size--;
        }

        private void InsertNodeIntoEmptyList(DoublyLinkedListNode<T> newNode)
        {
            newNode.next = newNode;
            newNode.prev = newNode;
            head = newNode;

            size++;
        }

        private void InsertNodeBefore(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> newNode)
        {
            newNode.next = node;
            newNode.prev = node.prev;
            node.prev.next = newNode;
            node.prev = newNode;
            this.size++;
        }

        private void InvalidateNode(DoublyLinkedListNode<T> node)
        {
            node.list = null;
            node.prev = null;
            node.next = null;
        }

        void ICollection<T>.Add(T item)
        {
            Append(item);
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        #region IEnumerable<T> Impl

        public IEnumerator<T> GetEnumerator()
        {
            DoublyLinkedListNode<T> current = head;
            while (current != null)
            {
                yield return current.item;
                
                current = current.next;
                if (current == head)
                {
                    yield break;
                }
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
