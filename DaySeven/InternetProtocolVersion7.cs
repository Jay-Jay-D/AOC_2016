using System.Text.RegularExpressions;

namespace DaySeven
{
    public class InternetProtocolVersion7
    {
        public static bool SupportsTls(string ip)
        {
            var regex = new Regex(@"\[.+?\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var hypernet = regex.Matches(ip).Select(m => m.Value);
            var supernet = regex.Split(ip);
            return supernet.Any(o => HasAbba(o)) && hypernet.All(o => !HasAbba(o));
        }

        public static bool SupportsSsl(string ip)
        {
            var regex = new Regex(@"\[.+?\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var hypernets = regex.Matches(ip).Select(m => m.Value);
            var supernets = regex.Split(ip);
            var babs = supernets.Select(cs => GetAba(cs))
                                .SelectMany(l => l)
                                .Select(aba => new string(new[]
                                            {
                                                aba[1],
                                                aba[0],
                                                aba[1]
                                            }))
                                .ToList();
            foreach (var hypernet in hypernets)
            {
                foreach (var bab in babs)
                {
                    if (hypernet.Contains(bab)) return true;
                }
            }
            return false;
        }

        private static bool HasAbba(string charSequence)
        {
            for (var i = 0; i < charSequence.Length - 3; i++)
            {
                if (charSequence[i] != charSequence[i + 1]
                    && charSequence[i] == charSequence[i + 3]
                    && charSequence[i + 1] == charSequence[i + 2])
                {
                    return true;
                }
            }
            return false;
        }

        private static IEnumerable<string> GetAba(string charSequence)
        {
            for (var i = 0; i < charSequence.Length - 2; i++)
            {
                if (charSequence[i] != charSequence[i + 1]
                    && charSequence[i] == charSequence[i + 2])
                {
                    yield return charSequence.Substring(i, 3);
                }
            }
        }
    }
}