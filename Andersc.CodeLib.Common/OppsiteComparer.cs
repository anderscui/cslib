using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common
{
    public class OppsiteComparer<T> : IComparer<T>
    {
        private IComparer<T> originalComparer = null;

        public OppsiteComparer(IComparer<T> comparer)
        {
            if (comparer.IsNull()) { throw new ArgumentNullException("comparer"); }

            originalComparer = comparer;
        }

        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            return originalComparer.Compare(y, x);
        }

        #endregion
    }
}
