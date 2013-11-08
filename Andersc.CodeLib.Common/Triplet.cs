using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// Provides a helper class that is used to store three related objects.
    /// </summary>
    /// <typeparam name="TFirst">The type of the first.</typeparam>
    /// <typeparam name="TSecond">The type of the second.</typeparam>
    /// <typeparam name="TThird">The type of the third.</typeparam>
    [Serializable]
    public sealed class Triplet<TFirst, TSecond, TThird>
    {
        public TFirst First { get; set; }
        public TSecond Second { get; set; }
        public TThird Third { get; set; }

        public Triplet()
        {
        }

        public Triplet(TFirst first, TSecond second)
        {
            this.First = first;
            this.Second = second;
        }

        public Triplet(TFirst first, TSecond second, TThird third) : this(first, second)
        {
            this.Third = third;
        }
    }
}
