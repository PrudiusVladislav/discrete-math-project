namespace DiscreteMath.Project.FordFulkerson;

public class DirectedWeighedGraph
{
    private static readonly Random Random = new();
    
    public int[,] CapacityMatrix { get; }
    public List<(int,int)>[] AdjacencyList { get; }

    public DirectedWeighedGraph(int[,] capacityMatrix)
    {
        CapacityMatrix = capacityMatrix;
        AdjacencyList = CreateAdjacencyListFromCapacityMatrix(capacityMatrix);
    }
    
    public DirectedWeighedGraph(List<(int, int)>[] adjacencyList)
    {
        AdjacencyList = adjacencyList;
        CapacityMatrix = CreateCapacityMatrixFromAdjacencyList(adjacencyList);
    }
    
    public static DirectedWeighedGraph CreateRandomGraph(GraphGenerationOptions options)
    {
        int[,] capacityMatrix = new int[options.NumVertices, options.NumVertices];

        // all capacities to 0
        for (int i = 0; i < options.NumVertices; i++)
        {
            for (int j = 0; j < options.NumVertices; j++)
            {
                capacityMatrix[i, j] = 0;
            }
        }

        // Erdos-Renyi model (directed graph)
        for (int i = 0; i < options.NumVertices; i++)
        {
            for (int j = 0; j < options.NumVertices; j++)
            {
                if (i != j && Random.NextDouble() < options.Density)
                {
                    capacityMatrix[i, j] = Random.Next(
                        options.MinCapacity, options.MaxCapacity + 1);
                }
            }
        }

        return new DirectedWeighedGraph(capacityMatrix);
    }
    
    private static int[,] CreateCapacityMatrixFromAdjacencyList(List<(int, int)>[] adjacencyList)
    {
        int numVertices = adjacencyList.Length;
        int[,] capacityMatrix = new int[numVertices, numVertices];

        for (int i = 0; i < numVertices; i++)
        {
            foreach (var (neighbour, capacity) in adjacencyList[i])
            {
                capacityMatrix[i, neighbour] = capacity;
            }
        }

        return capacityMatrix;
    }
    
    private static List<(int, int)>[] CreateAdjacencyListFromCapacityMatrix(int[,] capacityMatrix)
    {
        int numVertices = capacityMatrix.GetLength(0);
        List<(int, int)>[] adjacencyList = new List<(int, int)>[numVertices];

        for (int i = 0; i < numVertices; i++)
        {
            adjacencyList[i] = new List<(int, int)>();
            for (int j = 0; j < numVertices; j++)
            {
                if (capacityMatrix[i, j] > 0)
                {
                    adjacencyList[i].Add((j, capacityMatrix[i, j]));
                }
            }
        }

        return adjacencyList;
    }
    
    public int GetNumberOfEdges()
    {
        int numVertices = CapacityMatrix.GetLength(0);
        int numEdges = 0;

        for (int i = 0; i < numVertices; i++)
        {
            for (int j = 0; j < numVertices; j++)
            {
                if (CapacityMatrix[i, j] > 0)
                {
                    numEdges++;
                }
            }
        }

        return numEdges;
    }
}