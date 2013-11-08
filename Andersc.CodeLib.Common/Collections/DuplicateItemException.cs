using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    [Serializable]
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException() { }
        public DuplicateItemException(string message) : base(message) { }
        public DuplicateItemException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateItemException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
