using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Eulerian
{
    class Graph
    {
        private int V;
        private Dictionary<int, List<int>> G;

        Graph(int vertices)
        {
            V = vertices;
            G = new Dictionary<int, List<int>>();
        }

        void AddEdge(int u, int v)
        {
            try
            {
                G[u].Add(v);
            }
            catch (Exception e)
            {
                List<int> i = new List<int>() { v };
                G.Add(u, i);
            }

            try
            {
                G[v].Add(u);
            }
            catch (Exception e)
            {
                List<int> j = new List<int>() { u };
                G.Add(v, j);
            }
        }

        void Test()
        {
            int result = this.IsEulerian();

            if (result == 0)
                Console.WriteLine("The graph is not Eulerian");
            else if (result == 1)
                Console.WriteLine("The graph is an Eulerian path");
            else
                Console.WriteLine("The graph is an Eulerian Cycle");
        }

        int IsEulerian()
        {
            //  not Eulerian
            if (this.IsConnected() == false)
                return 0;
            int odd = 0;

            //  if 2 vertices have odd paths, then Path
            //  else if 1 vertex odd, then cycle
            //  else not Eulerian
            foreach (int i in G.Keys)
            {
                if (G[i].Count % 2 != 0)
                    odd += 1;
            }

            if (odd == 0)
                return 2;
            if (odd == 2)
                return 1;
            return 0;
        }

        bool IsConnected()
        {
            int iCount = 0;
            bool[] visited = new bool[V];


            foreach (int i in G.Keys)
            {
                if ( G[i].Count > 0)
                    break;

                iCount++;
            }

            //  base case
            if (iCount == V - 1)
                return true;

            //  depth first search: start at first non-zero vertex 
            DFSUtil(iCount, visited);

            foreach (int i in G.Keys)
            {
                if (visited[i] == false && G[i].Count > 0)
                    return false;
            }

            return true;
        }

        void DFSUtil(int ver, bool[] visited)
        {
            visited[ver] = true;

            try
            {
                foreach (int i in G[ver])
                {
                    if (visited[i] == false)
                    {
                        DFSUtil(i, visited);
                    }
                }
            }
            catch (Exception e)
            {
                
            }
        }

        public static void Main(string[] args)
        {
            Graph g1 = new Graph(5);
            g1.AddEdge(1, 0);
            g1.AddEdge(0, 2);
            g1.AddEdge(2, 1);
            g1.AddEdge(0, 3);
            g1.AddEdge(3, 4);
            g1.Test();

            Graph g2 = new Graph(5);
            g2.AddEdge(1, 0);
            g2.AddEdge(0, 2);
            g2.AddEdge(2, 1);
            g2.AddEdge(0, 3);
            g2.AddEdge(3, 4);
            g2.AddEdge(4, 0);
            g2.Test();

            Graph g3 = new Graph(5);
            g3.AddEdge(1, 0);
            g3.AddEdge(0, 2);
            g3.AddEdge(2, 1);
            g3.AddEdge(0, 3);
            g3.AddEdge(3, 4);
            g3.AddEdge(1, 3);
            g3.Test();

            Graph g4 = new Graph(3);
            g4.AddEdge(0, 1);
            g4.AddEdge(1, 2);
            g4.AddEdge(2, 0);
            g4.Test();

            Graph g5 = new Graph(3);
            g5.Test();


            Console.ReadKey();
        }
    }
}
