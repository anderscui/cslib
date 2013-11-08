using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public sealed class SinglyLinkedListNode<T>
    {
        public SinglyLinkedList<T> List { get; private set; }
        public T Data { get; set; }
        public SinglyLinkedListNode<T> Next { get; set; }

        public SinglyLinkedListNode(SinglyLinkedList<T> list, T data, SinglyLinkedListNode<T> next)
        {
            List = list;
            Data = data;
            Next = next;
        }

        public void InsertAfter(T item)
        {
            Next = new SinglyLinkedListNode<T>(List, item, Next);
            if (List.Tail == this)
            {
                List.Tail = Next;
            }
        }

        public void InsertBefore(T item)
        {
            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(List, item, this);

            if (List.Head == this)
            {
                List.Head = newNode;
            }
            else
            {
                SinglyLinkedListNode<T> previous = List.Head;
                while (previous.IsNotNull() && previous.Next != this)
                {
                    previous = previous.Next;
                }
                previous.Next = newNode;
            }
        }
    }
}
