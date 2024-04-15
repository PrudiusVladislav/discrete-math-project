using DiscreteMath.Project.FordFulkerson;

/*
int[,] capacities = 
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

Console.WriteLine($"The maximum possible flow is {maxFlow}");*/

List<BenchmarkResult> results = Benchmark.MeasureMaxFlowAlgorithm(graphMatrix =>
{
    MaximumFlow maximumFlow = new(graphMatrix);
    maximumFlow.FindMaxFlow(
        source: 0,
        sink: graphMatrix.GetLength(0) - 1);
});

Benchmark.PrintResults(results);
