using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ACC = Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Algorithm.Graphs
{
    // TODO: Add some comments.
    public class Graph
    {
        // TODO: check all modifiers.
        internal static readonly double INFINITY = double.MaxValue;

        private Dictionary<string, Vertex> vertexes = new Dictionary<string, Vertex>();

        public void AddEdge(string sourceName, string destName, double cost)
        {
            Vertex source = GetVertex(sourceName);
            Vertex dest = GetVertex(destName);
            // TODO: add last or first?
            source.Adjacences.AddLast(new Edge(dest, cost));
        }

        public void Unweighted(string startName)
        {
            ClearAll();

            Vertex start = vertexes[startName];
            if (start == null) { throw new ArgumentOutOfRangeException("startName"); }

            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(start);
            start.Distance = 0;

            while (queue.Count > 0)
            {
                Vertex current = queue.Dequeue();
                foreach (Edge edge in current.Adjacences)
                {
                    Vertex dest = edge.Dest;
                    if (dest.Distance == INFINITY)
                    {
                        dest.Distance = current.Distance + 1;
                        dest.Prev = current;
                        queue.Enqueue(dest);
                    }
                }
            }
        }

        public void Dijkstra(string startName)
        {
            ACC.PriorityQueue<Path> pq = new ACC.PriorityQueue<Path>();

            Vertex start = vertexes[startName];
            if (start == null) { throw new ArgumentOutOfRangeException("startName"); }

            ClearAll();
            
            start.Distance = 0;
            pq.Add(new Path(start, 0));
            int nodesSeen = 0;
            while (!pq.IsEmpty && nodesSeen < vertexes.Count)
            {
                Path path = pq.Remove();
                Vertex v = path.Dest;
                if (v.Scratch != 0) // already processed node v.
                {
                    continue;
                }

                v.Scratch = 1; // this node has been seen.
                nodesSeen++;

                foreach (Edge edge in v.Adjacences)
                {
                    Vertex w = edge.Dest;
                    double costOfW = edge.Cost;

                    if (costOfW < 0)
                    {
                        throw new Exception("Graph has negatie cost.");
                    }

                    if (w.Distance > v.Distance + costOfW) // find a shorter path
                    {
                        w.Distance = v.Distance + costOfW;
                        w.Prev = v;
                        pq.Add(new Path(w, w.Distance));
                    }
                }
            }
        }

        public void PrintAllPaths()
        {
            foreach (string name in vertexes.Keys)
            {
                PrintPath(name);
            }
        }

        public void PrintPath(string destName)
        {
            Vertex dest = vertexes[destName];
            if (dest == null) { throw new ArgumentOutOfRangeException("destName"); }

            if (dest.Distance == INFINITY)
            {
                Console.WriteLine("unreachable to {0}", destName);
            }
            else
            {
                Console.WriteLine("Distance to {0} is {1}", destName, dest.Distance);
                PrintPath(dest);
                Console.WriteLine();
            }
        }

        private Vertex GetVertex(string name)
        {
            // TODO: check name?
            if (!vertexes.ContainsKey(name))
            {
                Vertex newVertex = new Vertex(name);
                vertexes.Add(name, newVertex);
            }

            return vertexes[name];
        }

        private void PrintPath(Vertex dest)
        {
            if (dest.Prev != null)
            {
                PrintPath(dest.Prev);
                Console.Write(" to ");
            }
            Console.Write(dest.Name);
        }

        private void ClearAll()
        {
            foreach (Vertex v in vertexes.Values)
            {
                v.Reset();
            }
        }
    }
}
