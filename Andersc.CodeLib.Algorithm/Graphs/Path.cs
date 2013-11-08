using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Algorithm.Graphs
{
    public class Path : IComparable<Path>
    {
        public Vertex Dest { get; set; }
        public double Cost { get; set; }

        public Path(Vertex dest, double cost)
        {
            Dest = dest;
            Cost = cost;
        }

        public int CompareTo(Path other)
        {
            return Cost > other.Cost ? 1 : Cost < other.Cost ? -1 : 0;
        }
    }
}
