using System.Diagnostics;
using ConsoleTables;

namespace DiscreteMath.Project.FordFulkerson;

public class Benchmark
{
    private static int[] GetSampleVertices(
        int min = 20, int max = 200, int amount = 10)
    {
        int[] vertices = new int[amount];
        int step = (max - min) / amount;
        for (int i = 0; i < amount; i++)
        {
            vertices[i] = min + i * step;
        }
        
        return vertices;
    }
    
    private static double[] GetSampleDensities(int amount = 5)
    {
        double[] densities = new double[amount];
        double step = Math.Round(1.0 / amount, 2);
        
        for (int i = 0; i < amount; i++)
        {
            densities[i] = Math.Round(step + i * step, 2);
        }
        
        return densities;
    }
    
    public static List<GraphGenerationOptions> GenerateOptions()
    {
        int[] numVertices = GetSampleVertices();
        double[] densities = GetSampleDensities();
        List<GraphGenerationOptions> options = [];
        
        foreach (int vertices in numVertices)
        {
            foreach (double density in densities)
            {
                options.Add(new GraphGenerationOptions(vertices, density));
            }
        }
        
        return options;
    }
    
    public static List<BenchmarkResult> MeasureMaxFlowAlgorithm(
        Action<DirectedWeighedGraph> action, int iterations = 20)
    {
        Stopwatch stopwatch = new();
        List<BenchmarkResult> results = [];
        
        List<GraphGenerationOptions> options = GenerateOptions();
        
        foreach (GraphGenerationOptions option in options)
        {
            BenchmarkResult benchmarkResult = new(option, []);
            
            for (int i = 0; i < iterations; i++)
            {
                DirectedWeighedGraph graph = DirectedWeighedGraph.CreateRandomGraph(option);
                stopwatch.Restart();
                action(graph);
                stopwatch.Stop();
                benchmarkResult.IterationTimes.Add(stopwatch.Elapsed);
            }

            results.Add(benchmarkResult with 
            {
                AvgTime = TimeSpan.FromTicks(
                    (long)benchmarkResult.IterationTimes.Average(t => t.Ticks))
            });
        }
        
        return results;
    }
    
    public static void PrintResults(List<BenchmarkResult> results)
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
}