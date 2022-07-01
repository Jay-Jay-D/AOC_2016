namespace DaySix;

public class Program
{
    public static void Main(string[] args)
    {
        var signalAndNoise = File.ReadAllLines(@"Input/DaySix.txt");
        var signal = SignalsAndNoise.GetSignal(signalAndNoise);
        Console.WriteLine($"Part 1: The signal is {signal}.");
        signal = SignalsAndNoise.GetSignal(signalAndNoise, isModifiedRepetitionCode: true);
        Console.WriteLine($"Part 2: The signal is {signal}.");
    }
}