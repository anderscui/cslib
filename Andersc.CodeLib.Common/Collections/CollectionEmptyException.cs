using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    // TODO: Refactor this.
    [Serializable]
    public class CollectionEmptyException : Exception
    {
        public CollectionEmptyException() { }
        public CollectionEmptyException(string message) : base(message) { }
        public CollectionEmptyException(string message, Exception inner) : base(message, inner) { }
    }
}
