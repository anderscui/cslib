using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        T Front { get; }

        void Clear();
        int Count { get; }
        bool IsEmpty { get; }
    }
}
