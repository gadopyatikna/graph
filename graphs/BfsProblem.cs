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

            return p;
        }
    }

    public class BfsProblem
    {
        Dictionary<int, List<(int vertex, int cost)>> connections;

        Dictionary<int, bool> isVisited;
        Dictionary<int, int> distances;
        Queue<int> toVisit;

        int numVertices;

        public BfsProblem(int nVertices)
        {
            numVertices = nVertices;
            connections = new Dictionary<int, List<(int vertex, int cost)>>();
            isVisited = new Dictionary<int, bool>();
            distances = new Dictionary<int, int>();
            toVisit = new Queue<int>();
            toVisit.Enqueue(1);

            for (int i = 1; i <= numVertices; i++)
            {
                connections[i] = new List<(int, int)>();
                //connections[i].Add((i, 0)); // ?

                isVisited[i] = false;
                distances[i] = int.MaxValue;
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
                //if (isVisited[v] == true)
                //    continue;

                //isVisited[v] = true;
                foreach (var chV in connections[v])
                {
                    //if (isVisited[chV.vertex] == true)
                    //    continue;

                    //isVisited[chV.vertex] = true;

                    var d = chV.cost + distances[v];
                    if (distances[chV.vertex] > d)
                    {
                        distances[chV.vertex] = d;
                        toVisit.Enqueue(chV.vertex);
                    }
                }
            }
        }
    }
}
