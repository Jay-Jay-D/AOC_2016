namespace DayTen;

public class Program
{
    public static void Main(string[] args)
    {
        var instructions = File.ReadAllLines(@"Input/DayTen.txt");
        var balanceBots = new BalanceBots(instructions);
        balanceBots.Run();
        Console.WriteLine("Part 1: the bot that is responsible for comparing value-61 microchips " +
                          $"with value-17 microchips is {balanceBots.WhichBotCompared(17, 61)}.");
        var productOutputBins = Enumerable.Range(0,3)
                                          .Select(idx=> balanceBots.GetChipFromOutputBin(idx))
                                          .Aggregate(1, (acc, val) => acc * val);
        Console.WriteLine("What do you get if you multiply together the values of one chip in each of outputs 0, 1, and 2?" +
                          $"\n\t{productOutputBins}.");
    }
}
