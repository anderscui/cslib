using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public abstract class AbstractContainer<T> : IContainer<T>, IEnumerable<T>
    {
        #region Impl of IContainer<T>

        public virtual int Count
        {
            get { return 0; }
        }

        public virtual bool IsEmpty
        {
            get { return (Count == 0); }
        }

        public virtual bool IsFull
        {
            get { return false; }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Impl of IEnumerable<T>

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Impl of IEnumerable

        // TODO: Need other impls?
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
