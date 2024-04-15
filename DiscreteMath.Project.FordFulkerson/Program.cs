﻿

using DiscreteMath.Project.FordFulkerson;

int[,] capacities = new int[,]
{
    {0, 10, 10, 0, 0, 0},
    {0, 0, 0, 25, 0, 0},
    {0, 0, 0, 0, 15, 0},
    {0, 0, 0, 0, 0, 10},
    {0, 6, 0, 0, 0, 10},
    {0, 0, 0, 0, 0, 0}
};

int source = 0;
int sink = 5;

MaximumFlow maximumFlow = new(capacities);
int maxFlow = maximumFlow.FindMaxFlow(source, sink);

Console.WriteLine($"The maximum possible flow is {maxFlow}");