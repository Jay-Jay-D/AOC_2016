namespace DayFour;

public class Program
{
    public static void Main(string[] args)
    {
        var encryptedNames = File.ReadAllLines(@"Input/DayFour.txt");
        var sectorIdSum = encryptedNames.Sum(en => SecurityThroughObscurity.CheckRoom(en));
        Console.WriteLine($"Part 1: Sum of the sector IDs of the real rooms is {sectorIdSum}");
        foreach (var encryptedName in encryptedNames)
        {
            var sectorId = SecurityThroughObscurity.CheckRoom(encryptedName);
            if (sectorId == 0) continue;
            var realName = SecurityThroughObscurity.DecryptRoomName(encryptedName);
            if (realName.Contains("northpole")) Console.WriteLine($"Part 2: Room {sectorId} - {realName}");
        }
    }
}