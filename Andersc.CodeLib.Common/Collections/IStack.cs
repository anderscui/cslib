using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    interface IStack<T>
    {
        void Push(T item);
        T Pop();
        T Top { get; }

        void Clear();
        int Count { get; }
        bool IsEmpty { get; }
    }
}
