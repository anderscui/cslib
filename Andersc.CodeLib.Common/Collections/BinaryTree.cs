using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    /// <summary>
    /// A general implementation of binary tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        // TODO: Change this class to abstract, use avl as default?

        #region Properties

        protected BinaryTreeNode<T> root;

        public BinaryTreeNode<T> Root
        {
            get { return root; }
        }

        public bool IsEmpty
        {
            get { return (root == null); }
        }

        public int Size
        {
            get
            {
                return GetNodeSize(root);
            }
        }

        public int Height
        {
            get
            {
                return GetNodeHeight(root);
            }
        }

        #endregion

        #region Constructors

        public BinaryTree()
        {
            root = null;
        }

        public BinaryTree(T rootItem)
        {
            root = new BinaryTreeNode<T>(rootItem);
        }

        #endregion

        public void Clear()
        {
            root = null;
        }

        // T(n) = O(n); S(n) = O(h) (height)
        private IEnumerator<T> GetPreOrderEnumerator()
        {
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            BinaryTreeNode<T> current = root;
            while (current.IsNotNull() || !stack.IsEmpty)
            {
                while (current.IsNotNull())
                {
                    yield return current.Item;
                    //Console.WriteLine("Push {0}", current.Item);
                    stack.Push(current);
                    current = current.Left;
                }
                if(!stack.IsEmpty)
                {
                    current = stack.Pop();
                    current = current.Right;
                }
            }
        }

        public void PreOrderTraverse(Action<T> visitor)
        {
            IEnumerator<T> enumerator = GetPreOrderEnumerator();
            while (enumerator.MoveNext())
            {
                visitor(enumerator.Current);
            }
        }

        // T(n) = O(n); S(n) = O(h) (height)
        private IEnumerator<T> GetInOrderEnumerator()
        {
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            BinaryTreeNode<T> current = root;
            while (current.IsNotNull() || !stack.IsEmpty)
            {
                while (current.IsNotNull())
                {
                    Console.WriteLine("Push {0}", current.Item);
                    stack.Push(current);
                    current = current.Left;
                }
                if (!stack.IsEmpty)
                {
                    current = stack.Pop();
                    yield return current.Item;
                    current = current.Right;
                }
            }
        }

        public void InOrderTraverse(Action<T> visitor)
        {
            IEnumerator<T> enumerator = GetInOrderEnumerator();
            while (enumerator.MoveNext())
            {
                visitor(enumerator.Current);
            }
        }

        //private IEnumerator<T> GetPostOrderEnumerator()
        //{
        //    Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
        //    BinaryTreeNode<T> current = root;
        //    BinaryTreeNode<T> lastVisited = null;

        //    while (current.IsNotNull())
        //    {
        //        for (; current.Left.IsNotNull(); current = current.Left)
        //        {
        //            stack.Push(current);
        //        }

        //        while (current.IsNotNull() && (current.Right.IsNull() || current.Right == lastVisited))
        //        {
        //            yield return current.Item;
        //            lastVisited = current;
        //            if (stack.IsEmpty)
        //            {
        //                yield break;
        //            }
        //            current = stack.Pop();
        //        }
        //        stack.Push(current);
        //        current = current.Right;
        //    }
        //}

        // Best method with T(n) and S(n).

        private IEnumerator<T> GetPostOrderEnumerator()
        {
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            BinaryTreeNode<T> current = root, lastVisited = null;

            while (current.IsNotNull() || stack.Count > 0)
            {
                while (current.IsNotNull())
                {
                    stack.Push(current);
                    current = current.Left;
                }

                if (stack.Count > 0)
                {
                    BinaryTreeNode<T> right = stack.Top.Right;
                    // If the node has not right child, or the right child has been visited;
                    if (right.IsNull() || right == lastVisited)
                    {
                        current = stack.Pop();
                        yield return current.Item;
                        lastVisited = current;
                        current = null;
                    }
                    else
                    {
                        // Now need to visit right sub tree.
                        current = right;
                    }
                }
            }
        }

        // Use two stacks.
        //private IEnumerator<T> GetPostOrderEnumerator()
        //{
        //    Stack<BinaryTreeNode<T>> leftStack = new Stack<BinaryTreeNode<T>>();
        //    Stack<BinaryTreeNode<T>> rightStack = new Stack<BinaryTreeNode<T>>();
        //    BinaryTreeNode<T> current, rightChild;

        //    current = root;
        //    do
        //    {
        //        while (current.IsNotNull())
        //        {
        //            rightChild = current.Right;
        //            leftStack.Push(current);
        //            rightStack.Push(rightChild);
        //            current = current.Left;
        //        }
        //        current = leftStack.Pop();
        //        rightChild = rightStack.Pop();
        //        if (rightChild.IsNull())
        //        {
        //            yield return current.Item;
        //        }
        //        else
        //        {
        //            // Wait a moment, go back to stack.
        //            leftStack.Push(current);
        //            // When this item popped again, we can process directly.
        //            rightStack.Push(null);
        //        }
        //        current = rightChild;
        //    } while (!leftStack.IsEmpty); // Just need to test left stack, because the two stacks push and pop at the same time.
        //}

        public void PostOrderTraverse(Action<T> visitor)
        {
            IEnumerator<T> enumerator = GetPostOrderEnumerator();
            while (enumerator.MoveNext())
            {
                visitor(enumerator.Current);
            }
        }

        private IEnumerator<T> GetLevelOrderEnumerator()
        {
            if (IsEmpty) { yield break; }

            Queue<BinaryTreeNode<T>> q = new Queue<BinaryTreeNode<T>>();
            q.Enqueue(root);

            BinaryTreeNode<T> current = null;
            while (!q.IsEmpty)
            {
                current = q.Dequeue();
                yield return current.Item;

                if (current.Left.IsNotNull())
                {
                    q.Enqueue(current.Left);
                }

                if (current.Right.IsNotNull())
                {
                    q.Enqueue(current.Right);
                }
            }
        }

        public void LevelOrderTraverse(Action<T> visitor)
        {
            IEnumerator<T> enumerator = GetLevelOrderEnumerator();
            while (enumerator.MoveNext())
            {
                visitor(enumerator.Current);
            }
        }

        public static BinaryTree<T> Merge(T rootItem, BinaryTree<T> tree1, BinaryTree<T> tree2)
        {
            // TODO: if tree is null?
            if (tree1.root == tree2.root && tree1.root.IsNotNull())
            {
                throw new InvalidOperationException("Cannot merge the same tree.");
            }

            BinaryTreeNode<T> root = new BinaryTreeNode<T>(rootItem, tree1.root, tree2.root);
            BinaryTree<T> newTree = new BinaryTree<T>();
            newTree.root = root;

            tree1.root = null;
            tree2.root = null;

            return newTree;
        }

        #region Private Methods

        private int GetNodeSize(BinaryTreeNode<T> node)
        {
            if (node.IsNull())
            {
                return 0;
            }

            return 1 + GetNodeSize(node.Left) + GetNodeSize(node.Right);
        }

        private int GetNodeHeight(BinaryTreeNode<T> node)
        {
            if (node.IsNull())
            {
                return -1;
            }

            return 1 + Math.Max(GetNodeHeight(node.Left), GetNodeHeight(node.Right));
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return GetLevelOrderEnumerator();
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
