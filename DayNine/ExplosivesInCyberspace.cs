using System.Text;

namespace DayNine;

public class ExplosivesInCyberspace
{
    public static object Decompress(string compressed)
    {
        var idx = 0;
        var charsCountToRepeat = 0;
        var repetitions = 0;
        var decompressed = new StringBuilder();
        while (idx < compressed.Length)
        {
            if (compressed[idx] == ' ')
            {
                idx++;
                continue;
            }
            if (compressed[idx] == '(')
            {
                idx++; // the '(' character
                var marker = new string(compressed.Skip(idx).TakeWhile(c => c != ')').ToArray());
                idx += marker.Length + 1; // the ')' character
                var markerParts = marker.Split("x");
                charsCountToRepeat = int.Parse(markerParts[0]);
                repetitions = int.Parse(markerParts[1]);
                var charsToRepeat = new string(compressed.Skip(idx).Take(charsCountToRepeat).ToArray());
                for (var r = 0; r < repetitions; r++)
                {
                    decompressed.Append(charsToRepeat);
                }
                idx += charsCountToRepeat;
                continue;
            }
            decompressed.Append(compressed[idx++]);
        }
        return decompressed.ToString();
    }
}
