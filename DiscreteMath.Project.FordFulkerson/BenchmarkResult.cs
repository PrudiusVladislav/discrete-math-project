namespace DiscreteMath.Project.FordFulkerson;

public record BenchmarkResult(
    GraphGenerationOptions Options, 
    List<TimeSpan> IterationTimes)
{
    public TimeSpan AvgTime { get; set; }
}