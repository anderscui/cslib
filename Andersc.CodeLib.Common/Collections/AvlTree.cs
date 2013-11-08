using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    // TODO: How to implement inheritance from BinaryTree or BST?
    public class AvlTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public AvlNode<T> Root { get; set; }

        public bool IsEmpty
        {
            get { return (Root.IsNull()); }
        }

        public int Size
        {
            get { return GetNodeSize(Root); }
        }

        public int Height
        {
            get
            {
                if (IsEmpty)
                {
                    return -1;
                }
                else
                {
                    return Root.Height;
                }
            }
        }

        public AvlTree()
        {
            Root = null;
        }

        public AvlTree(T rootItem)
        {
            Root = new AvlNode<T>(rootItem);
        }

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

        public void Insert(T item)
        {
            Root = Insert(Root, item);
        }

        private AvlNode<T> Insert(AvlNode<T> node, T item)
        {
            if (node == null)
            {
                // If the tree is empty, create the root node.
                node = new AvlNode<T>(item, null, null);
            }
            else if (item.CompareTo(node.Item) < 0)
            {
                node.Left = Insert(node.Left, item);
                if (GetNodeHeight(node.Left) - GetNodeHeight(node.Right) == 2)
                    if (item.CompareTo(node.Left.Item) < 0)
                        // LL
                        node = RotateWithLeftChild(node);
                    else
                        // LR
                        node = DoubleWithLeftChild(node);
            }
            else if (item.CompareTo(node.Item) > 0)
            {
                node.Right = Insert(node.Right, item);
                if (GetNodeHeight(node.Right) - GetNodeHeight(node.Left) == 2)
                    if (item.CompareTo(node.Right.Item) > 0)
                        // RR
                        node = RotateWithRightChild(node);
                    else
                        // RL
                        node = DoubleWithRightChild(node);
            }
            else
            {
                throw new DuplicateItemException();
            }

            node.Height = Math.Max(GetNodeHeight(node.Left), GetNodeHeight(node.Right)) + 1;

            return node;
        }

        public void Remove(T item)
        {
            // TODO: Impl
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Root = null;
        }

        #region Find Methods

        public bool Contains(T item)
        {
            return Find(item).IsNotNull();
        }

        public AvlNode<T> Find(T item)
        {
            return Find(Root, item);
        }

        private AvlNode<T> Find(AvlNode<T> node, T item)
        {
            AvlNode<T> current = node;
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

        public AvlNode<T> FindMin()
        {
            return FindMin(Root);
        }

        protected AvlNode<T> FindMin(AvlNode<T> tree)
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

        public AvlNode<T> FindMax()
        {
            return FindMax(Root);
        }

        protected AvlNode<T> FindMax(AvlNode<T> tree)
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

        #endregion

        #region Traverse Methods

        // T(n) = O(n); S(n) = O(h) (height)
        private IEnumerator<T> GetPreOrderEnumerator()
        {
            Stack<AvlNode<T>> stack = new Stack<AvlNode<T>>();
            AvlNode<T> current = Root;
            while (current.IsNotNull() || !stack.IsEmpty)
            {
                if (current.IsNotNull())
                {
                    yield return current.Item;
                    stack.Push(current);
                    current = current.Left;
                }
                else
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
            Stack<AvlNode<T>> stack = new Stack<AvlNode<T>>();
            AvlNode<T> current = Root;
            while (current.IsNotNull() || !stack.IsEmpty)
            {
                while (current.IsNotNull())
                {
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

        private IEnumerator<T> GetPostOrderEnumerator()
        {
            Stack<AvlNode<T>> stack = new Stack<AvlNode<T>>();
            AvlNode<T> current = Root;
            AvlNode<T> lastVisited = null;

            while (current.IsNotNull())
            {
                for (; current.Left.IsNotNull(); current = current.Left)
                {
                    stack.Push(current);
                }

                while (current.IsNotNull() && (current.Right.IsNull() || current.Right == lastVisited))
                {
                    yield return current.Item;
                    lastVisited = current;
                    if (stack.IsEmpty)
                    {
                        yield break;
                    }
                    current = stack.Pop();
                }
                stack.Push(current);
                current = current.Right;
            }
        }

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

            Queue<AvlNode<T>> q = new Queue<AvlNode<T>>();
            q.Enqueue(Root);

            AvlNode<T> current = null;
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

        #endregion

        #region Rotate Methods

        private static AvlNode<T> RotateWithLeftChild(AvlNode<T> k2)
        {
            AvlNode<T> k1 = k2.Left;
            k2.Left = k1.Right;
            k1.Right = k2;
            k2.Height = Math.Max(GetNodeHeight(k2.Left), GetNodeHeight(k2.Right)) + 1;
            k1.Height = Math.Max(GetNodeHeight(k1.Left), k2.Height) + 1;

            return k1;
        }

        private static AvlNode<T> RotateWithRightChild(AvlNode<T> k1)
        {
            AvlNode<T> k2 = k1.Right;
            k1.Right = k2.Left;
            k2.Left = k1;
            k1.Height = Math.Max(GetNodeHeight(k1.Left), GetNodeHeight(k1.Right)) + 1;
            k2.Height = Math.Max(GetNodeHeight(k2.Right), k1.Height) + 1;

            return k2;
        }

        private static AvlNode<T> DoubleWithLeftChild(AvlNode<T> k3)
        {
            k3.Left = RotateWithRightChild(k3.Left);
            return RotateWithLeftChild(k3);
        }

        private static AvlNode<T> DoubleWithRightChild(AvlNode<T> k1)
        {
            k1.Right = RotateWithLeftChild(k1.Right);
            return RotateWithRightChild(k1);
        }

        #endregion

        #region Private Methods

        private bool IsRoot(AvlNode<T> node)
        {
            return (Root.Item.CompareTo(node.Item) == 0);
        }

        private int GetNodeSize(AvlNode<T> node)
        {
            if (node.IsNull())
            {
                return 0;
            }

            return 1 + GetNodeSize(node.Left) + GetNodeSize(node.Right);
        }

        private static int GetNodeHeight(AvlNode<T> node)
        {
            return node.IsNull() ? -1 : node.Height;
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
