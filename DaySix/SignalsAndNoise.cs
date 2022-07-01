namespace DaySix;

public class SignalsAndNoise
{
    public static string GetSignal(IEnumerable<string> signalAndNoise, int lineLength = 8, bool isModifiedRepetitionCode = false)
    {
        var counter = GetCharacterOccurences(signalAndNoise, lineLength);
        var signal = GetSignal(counter, lineLength, isModifiedRepetitionCode);
        return signal;
    }

    private static Dictionary<char, Dictionary<int, int>> GetCharacterOccurences(IEnumerable<string> signalAndNoise, int lineLength)
    {
        var counter = new Dictionary<char, Dictionary<int, int>>();
        foreach (var line in signalAndNoise)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (!counter.ContainsKey(line[i]))
                {
                    counter[line[i]] = Enumerable.Range(0, lineLength)
                                                 .Select(idx => new KeyValuePair<int, int>(idx, 0))
                                                 .ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
                }
                counter[line[i]][i] += 1;
            }
        }
        return counter;
    }

    private static string GetSignal(Dictionary<char, Dictionary<int, int>> counter, int lineLength, bool isModifiedRepetitionCode)
    {
        var positionSum = Enumerable.Repeat<int>(isModifiedRepetitionCode ? int.MaxValue : int.MinValue, lineLength).ToArray();
        var signal = new char[lineLength];
        foreach (var charPositionCount in counter)
        {
            var character = charPositionCount.Key;
            foreach (var positionCount in charPositionCount.Value)
            {
                if ((isModifiedRepetitionCode && positionCount.Value < positionSum[positionCount.Key]) ||
                    (!isModifiedRepetitionCode && positionCount.Value > positionSum[positionCount.Key]))
                {
                    positionSum[positionCount.Key] = positionCount.Value;
                    signal[positionCount.Key] = character;
                }
            }
        }

        return new string(signal);
    }


}
