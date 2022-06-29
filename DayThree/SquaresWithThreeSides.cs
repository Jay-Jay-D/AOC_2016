namespace DayThree;

public class SquaresWithThreeSides
{
    public static bool IsTriangle(string triangleSides)
    {
        var sides = triangleSides.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => int.Parse(s))
                                 .ToArray();
        return sides[0] + sides[1] > sides[2] &&
        sides[1] + sides[2] > sides[0] &&
        sides[0] + sides[2] > sides[1];
    }

    public static int CountTriangles(IEnumerable<string> trianglesSides)
    {
        return trianglesSides.Count(ts => IsTriangle(ts));
    }

    public static IEnumerable<string> ReadByColumns(IEnumerable<string> trianglesSideData)
    {
        var counter = 1;
        var sidesToYield = Enumerable.Repeat(string.Empty, 3).ToArray();
        foreach (var row in trianglesSideData)
        {
            var sides = row.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)
                           .ToArray();
            for (var idx = 0; idx < 3; idx++)
            {
                sidesToYield[idx] += $"{sides[idx]} ";
            }
            if (counter++ % 3 == 0)
            {
                foreach (var s in sidesToYield)
                {
                    yield return s;
                }
                sidesToYield = Enumerable.Repeat(string.Empty, 3).ToArray();
            }
        }
    }
}
