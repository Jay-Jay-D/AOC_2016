namespace DayFive;

public class Program
{
    public static void Main(string[] args)
    {
        var doorId = "cxdnnyjw";
        var password = NiceGameOfChess.GetPasswordFrom(doorId);
        Console.WriteLine($"Part 1: the password fofr {doorId} is {password}");
    }
}