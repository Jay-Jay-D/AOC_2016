namespace DayFive;

public class Program
{
    public static void Main(string[] args)
    {
        var doorId = "cxdnnyjw";
        var password = NiceGameOfChess.GetPasswordFrom(doorId);
        Console.WriteLine($"Part 1: the password for {doorId} is {password}");
        password = NiceGameOfChess.GetPasswordFrom(doorId, true);
        Console.WriteLine($"Part 2: the password for {doorId} is {password}");
    }
}