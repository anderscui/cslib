using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// Provides a helper class that is used to store two related objects.
    /// </summary>
    /// <typeparam name="TFirst">The type of the first.</typeparam>
    /// <typeparam name="TSecond">The type of the second.</typeparam>
    [Serializable]
    public sealed class Pair<TFirst, TSecond>
    {
        public TFirst First { get; set; }
        public TSecond Second { get; set; }

        public Pair()
        {
        }

        public Pair(TFirst first, TSecond second)
        {
            this.First = first;
            this.Second = second;
        }
    }
}
