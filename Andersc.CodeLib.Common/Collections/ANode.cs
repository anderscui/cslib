using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public class ANode<T> where T : IComparable<T>
    {
        public T Item { get; set; }
        public ANode<T> Left { get; set; }
        public ANode<T> Right { get; set; }
        public int BalanceFactor { get; set; }

        public ANode(T item) : this(item, null, null)
        { }

        public ANode(T item, ANode<T> leftChild, ANode<T>
            rightChild)
        {
            Item = item;
            Left = leftChild;
            Right = rightChild;
        }

        public int Degree
        {
            get
            {
                int degree = 0;
                if (Left.IsNotNull()) { degree++; }
                if (Right.IsNotNull()) { degree++; }

                return degree;
            }
        }

        public bool IsLeaf
        {
            get { return (Left.IsNull() && Right.IsNull()); }
        }
    }
}
