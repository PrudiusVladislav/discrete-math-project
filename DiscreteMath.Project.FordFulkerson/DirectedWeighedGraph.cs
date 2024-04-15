namespace DiscreteMath.Project.FordFulkerson;

public class DirectedWeighedGraph
{
    public int[,] CapacityMatrix { get; }

    public DirectedWeighedGraph(int[,] capacityMatrix)
    {
        CapacityMatrix = capacityMatrix;
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
    
    public void PrintMatrix()
    {
        int numVertices = CapacityMatrix.GetLength(0);

        for (int i = 0; i < numVertices; i++)
        {
            for (int j = 0; j < numVertices; j++)
            {
                Console.Write($"{CapacityMatrix[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}