namespace DayThree;

public class Program
{
    public static void Main(string[] args)
    {
        var trianglesSides = File.ReadAllLines(@"Input/DayThree.txt");
        var triangleCount = SquaresWithThreeSides.CountTriangles(trianglesSides);
        Console.WriteLine($"Part 1: there are {triangleCount} triangles.");
        triangleCount = SquaresWithThreeSides.CountTriangles(SquaresWithThreeSides.ReadByColumns(trianglesSides));
        Console.WriteLine($"Part 2: there are {triangleCount} triangles.");
    }
}