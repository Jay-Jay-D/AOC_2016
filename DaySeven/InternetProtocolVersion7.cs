using System.Text.RegularExpressions;

namespace DaySeven
{
    public class InternetProtocolVersion7
    {
        public static bool TlsSupport(string ip)
        {
            var regex = new Regex(@"\[.+?\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var insiders = regex.Matches(ip).Select(m => m.Value);
            var outsiders = regex.Split(ip);
            return outsiders.Any(o => HasAbba(o)) && insiders.All(o => !HasAbba(o));
        }

        private static bool HasAbba(string characterChain)
        {
            for (var i = 0; i < characterChain.Length - 3; i++)
            {
                if (characterChain[i] == characterChain[i + 1]) continue;
                if (characterChain[i] == characterChain[i + 3] && characterChain[i + 1] == characterChain[i + 2])
                {
                    return true;
                }
            }
            return false;
        }
    }
}