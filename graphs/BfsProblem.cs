using System;
using System.Collections.Generic;

namespace graphs
{
    static class BfsProblemFactory
    {
        public static BfsProblem Create()
        {
            var p = new BfsProblem(4);

            p.AddConnection(1, 2, 10);
            p.AddConnection(1, 3, 1);
            p.AddConnection(2, 3, 5);
            p.AddConnection(3, 4, 1);
            p.AddConnection(4, 2, 1);

            p.Solve();
            p.PrintPath(2);
            return p;
        }
    }

    public class BfsProblem
    {
        Dictionary<int, List<(int vertex, int cost)>> connections;

        Dictionary<int, int> distances;
        Dictionary<int, int> paths;
        Queue<int> toVisit;

        int numVertices;

        public BfsProblem(int nVertices)
        {
            numVertices = nVertices;
            connections = new Dictionary<int, List<(int vertex, int cost)>>();
            distances = new Dictionary<int, int>();
            toVisit = new Queue<int>();
            paths = new Dictionary<int, int>();
            toVisit.Enqueue(1);

            for (int i = 1; i <= numVertices; i++)
            {
                connections[i] = new List<(int, int)>();
                distances[i] = int.MaxValue;
                paths[i] = -1;
            }

            distances[1] = 0;
        }


        public void AddConnection(int v1, int v2, int cost)
        {
            connections[v1].Add((v2, cost));
            connections[v2].Add((v1, cost));
        }

        public void Solve()
        {

            while (toVisit.Count != 0)
            {
                var v = toVisit.Dequeue();
                foreach (var chV in connections[v])
                { 

                    var d = chV.cost + distances[v];
                    if (distances[chV.vertex] > d)
                    {
                        distances[chV.vertex] = d;
                        // dijkstra path tracking
                        paths[chV.vertex] = v;
                        toVisit.Enqueue(chV.vertex);
                    }
                }
            }
        }

        public void PrintPath(int v)
        {
            var curV = v;
            while (paths[curV] != -1)
            {
                var prevV = paths[curV];
                Console.Out.WriteLine(prevV);
                curV = prevV;
            }
        }
    }
}
