using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Algorithm.Graphs;

namespace Andersc.CodeLib.Tester.Algorithm
{
    [TestFixture]
    public class TestGraph
    {
        private Graph GetNewGraph()
        {
            Graph g = new Graph();
            //g.AddEdge("D", "C", 10);
            g.AddEdge("A", "B", 12);
            g.AddEdge("D", "B", 23);
            g.AddEdge("A", "D", 87);
            g.AddEdge("E", "D", 43);
            g.AddEdge("B", "E", 11);
            g.AddEdge("C", "A", 19);

            return g;
        }

        [Test]
        public void TestNonweighted()
        {
            Graph g = GetNewGraph();
            g.Unweighted("A");
            g.PrintAllPaths();
        }

        [Test]
        public void TestDijkstra()
        {
            Graph g = GetNewGraph();
            g.Dijkstra("A");
            g.PrintAllPaths();
        }
    }
}
