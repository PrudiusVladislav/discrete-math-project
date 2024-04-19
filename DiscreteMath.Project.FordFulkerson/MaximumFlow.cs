namespace DiscreteMath.Project.FordFulkerson;

public class MaximumFlow
{
    private readonly DirectedWeighedGraph _graph;
    private readonly int[,] _flow;
    private readonly int[] _parent;
    private readonly int _nodesCount;

    public MaximumFlow(DirectedWeighedGraph graph)
    {
        _graph = graph;
        _nodesCount = graph.CapacityMatrix.GetLength(0);
        _flow = new int[_nodesCount, _nodesCount];
        _parent = new int[_nodesCount];
    }
    
    public int FindMaxFlow(FlowPath flowPath, bool useAdjacencyList)
    {
        return useAdjacencyList 
            ? FindMaxFlowAdjacencyList(flowPath) 
            : FindMaxFlow(flowPath);
    }

    private bool BFS(int source, int sink)
    {
        bool[] visited = new bool[_nodesCount];
        Queue<int> queue = new();
        queue.Enqueue(source);
        visited[source] = true;
        _parent[source] = -1;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            for (int v = 0; v < _nodesCount; v++)
            {
                // if there is capacity from u to v
                if (!visited[v] && _graph.CapacityMatrix[u, v] - _flow[u, v] > 0) 
                {
                    queue.Enqueue(v);
                    _parent[v] = u;
                    visited[v] = true;
                }
            }
        }
        return visited[sink];
    }
    
    private bool BFSAdjacencyList(int source, int sink)
    {
        bool[] visited = new bool[_nodesCount];
        Queue<int> queue = new();
        queue.Enqueue(source);
        visited[source] = true;
        _parent[source] = -1;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            foreach (var (neighbour, capacity) in _graph.AdjacencyList[u])
            {
                if (!visited[neighbour] && capacity - _flow[u, neighbour] > 0)
                {
                    queue.Enqueue(neighbour);
                    _parent[neighbour] = u;
                    visited[neighbour] = true;
                }
            }
        }
        return visited[sink];
    }

    private int FindMaxFlow(FlowPath flowPath)
    {
        int maxFlow = 0;
        while (BFS(flowPath.Source, flowPath.Sink))
        {
            int pathFlow = int.MaxValue;
            for (int v = flowPath.Sink; v != flowPath.Source; v = _parent[v])
            {
                int u = _parent[v];
                pathFlow = Math.Min(pathFlow, _graph.CapacityMatrix[u, v] - _flow[u, v]); // find bottleneck
            }
            for (int v = flowPath.Sink; v != flowPath.Source; v = _parent[v])
            {
                int u = _parent[v];
                _flow[u, v] += pathFlow;
                _flow[v, u] -= pathFlow; // residual capacity
            }
            maxFlow += pathFlow;
        }
        return maxFlow;
    }
    
    private int FindMaxFlowAdjacencyList(FlowPath flowPath)
    {
        int maxFlow = 0;
        while (BFSAdjacencyList(flowPath.Source, flowPath.Sink))
        {
            int pathFlow = int.MaxValue;
            for (int v = flowPath.Sink; v != flowPath.Source; v = _parent[v])
            {
                int u = _parent[v];
                pathFlow = Math.Min(
                    pathFlow, 
                    _graph.AdjacencyList[u].First(n => n.Node == v).EdgeCapacity); 
            }
            for (int v = flowPath.Sink; v != flowPath.Source; v = _parent[v])
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