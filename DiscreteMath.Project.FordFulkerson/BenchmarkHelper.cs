using System.Diagnostics;
using ConsoleTables;

namespace DiscreteMath.Project.FordFulkerson;

public class BenchmarkHelper
{
    public static Task<List<BenchmarkResult>> MeasureMaxFlowAsync(bool useAdjacencyList = false)
    { 
        return Task.Run(() => 
            Benchmark.MeasureMaxFlowAlgorithm((graph, flowPath) => 
                { 
                    new MaximumFlow(graph).FindMaxFlow(flowPath, useAdjacencyList); 
                })
                .OrderBy(r => r.AvgTime)
                .ToList());
    }
    
    public static void PrintResults(List<BenchmarkResult> results, bool useAdjacencyList = false)
    {
        Console.WriteLine($"Benchmark results (graph-form: {(useAdjacencyList ? "Adjacency List" : "Adjacency Matrix")}):");
        PrintResultsTable(results);
        Console.WriteLine("=============================================");
    }
    
    private static void PrintResultsTable(List<BenchmarkResult> results)
    {
        ConsoleTable table = new();
        table.AddColumn(new []{ "Vertices", "Density", "AvgTime (ms)" });
        
        foreach (BenchmarkResult result in results)
        {
            table.AddRow(
                result.Options.NumVertices, 
                result.Options.Density, 
                result.AvgTime.TotalMilliseconds);
        }
        
        table.Write();
    }
    
    public static async Task WriteResults(List<BenchmarkResult> results, bool usedAdjacencyList = false)
    {
        await Task.Run(() =>
        {
            var densityGroups = results
                .GroupBy(r => r.Options.Density)
                .ToList();

            var vertices = results
                .GroupBy(r => r.Options.NumVertices)
                .Select(g => g.Key);
            
            using StreamWriter writer = 
                new($"../../../results/results-{(usedAdjacencyList ? "list" : "matrix")}.csv");
                
            writer.WriteLine($"Vertices,{string.Join(',', densityGroups.Select(g => g.Key))}");

            foreach (int vertex in vertices)
            {
                writer.Write($"{vertex}");
                foreach (var group in densityGroups)
                {
                    writer.Write($",{group.First(r => r.Options.NumVertices == vertex).AvgTime.TotalMilliseconds}");
                }
                writer.WriteLine();
            }
        });
    }
    
    public static async Task<long> MeasureTimeAsync(Func<Task> action)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        await action();
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}