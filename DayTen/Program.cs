namespace DayTen;

public class Program
{
    public static void Main(string[] args)
    {
        var instructions = File.ReadAllLines(@"Input/DayTen.txt");
        var balanceBots = new BalanceBots(instructions);
        balanceBots.InitializeBots();
        balanceBots.Run();
        Console.WriteLine("Part 1: the bot that is responsible for comparing value-61 microchips " +
                          $"with value-17 microchips is {balanceBots.WhichBotCompared(17, 61)}.");
    }
}
