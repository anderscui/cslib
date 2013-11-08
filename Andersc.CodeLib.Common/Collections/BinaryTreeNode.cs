using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    // TODO: IComparable or other interfaces?
    public class BinaryTreeNode<T> where T : IComparable<T>
    {
        public T Item { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T item) : this(item, null, null)
        { }

        public BinaryTreeNode(T item, BinaryTreeNode<T> leftChild, BinaryTreeNode<T> rightChild)
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
            // Or, Degree == 0;
            get { return (Left.IsNull() && Right.IsNull()); }
        }

        public BinaryTreeNode<T> Duplicate()
        {
            BinaryTreeNode<T> root = new BinaryTreeNode<T>(Item, null, null);
            if (Left.IsNotNull())
            {
                root.Left = Left.Duplicate();
            }
            if (Right.IsNotNull())
            {
                root.Right = Right.Duplicate();
            }

            return root;
        }
    }
}
