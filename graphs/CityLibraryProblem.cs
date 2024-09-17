using System;
using System.Collections.Generic;

namespace graphs
{
    // 1 lib + (c - 1) roads

    static class CityLibraryProblemFactory
    {
        public static CityLibraryProblem Create()
        {
            var p = new CityLibraryProblem(3, 3, 2, 1);

            p.AddConnection(1, 2);
            p.AddConnection(3, 1);
            p.AddConnection(2, 3);

            var s = p.Solve();

            return p;
            //6 6 2 5
            //1 3
            //3 4
            //2 4
            //1 2
            //2 3
            //5 6
        }
    }

    class CityLibraryProblem
    {
        int numCities;
        int numRoads;
        int costLib;
        int costRoad;
        Dictionary<int, List<int>> connections;
        Dictionary<int, List<int>> groups;
        Dictionary<int, bool> isVisited;

        public CityLibraryProblem(
            int n_cities,
            int n_roads,
            int c_lib,
            int c_road
            )
        {
            numCities = n_cities;
            numRoads = n_roads;
            costLib = c_lib;
            costRoad = c_road;

            connections = new Dictionary<int, List<int>>();
            groups = new Dictionary<int, List<int>>();
            isVisited = new Dictionary<int, bool>();

            for (int i = 1; i <= numCities; i++)
            {
                connections[i] = new List<int>();
                connections[i].Add(i); // ?

                groups[i] = new List<int>();
                isVisited[i] = false;
            }

        }

        public void AddConnection(int city1, int city2)
        {
            connections[city1].Add(city2);
            connections[city2].Add(city1);
        }

        public int Solve()
        {
            if (costLib <= costRoad)
                return costLib * numCities;

            var groups = GetGroups();
            var totalCost = groups.Count * costLib; 
            foreach (var g in groups)
                totalCost += (g.Count - 1) * costRoad;
            return totalCost;
        }

        private List<HashSet<int>> GetGroups()
        {
            var allGroups = new List<HashSet<int>>();

            for (int i = 1; i <= numCities; i++)
            {
                if (isVisited[i])
                    continue;

                isVisited[i] = true;

                // keep track of group
                var newGroup = new List<int>();

                // init queue of connected set
                var q = new Queue<List<int>>();
                q.Enqueue(connections[i]);

                while (q.Count != 0)
                {
                    var set = q.Dequeue();
                    newGroup.AddRange(set);

                    foreach (var city in set)
                    {
                        if (isVisited[city])
                            continue;

                        isVisited[city] = true;
                        q.Enqueue(connections[city]);
                    }
                }

                allGroups.Add(new HashSet<int>(newGroup));
            }

            return allGroups;
        }
    }
}
