namespace DayOne;

public class Program
{
    static void Main(string[] args)
    {
        var instructions = System.IO.File.ReadAllText("./Input/DayOne.txt").Trim();
        var noTimeForATaxicab = new NoTimeForATaxicab();
        var distance = noTimeForATaxicab.FollowInstructions(instructions);
        Console.WriteLine($"Part 1: distance is {distance}");
        var locationVisitedTwice = noTimeForATaxicab.EasterBunnyHQ?.GetDistanceToOrigin();
        Console.WriteLine($"Part 2: distance to the first location you visit twice is {locationVisitedTwice}");
    }
}

