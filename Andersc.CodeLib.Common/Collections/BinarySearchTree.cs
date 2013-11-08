using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    /// <summary>
    /// The simple implementation of binary search tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// The min item in the tree.
        /// </summary>
        /// <exception cref="Andersc.CodeLib.Common.Collections.CollectionEmptyException">If the tree is empty.</exception>
        public T Min
        {
            get
            {
                if (IsEmpty) { throw new CollectionEmptyException(); }

                return FindMin().Item;
            }
        }

        /// <summary>
        /// The max item in the tree.
        /// </summary>
        /// <exception cref="Andersc.CodeLib.Common.Collections.CollectionEmptyException">If the tree is empty.</exception>
        public T Max
        {
            get
            {
                if (IsEmpty) { throw new CollectionEmptyException(); }

                return FindMax().Item;
            }
        }

        #region Constructors

        public BinarySearchTree() : base() 
        { }

        public BinarySearchTree(T rootItem) : base(rootItem)
        { }

        #endregion

        public void Insert(T item)
        {
            root = Insert(root, item);
        }

        /// <summary>
        /// Inserts a new item to a sub tree.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected BinaryTreeNode<T> Insert(BinaryTreeNode<T> tree, T item)
        {
            // TODO: How to deal with duplicate items?
            if (tree.IsNull())
            {
                tree = new BinaryTreeNode<T>(item);
            }
            else if (item.CompareTo(tree.Item) < 0)
            {
                tree.Left = Insert(tree.Left, item);
            }
            else if (item.CompareTo(tree.Item) > 0)
            {
                tree.Right = Insert(tree.Right, item);
            }
            else
            {
                throw new DuplicateItemException(item.ToString());
            }

            return tree;
        }

        public BinaryTreeNode<T> FindMin()
        {
            return FindMin(root);
        }

        protected BinaryTreeNode<T> FindMin(BinaryTreeNode<T> tree)
        {
            if (tree.IsNotNull())
            {
                while (tree.Left.IsNotNull())
                {
                    tree = tree.Left;
                }
            }

            return tree;
        }

        public BinaryTreeNode<T> FindMax()
        {
            return FindMax(root);
        }

        protected BinaryTreeNode<T> FindMax(BinaryTreeNode<T> tree)
        {
            if (tree.IsNotNull())
            {
                while (tree.Right.IsNotNull())
                {
                    tree = tree.Right;
                }
            }

            return tree;
        }

        public BinaryTreeNode<T> Find(T item)
        {
            return Find(root, item);
        }

        private BinaryTreeNode<T> Find(BinaryTreeNode<T> node, T item)
        {
            BinaryTreeNode<T> current = node;
            while (current.IsNotNull())
            {
                if (current.Item.CompareTo(item) == 0)
                {
                    return current; // Match
                }
                else if (current.Item.CompareTo(item) > 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return null; // Not found.
        }

        public void Remove(T item)
        {
            root = Remove(root, item);
        }

        /// <summary>
        /// Removes a node from a sub tree by its value.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="item"></param>
        /// <returns>The item used to replace the original node.</returns>
        private BinaryTreeNode<T> Remove(BinaryTreeNode<T> tree, T item)
        {
            if (tree.IsNull()) { throw new ElementNotFoundException(); }

            if (item.CompareTo(tree.Item) < 0)
            {
                tree.Left = Remove(tree.Left, item);
            }
            else if (item.CompareTo(tree.Item) > 0)
            {
                tree.Right = Remove(tree.Right, item);
            }
            else if (tree.Left.IsNotNull() && tree.Right.IsNotNull()) // Has two children
            {
                // Replace the removed node with min of right sub tree.
                tree.Item = FindMin(tree.Right).Item;
                // Remove the min node of right sub tree.
                tree.Right = RemoveMin(tree.Right);
            }
            else
            {
                tree = tree.Left.IsNotNull() ? tree.Left : tree.Right;
            }

            return tree;
        }

        public void RemoveMin()
        {
            root = RemoveMin(root);
        }

        /// <summary>
        /// Removes the min node from a sub tree by its value.
        /// </summary>
        /// <param name="tree"></param>
        /// <returns>The item used to replace the original node.</returns>
        protected BinaryTreeNode<T> RemoveMin(BinaryTreeNode<T> tree)
        {
            if (tree.IsNull())
            {
                throw new ElementNotFoundException();
            }
            else if (tree.Left.IsNotNull())
            {
                tree.Left = RemoveMin(tree.Left);
                return tree;
            }
            else
            {
                return tree.Right;
            }
        }
    }
}
