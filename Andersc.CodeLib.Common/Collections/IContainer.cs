using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Collections
{
    public interface IContainer<T> : ICollection<T>, IEnumerable<T>
    {
        bool IsEmpty { get; }
        bool IsFull { get; }
    }
}
