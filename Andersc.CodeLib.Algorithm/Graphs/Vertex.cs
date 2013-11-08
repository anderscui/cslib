using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Algorithm.Graphs
{
    // TODO: rename to Stop
    public class Vertex
    {
        public string Name { get; set; }
        public LinkedList<Edge> Adjacences { get; set; }
        public double Distance { get; set; }
        public Vertex Prev { get; set; }
        // TODO: rename to processed?
        public int Scratch { get; set; }

        public Vertex(string name)
        {
            Name = name;
            Adjacences = new LinkedList<Edge>();
            Reset();
        }

        public void Reset()
        {
            Distance = Graph.INFINITY;
            Prev = null;
            Scratch = 0;
        }
    }
}
