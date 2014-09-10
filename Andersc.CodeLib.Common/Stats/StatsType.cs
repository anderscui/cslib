using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Andersc.CodeLib.Common.Stats
{
    struct StatsType<T>
    {
        public T Value { get; set; }

        // Missing Value?
        public bool HasValue { get { return Value == null; } }

        public int Empty()
        {
            return 0;
        }
    }
}
