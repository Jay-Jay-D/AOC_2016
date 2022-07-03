namespace DaySeven;

public class Program
{
    public static void Main(string[] args)
    {
        var ipList = File.ReadAllLines(@"Input/DaySeven.txt");
        var supportTlsCount = ipList.Count(ip => InternetProtocolVersion7.SupportsTls(ip));
        Console.WriteLine($"Part 1: {supportTlsCount} IP supports TLS.");
        var supportSslCount = ipList.Count(ip => InternetProtocolVersion7.SupportsSsl(ip));
        Console.WriteLine($"Part 2: {supportSslCount} IP supports Ssl.");
    }
}