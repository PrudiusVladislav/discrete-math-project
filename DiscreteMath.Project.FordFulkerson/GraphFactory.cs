﻿namespace DiscreteMath.Project.FordFulkerson;

public class GraphFactory
{
    private readonly Random _random = new();

    public int[,] GenerateRandomDirectedGraph(GraphGenerationOptions options)
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
                if (i != j && _random.NextDouble() < options.Density)
                {
                    capacityMatrix[i, j] = _random.Next(
                        options.MinCapacity, options.MaxCapacity + 1);
                }
            }
        }

        return capacityMatrix;
    }

    public void PrintCapacityMatrix(int[,] capacityMatrix)
    {
        int numVertices = capacityMatrix.GetLength(0);

        for (int i = 0; i < numVertices; i++)
        {
            for (int j = 0; j < numVertices; j++)
            {
                Console.Write($"{capacityMatrix[i, j]} ");
            }
            Console.WriteLine();
        }
    }
    
    public int GetNumberOfEdges(int[,] capacityMatrix)
    {
        int numVertices = capacityMatrix.GetLength(0);
        int numEdges = 0;

        for (int i = 0; i < numVertices; i++)
        {
            for (int j = 0; j < numVertices; j++)
            {
                if (capacityMatrix[i, j] > 0)
                {
                    numEdges++;
                }
            }
        }

        return numEdges;
    }
}