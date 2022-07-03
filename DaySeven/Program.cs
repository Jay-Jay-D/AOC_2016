namespace DaySeven;

public class Program
{
    public static void Main(string[] args)
    {
        var ipList = File.ReadAllLines(@"Input/DaySeven.txt");
        var supportTlsCount = ipList.Count(ip => InternetProtocolVersion7.TlsSupport(ip));
        Console.WriteLine($"Part 1: {supportTlsCount} IP supports TLS.");
    }
}