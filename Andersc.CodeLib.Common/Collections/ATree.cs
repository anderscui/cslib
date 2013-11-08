using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    // TODO: Use this as the default avl tree.
    public class ATree<T> where T : IComparable<T>
    {
        private ANode<T> _root;

        /// <summary>
        /// Tracks the complete path of the newly added node or deleted one.
        /// </summary>
        private ANode<T>[] path = new ANode<T>[32];
        /// <summary>
        /// Tracks the complete path of the newly added node or deleted one with path array.
        /// </summary>
        private int p;

        public ANode<T> Root
        {
            get { return _root; }
        }

        public bool IsEmpty
        {
            get { return (_root.IsNull()); }
        }

        public bool Add(T value)
        {
            if (IsEmpty)
            {
                // here just create the root node.
                _root = new ANode<T>(value);
                _root.BalanceFactor = 0;

                return true;
            }

            p = 0;
            ANode<T> current = _root, prev = null;
            while (current.IsNotNull())
            {
                path[p++] = current;
                if (current.Item.CompareTo(value) == 0)
                {
                    // item exists.
                    return false;
                }
                prev = current;
                current = (value.CompareTo(current.Item) < 0) ? current.Left : current.Right;
            }

            // 
            current = new ANode<T>(value);
            current.BalanceFactor = 0;
            if (value.CompareTo(prev.Item) < 0)
            {
                prev.Left = current;
            }
            else
            {
                prev.Right = current;
            }
            path[p] = current;

            int bf = 0;
            while (p > 0)
            {
                // compare to node's parent.
                bf = (value.CompareTo(path[p - 1].Item) < 0) ? 1 : -1;
                path[--p].BalanceFactor += bf; // change bf of parent node.
                bf = path[p].BalanceFactor;
                if (bf == 0)
                {
                    // here means the tree is balanced.
                    return true;
                }
                else if (Math.Abs(bf) == 2)
                {
                    // Balance mimimum unbalanced tree.
                    RotateSubTree(bf);
                    return true;
                }
            }

            return true;
        }

        public bool Remove(T value)
        {
            p = -1;
            ANode<T> node = _root;
            while (node.IsNotNull())
            {
                path[++p] = node;
                if (value.CompareTo(node.Item) == 0)
                {
                    RemoveNode(node);
                    return true;
                }

                if (value.CompareTo(node.Item) < 0)
                {
                    node = node.Left;
                }
                else
                {
                    node = node.Right;
                }
            }

            return false;
        }

        private void RemoveNode(ANode<T> node)
        {
            ANode<T> tmp = null;
            if (node.Degree == 2)
            {
                tmp = node.Left;
                path[++p] = tmp;
                // Find max node of left sub tree as the new root.
                while (tmp.Right.IsNotNull())
                {
                    tmp = tmp.Right;
                    path[++p] = tmp;
                }
                node.Item = tmp.Item;
                if (path[p - 1] == node)
                {
                    path[p - 1].Left = tmp.Left;
                }
                else
                {
                    path[p - 1].Right = tmp.Left;
                }
            }
            else
            {
                tmp = node.Left;
                if (tmp.IsNull())
                {
                    tmp = node.Right;
                }
                if (p > 0)
                {
                    if (path[p - 1].Left == node)
                    {
                        path[p - 1].Left = tmp;
                    }
                    else
                    {
                        path[p - 1].Right = tmp;
                    }
                }
                else
                {
                    _root = tmp;
                }
            }

            // p points to the deleted node.
            while (p > 0)
            {
                int bf = (node.Item.CompareTo(path[p - 1].Item) < 0) ? -1 : 1;
                path[--p].BalanceFactor += bf;
                bf = path[p].BalanceFactor;
                if (bf != 0)
                {
                    // if abs(bf) is 1 or the height is not changed after rotation, stop backtracking.
                    if (bf == 1 || bf == -1 || !RotateSubTree(bf))
                    {
                        break;
                    }
                }
            }
        }

        #region Rotation Methods

        /// <summary>
        /// Rotates a sub tree, tree root is tracked by path array.
        /// </summary>
        /// <param name="balanceFactor">Balance factor of sub tree.</param>
        /// <returns>Whether the sub tree's height is changed.</returns>
        private bool RotateSubTree(int balanceFactor)
        {
            bool heightChanged = true;
            ANode<T> root = path[p];
            ANode<T> newRoot = null;
            if (balanceFactor == 2) // new node in left sub tree.
            {
                int leftBF = root.Left.BalanceFactor;
                if (leftBF == -1)
                {
                    newRoot = LR(root);
                }
                else if (leftBF == 1)
                {
                    newRoot = LL(root);
                }
                else
                {
                    // bf == 0, only for deletion
                    newRoot = LL(root);
                    heightChanged = false;
                }
            }

            if (balanceFactor == -2)
            {
                int rightBF = root.Right.BalanceFactor;
                if (rightBF == 1)
                {
                    newRoot = RL(root);
                }
                else if (rightBF == -1)
                {
                    newRoot = RR(root);
                }
                else
                {
                    // bf == 0, only for deletion
                    newRoot = RR(root);
                    heightChanged = false;
                }
            }

            // Change node relation for sub tree's parent node.
            if (p > 0)
            {
                if (root.Item.CompareTo(path[p - 1].Item) < 0)
                {
                    path[p - 1].Left = newRoot;
                }
                else
                {
                    path[p - 1].Right = newRoot;
                }
            }
            else
            {
                _root = newRoot;
            }

            return heightChanged;
        }

        private ANode<T> LL(ANode<T> root)
        {
            ANode<T> newRoot = root.Left;
            root.Left = newRoot.Right;
            newRoot.Right = root;
            if (newRoot.BalanceFactor == 1)
            {
                // For insertion
                root.BalanceFactor = 0;
                newRoot.BalanceFactor = 0;
            }
            else
            {
                // For deletion
                root.BalanceFactor = 1;
                newRoot.BalanceFactor = -1;
            }

            return newRoot;
        }

        private ANode<T> RR(ANode<T> root)
        {
            ANode<T> newRoot = root.Right;
            root.Right = newRoot.Left;
            newRoot.Left = root;
            if (newRoot.BalanceFactor == -1)
            {
                // For insertion
                root.BalanceFactor = 0;
                newRoot.BalanceFactor = 0;
            }
            else
            {
                // For deletion
                root.BalanceFactor = -1;
                newRoot.BalanceFactor = 1;
            }

            return newRoot;
        }

        private ANode<T> LR(ANode<T> root)
        {
            // TODO: 
            //root.Left = RR(root.Left);
            //return LL(root);
            ANode<T> rootLeft = root.Left;
            ANode<T> newRoot = rootLeft.Right;
            root.Left = newRoot.Right;
            rootLeft.Right = newRoot.Left;
            newRoot.Left = rootLeft;
            newRoot.Right = root;
            switch (newRoot.BalanceFactor)
            {
                case 0:
                    root.BalanceFactor = 0;
                    rootLeft.BalanceFactor = 0;
                    break;
                case 1:
                    root.BalanceFactor = -1;
                    rootLeft.BalanceFactor = 0;
                    break;
                case -1:
                    root.BalanceFactor = 0;
                    rootLeft.BalanceFactor = 1;
                    break;
            }
            newRoot.BalanceFactor = 0;
            return newRoot;
        }

        private ANode<T> RL(ANode<T> root)
        {
            // TODO: 
            //root.Right = LL(root.Right);
            //return RR(root);
            ANode<T> rootNext = root.Right;
            ANode<T> newRoot = rootNext.Left;
            root.Right = newRoot.Left;
            rootNext.Left = newRoot.Right;
            newRoot.Right = rootNext;
            newRoot.Left = root;
            switch (newRoot.BalanceFactor)
            {
                case 0:
                    root.BalanceFactor = 0;
                    rootNext.BalanceFactor = 0;
                    break;
                case 1:
                    root.BalanceFactor = 0;
                    rootNext.BalanceFactor = -1;
                    break;
                case -1:
                    root.BalanceFactor = 1;
                    rootNext.BalanceFactor = 0;
                    break;
            }
            newRoot.BalanceFactor = 0;
            return newRoot;
        }

        #endregion

        // TODO: replace this with enumerator.
        public void Print()
        {
            Print(_root);
            Console.WriteLine();
        }

        private void Print(ANode<T> root)
        {
            if (root.IsNotNull())
            {
                Print(root.Left);
                Console.Write(root.Item + " ");
                Print(root.Right);
            }
        }
    }
}
