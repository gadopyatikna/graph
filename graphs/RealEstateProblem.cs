using System;
using System.Collections.Generic;
using System.Linq;

namespace graphs
{
    static class RealEstateProblemfactory
    {
        public static RealEstateProblem Create()
        {
            //5 110
            //9 500
            //20 400

            //10 100
            //2 200
            //30 300
            List<(int x, int y)> clients = new List<(int x, int y)>();
            clients.Add((0, 0));
            clients.Add((5, 110));
            clients.Add((9, 500));
            clients.Add((20, 400));

            List<(int x, int y)> houses = new List<(int x, int y)>();
            houses.Add((0, 0));
            houses.Add((10, 100));
            houses.Add((2, 200));
            houses.Add((30, 300));

            var p = new RealEstateProblem(clients, houses);
            p.Solve();
            return p;
        }
    }

    class Node
    {
        public static int visitedToken = 0;

        public int idx;
        public int isVisited = visitedToken;
        public int flow = 0;
        public int capacity;
    }

    class RealEstateProblem
    {
        int root = 0;
        int target = int.MaxValue;

        Dictionary<int, int> isVisited;

        List<(int x, int y)> clients;
        List<(int x, int y)> houses;
        Dictionary<int, List<Node>> graph;

        public RealEstateProblem(List<(int x, int y)> c, List<(int x, int y)> h)
        {
            clients = c;
            houses = h;

            graph = new Dictionary<int, List<Node>>();
            graph[0] = new List<Node>(); // 0 connected to all clients
            isVisited = new Dictionary<int, int>();
            isVisited[0] = Node.visitedToken;

            for (var i = 1; i < clients.Count; i++)
            {
                graph[root].Add(new Node() { idx = i, capacity = 1 });
                graph[i] = new List<Node>();
                isVisited[i] = Node.visitedToken;

                for (int j = 1; j < houses.Count; j++)
                {
                    if (houses[j].x >= clients[i].x &&
                        houses[j].y <= clients[i].y)
                    {
                        graph[i].Add(new Node() { idx = j * 10, capacity = 1 }); // clients connected to some houses
                    }
                }
            }
            for (int j = 1; j < houses.Count; j++)
            {
                graph[j * 10] = new List<Node>() { new Node() { idx = target, capacity = 1 } };
                isVisited[j * 10] = Node.visitedToken;
            }

            graph[target] = new List<Node>();
            isVisited[target] = Node.visitedToken++;
        }

        public void Solve()
        {
            int maxFlow = 0;
            int f;
            do {
                f = Dfs(root, int.MaxValue);
                maxFlow+= f;
                Node.visitedToken++;
            }
            while (f != 0);
        }

        private int Dfs(int vertex, int flow)
        {
            if (vertex == target)
                return flow;

            isVisited[vertex] = Node.visitedToken;

            foreach (var chV in graph[vertex])
            {
                var remCap = chV.capacity - chV.flow;
                if (remCap > 0 && isVisited[chV.idx] != Node.visitedToken)
                {
                    var bottleneck = Dfs(chV.idx, Math.Min(flow, remCap));
                    if (bottleneck > 0)
                    {
                        chV.flow += bottleneck;
                        return bottleneck;
                    }
                }
            }
            return 0;
        }


    }
}
