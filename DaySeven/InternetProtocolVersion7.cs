using System.Text.RegularExpressions;

namespace DaySeven
{
    public class InternetProtocolVersion7
    {
        public static bool DoesIpSuppotTls(string ip)
        {
            var regex = new Regex(@"\[.+?\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var insideBracktes = regex.Matches(ip);
            var outside = regex.Split(ip);


            return false;
        }
    }
}