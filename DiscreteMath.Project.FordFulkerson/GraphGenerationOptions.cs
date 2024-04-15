namespace DiscreteMath.Project.FordFulkerson;

public record GraphGenerationOptions
{
    public int NumVertices { get; init; }
    public double Density { get; init; }
    public int MinCapacity { get; init; }
    public int MaxCapacity { get; init; }
    
    public GraphGenerationOptions(
        int numVertices, 
        double density, 
        int minCapacity = 1,
        int maxCapacity = 10)
    {
        if (density < 0 || density > 1 ||
            numVertices < 0 || 
            minCapacity <= 0 || maxCapacity < minCapacity)
        {
            throw new ArgumentException("Invalid graph generation parameters.");
        }
        
        NumVertices = numVertices;
        Density = density;
        MinCapacity = minCapacity;
        MaxCapacity = maxCapacity;
    }
};