using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Algorithm.Graphs
{
    // TODO: rename to Route
    public class Edge
    {
        // TODO: rename to End?
        public Vertex Dest { get; set; }
        public double Cost { get; set; }

        public Edge(Vertex dest, double cost)
        {
            Dest = dest;
            Cost = cost;
        }
    }
}
