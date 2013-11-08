using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    // TODO: ISerializable
    [Serializable]
    public sealed class DoublyLinkedListNode<T>
    {
        internal DoublyLinkedList<T> list;
        internal DoublyLinkedListNode<T> next;
        internal DoublyLinkedListNode<T> prev;
        internal T item;

        public DoublyLinkedList<T> List
        {
            get
            {
                return list;
            }
        }

        // TODO: change name to Data?
        public T Value
        {
            get
            {
                return item;
            }
        }

        public DoublyLinkedListNode<T> Next
        {
            get
            {
                if (next != null && this.next != list.head)
                {
                    return next;
                }

                return null;
            }
        }

        public DoublyLinkedListNode<T> Previous
        {
            get
            {
                if (prev != null && this != list.head)
                {
                    return this.prev;
                }

                return null;
            }
        }

        public DoublyLinkedListNode(T value)
        {
            item = value;
        }

        internal DoublyLinkedListNode(DoublyLinkedList<T> list, T value)
        {
            this.list = list;
            this.item = value;
        }
    }
}
