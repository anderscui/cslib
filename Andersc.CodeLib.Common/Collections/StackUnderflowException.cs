using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    [Serializable]
    public class StackUnderflowException : Exception
    {
        public StackUnderflowException() { }
        public StackUnderflowException(string message) : base(message) { }
        public StackUnderflowException(string message, Exception inner) : base(message, inner) { }
        protected StackUnderflowException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
