namespace DiscreteMath.Project.FordFulkerson;

public class MaximumFlow
{
    private readonly int[,] _capacity;
    private readonly int[,] _flow;
    private readonly int[] _parent;

    public MaximumFlow(int[,] capacity)
    {
        _capacity = capacity;
        int n = capacity.GetLength(0);
        _flow = new int[n, n];
        _parent = new int[n];
    }

    private bool BFS(int source, int sink)
    {
        bool[] visited = new bool[_capacity.GetLength(0)];
        Queue<int> queue = new();
        queue.Enqueue(source);
        visited[source] = true;
        _parent[source] = -1;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            for (int v = 0; v < _capacity.GetLength(0); v++)
            {
                if (!visited[v] && _capacity[u, v] - _flow[u, v] > 0)
                {
                    queue.Enqueue(v);
                    _parent[v] = u;
                    visited[v] = true;
                }
            }
        }
        return visited[sink];
    }

    public int FindMaxFlow(int source, int sink)
    {
        int maxFlow = 0;
        while (BFS(source, sink))
        {
            int pathFlow = int.MaxValue;
            for (int v = sink; v != source; v = _parent[v])
            {
                int u = _parent[v];
                pathFlow = Math.Min(pathFlow, _capacity[u, v] - _flow[u, v]);
            }
            for (int v = sink; v != source; v = _parent[v])
            {
                int u = _parent[v];
                _flow[u, v] += pathFlow;
                _flow[v, u] -= pathFlow;
            }
            maxFlow += pathFlow;
        }
        return maxFlow;
    }
}