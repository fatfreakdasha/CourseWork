using System;
using System.Collections.Generic;

public class Graph
{
    private int V;
    private List<int>[] adj;

    public Graph(int v)
    {
        V = v;
        adj = new List<int>[V];
        for (int i = 0; i < V; ++i)
            adj[i] = new List<int>();
    }

    public void AddEdge(int v, int w)
    {
        adj[v].Add(w);
        adj[w].Add(v);
    }

    public int GetV()
    {
        return V;
    }

    public List<int> GetAdj(int v)
    {
        return adj[v];
    }

    public void RemoveEdge(int v, int w)
    {
        adj[v].Remove(w);
        adj[w].Remove(v);
    }
}

public class EulerianPath
{
    private Graph graph;

    public EulerianPath(Graph g)
    {
        graph = g;
    }

    private void DFS(int v, List<int> path)
    {
        while (graph.GetAdj(v).Count != 0)
        {
            int u = graph.GetAdj(v)[0];
            graph.RemoveEdge(v, u);
            DFS(u, path);
        }
        path.Add(v);
    }

    public List<int> GetEulerianPath()
    {
        int oddDegreeVertex = -1;
        for (int i = 0; i < graph.GetV(); ++i)
        {
            if (graph.GetAdj(i).Count % 2 != 0)
            {
                oddDegreeVertex = i;
                break;
            }
        }

        List<int> path = new List<int>();
        path.Add(oddDegreeVertex);

        if (oddDegreeVertex == -1)
        {
            path.Add(0);
        }
        else
        {
            DFS(oddDegreeVertex, path);
        }

        return path;
    }
}

public class HamiltonianPath
{
    private Graph graph;
    private int[] path;

    public HamiltonianPath(Graph g)
    {
        graph = g;
        path = new int[g.GetV()];
        for (int i = 0; i < g.GetV(); ++i)
            path[i] = -1;
    }

    private bool IsSafe(int v, int pos)
    {
        if (!graph.GetAdj(path[pos - 1]).Contains(v))
            return false;

        for (int i = 0; i < pos; ++i)
        {
            if (path[i] == v)
                return false;
        }

        return true;
    }

    private bool HamiltonianUtil(int pos)
    {
        if (pos == graph.GetV())
        {
            if (graph.GetAdj(path[pos - 1]).Contains(path[0]))
                return true;
            else
                return false;
        }

        for (int v = 1; v < graph.GetV(); ++v)
        {
            if (IsSafe(v, pos))
            {
                path[pos] = v;

                if (HamiltonianUtil(pos + 1))
                    return true;

                path[pos] = -1;
            }
        }

        return false;
    }

    public List<int> GetHamiltonianPath()
    {
        path[0] = 0;
        if (HamiltonianUtil(1))
        {
            List<int> hamiltonianPath = new List<int>();
            for (int i = 0; i < path.Length; i++)
            {
                hamiltonianPath.Add(path[i]);
            }
            return hamiltonianPath;
        }
        else
        {
            return null;
        }
    }
}

public class Program
{
    public static Graph CreateGraphManually()
    {
        Console.WriteLine("Введите количество вершин в графе:");
        int vertexCount = Convert.ToInt32(Console.ReadLine());

        Graph graph = new Graph(vertexCount);

        Console.WriteLine("Введите количество ребер:");
        int edgeCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < edgeCount; i++)
        {
            Console.WriteLine("Введите начальную вершину ребра {0}:", i + 1);
            int startVertex = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите конечную вершину ребра {0}:", i + 1);
            int endVertex = Convert.ToInt32(Console.ReadLine());

            graph.AddEdge(startVertex, endVertex);
        }

        return graph;
    }

    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Graph graph = CreateGraphManually();

        EulerianPath eulerianPath = new EulerianPath(graph);
        List<int> eulerian = eulerianPath.GetEulerianPath();

        HamiltonianPath hamiltonianPath = new HamiltonianPath(graph);
        List<int> hamiltonian = hamiltonianPath.GetHamiltonianPath();

        Console.WriteLine("Эйлеров путь:");
        if (eulerian != null)
        {
            foreach (int vertex in eulerian)
            {
                Console.Write(vertex + " ");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Эйлеров путь не существует.");
        }

        Console.WriteLine("Гамильтонов путь:");
        if (hamiltonian != null)
        {
            foreach (int vertex in hamiltonian)
            {
                Console.Write(vertex + " ");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Гамильтонов путь не существует.");
        }

        Console.ReadLine();
    }
}
