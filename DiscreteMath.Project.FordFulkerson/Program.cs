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


long executionTimeMs = await BenchmarkHelper.MeasureTimeAsync(async () =>
{
    Task<List<BenchmarkResult>> maxFlow = BenchmarkHelper.MeasureMaxFlowAsync();
    Task<List<BenchmarkResult>> maxFlowWithList = BenchmarkHelper.MeasureMaxFlowAsync(
        useAdjacencyList: true);

    Task writeResults = BenchmarkHelper.WriteResults(await maxFlow);
    Task writeResultsWithList = BenchmarkHelper.WriteResults(await maxFlowWithList,
        usedAdjacencyList: true);
    
    await Task.WhenAll(writeResults, writeResultsWithList);
});

Console.WriteLine($"Total time: {executionTimeMs} ms");
