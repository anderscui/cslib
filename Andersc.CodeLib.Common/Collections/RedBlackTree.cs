using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    // TODO: Consistent interface for all trees; ITreeNode and ITree?
    // TODO: Rename to RedBlackTreeNode
    public class RBNode<T> where T : IComparable<T>
    {
        public T Item { get; set; }
        public RBNode<T> Left { get; set; }
        public RBNode<T> Right { get; set; }

        public bool IsRed { get; set; }
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

        public RBNode(T item) : this(item, null, null)
        { }

        public RBNode(T item, RBNode<T> leftChild, RBNode<T> rightChild)
        {
            Item = item;
            Left = leftChild;
            Right = rightChild;
        }
    }

    // TODO: Add head node constructor;
    public class RedBlackTree<T> where T : IComparable<T>
    {
        private static readonly int DefaultPathLength = 32;

        // TODO: -> _root;
        private RBNode<T> _head;
        // TODO: Comments
        private RBNode<T>[] path = new RBNode<T>[DefaultPathLength];
        private int p;

        public RBNode<T> Root
        {
            get { return _head; }
        }

        public bool IsEmpty
        {
            get { return (_head.IsNull()); }
        }

        public bool Add(T value)
        {
            // for an empty tree, just create a new root node.
            if (IsEmpty)
            {
                _head = new RBNode<T>(value);
                _head.IsRed = false;

                return true;
            }

            p = 0;
            RBNode<T> parent = null, current = _head;
            while (current.IsNotNull())
            {
                path[p++] = current;
                if (current.Item.CompareTo(value) == 0)
                {
                    // item exists.
                    return false;
                }
                parent = current;
                current = (value.CompareTo(parent.Item) < 0) ? parent.Left : parent.Right;
            }

            // all the new nodes should be RED.
            current = new RBNode<T>(value);
            current.IsRed = true;
            if (value.CompareTo(parent.Item) < 0)
            {
                parent.Left = current;
            }
            else
            {
                parent.Right = current;
            }

            if (!parent.IsRed)
            {
                return true;
            }

            path[p] = current; // get the complete path.
            while ((p -= 2) >= 0) // check grand parent node.
            {
                RBNode<T> grandParent = path[p];
                parent = path[p + 1];
                if (!parent.IsRed)
                {
                    break;
                }
                RBNode<T> uncle = (grandParent.Left == parent) ? grandParent.Right : grandParent.Left;
                current = path[p + 2];
                if (IsRed(uncle))
                {
                    parent.IsRed = false;
                    uncle.IsRed = false;
                    if (p > 0)
                    {
                        grandParent.IsRed = true;
                    }
                }
                else
                {
                    // if uncle is null or black.
                    RBNode<T> newRoot;
                    if (grandParent.Left == parent)
                    {
                        newRoot = (parent.Left == current) ? LL(grandParent) : LR(grandParent);
                    }
                    else
                    {
                        newRoot = (parent.Right == current) ? RR(grandParent) : RL(grandParent);
                    }
                    grandParent.IsRed = true;
                    newRoot.IsRed = false;
                    ReplaceChildOfNodeOrRoot((p > 0) ? path[p - 1] : null, grandParent, newRoot);

                    return true;
                }
            }

            return true;
        }

        public bool Remove(T value)
        {
            p = -1;
            RBNode<T> node = _head;
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

            // value not found.
            return false;
        }

        private void RemoveNode(RBNode<T> node)
        {
            RBNode<T> tmp = null; // after the below step, tmp points to the actual node to be deleted;
            if (node.Degree == 2)
            {
                tmp = node.Left;
                path[++p] = tmp;
                while (tmp.Right != null)
                {
                    tmp = tmp.Right;
                    path[++p] = tmp;
                }
                node.Item = tmp.Item;
            }
            else
            {
                tmp = node;
            }

            RBNode<T> newTmp = tmp.Left;
            if (newTmp.IsNull())
            {
                newTmp = tmp.Right;
            }

            if (p > 0)
            {
                RBNode<T> parent = path[p - 1];
                if (parent.Left == tmp)
                {
                    parent.Left = newTmp;
                }
                else
                {
                    parent.Right = newTmp;
                }

                // if the deleted is black and its single child is red.
                if (!tmp.IsRed && IsRed(newTmp))
                {
                    newTmp.IsRed = false;
                    return;
                }
            }
            else
            {
                // if delete the root node.
                _head = newTmp;
                if (_head.IsNotNull())
                {
                    _head.IsRed = false;
                }
                return;
            }

            path[p] = newTmp;
            if (IsRed(tmp))
            {
                // if the deleted is red, directly return.
                return;
            }

            // the deleted and its child are both BLACK.
            while (p > 0)
            {
                RBNode<T> current = path[p];
                RBNode<T> parent = path[p - 1];
                bool currentIsLeft = (parent.Left == current);
                RBNode<T> sibling = currentIsLeft ? parent.Right : parent.Left;
                if (IsRed(sibling))
                {
                    RBNode<T> newRoot;
                    if (currentIsLeft)
                    {
                        newRoot = RR(parent);
                    }
                    else
                    {
                        newRoot = LL(parent);
                    }
                    ReplaceChildOfNodeOrRoot(p > 1 ? path[p - 2] : null, parent, newRoot);
                    sibling.IsRed = false;
                    parent.IsRed = true;

                    path[p - 1] = newRoot;
                    path[p] = parent;
                    path[++p] = current;
                }
                else
                {
                    // black sibling && black sibling-left && black sibling-right
                    if (IsNullOrBlack(sibling.Left) && IsNullOrBlack(sibling.Right))
                    {
                        if (parent.IsRed)
                        {
                            parent.IsRed = false;
                            sibling.IsRed = true;
                            if (current.IsNotNull())
                            {
                                // TODO: not needed?
                                current.IsRed = false;
                            }
                            break;
                        }
                        else
                        {
                            // current must be black?
                            parent.IsRed = IsRed(current);
                            if (current.IsNotNull())
                            {
                                current.IsRed = false;
                            }
                            sibling.IsRed = true;
                            p--; // continue to backtrack.
                        }
                    }
                    else
                    {
                        RBNode<T> newRoot;
                        if (currentIsLeft)
                        {
                            if (IsRed(sibling.Right))
                            {
                                newRoot = RR(parent);
                                sibling.Right.IsRed = false;
                            }
                            else
                            {
                                newRoot = RL(parent);
                            }
                        }
                        else
                        {
                            if (IsRed(sibling.Left))
                            {
                                newRoot = LL(parent);
                                sibling.Left.IsRed = false;
                            }
                            else
                            {
                                newRoot = LR(parent);
                            }
                        }

                        if (current.IsNotNull())
                        {
                            current.IsRed = false;
                        }
                        newRoot.IsRed = parent.IsRed;
                        parent.IsRed = false;
                        ReplaceChildOfNodeOrRoot(p > 1 ? path[p - 2] : null, parent, newRoot);
                        break;
                    }
                }
            }
        }

        private RBNode<T> LL(RBNode<T> root)
        {
            RBNode<T> left = root.Left;
            root.Left = left.Right;
            left.Right = root;

            return left;
        }

        private RBNode<T> LR(RBNode<T> root)
        {
            RBNode<T> left = root.Left;
            RBNode<T> right = left.Right;
            root.Left = right.Right;
            right.Right = root;
            left.Right = right.Left;
            right.Left = left;

            return right;
        }

        private RBNode<T> RR(RBNode<T> root)
        {
            RBNode<T> right = root.Right;
            root.Right = right.Left;
            right.Left = root;

            return right;
        }

        private RBNode<T> RL(RBNode<T> root)
        {
            RBNode<T> right = root.Right;
            RBNode<T> left = right.Left;
            root.Right = left.Left;
            left.Left = root;
            right.Left = left.Right;
            left.Right = right;

            return left;
        }

        private void ReplaceChildOfNodeOrRoot(RBNode<T> parent, RBNode<T> child, RBNode<T> newChild)
        {
            if (parent.IsNotNull())
            {
                if (parent.Left == child)
                {
                    parent.Left = newChild;
                }
                else
                {
                    parent.Right = newChild;
                }
            }
            else
            {
                _head = newChild;
            }
        }

        private bool IsRed(RBNode<T> uncle)
        {
            return (uncle.IsNotNull() && uncle.IsRed);
        }

        private bool IsBlack(RBNode<T> node)
        {
            return (node.IsNotNull() && !node.IsRed);
        }

        private bool IsNullOrBlack(RBNode<T> node)
        {
            return (node.IsNull() || !node.IsRed);
        }

        // TODO: replace this with enumerator.
        public void Print()
        {
            Print(_head);
            Console.WriteLine();
        }

        private void Print(RBNode<T> root)
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
