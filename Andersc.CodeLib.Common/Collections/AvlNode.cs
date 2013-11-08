using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    // TODO: Consider how to inheirt from binarySearchNode;
    public class AvlNode<T> where T : IComparable<T>
    {
        public T Item { get; set; }
        public AvlNode<T> Left { get; set; }
        public AvlNode<T> Right { get; set; }
        public int Height { get; set; }

        public int Degree
        {
            get
            {
                int degree = 0;
                if (Left != null) { degree++; }
                if (Right != null) { degree++; }

                return degree;
            }
        }

        public bool IsLeaf
        {
            get { return (Degree == 0); }
        }

        public AvlNode(T item)
            : this(item, null, null)
        { }

        public AvlNode(T item, AvlNode<T> leftChild, AvlNode<T> rightChild)
        {
            Item = item;
            Left = leftChild;
            Right = rightChild;
            Height = 0;
        }
    }
}
