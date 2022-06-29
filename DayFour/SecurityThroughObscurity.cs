namespace DayFour;

public class SecurityThroughObscurity
{
    public static string DecryptRoomName(string encryptedName)
    {
        var sectorId = CheckRoom(encryptedName);
        if (sectorId == 0) { return string.Empty; }
        var decriptedName = new List<char>();
        var encryptedChars = encryptedName.TakeWhile(c => !char.IsDigit(c))
                                          .ToArray();
        foreach (var c in encryptedChars)
        {
            var newChar = c == '-' ? ' ' : ((c - 97 + sectorId) % 26) + 97;
            decriptedName.Add((char)newChar);
        }

        return new string(decriptedName.ToArray()).Trim();
    }

    public static int CheckRoom(string encryptedName)
    {
        var countDictionary = new Dictionary<char, int>();
        double sectorId = 0;
        var checkSum = encryptedName.SkipWhile(c => c != '[')
                                    .Skip(1)
                                    .TakeWhile(c => c != ']')
                                    .ToArray();

        encryptedName = encryptedName.Split('[')[0].Replace("-", string.Empty);

        foreach (var c in encryptedName)
        {
            if (char.IsDigit(c))
            {
                sectorId = sectorId * 10 + char.GetNumericValue(c);
            }
            if (char.IsLetter(c))
            {
                if (!countDictionary.ContainsKey(c))
                {
                    countDictionary.Add(c, 0);
                }
                countDictionary[c]++;
            }
        }
        var encryptedNameChecksum = countDictionary.OrderByDescending(kvp => kvp.Value)
                                                   .ThenBy(kvp => kvp.Key)
                                                   .Select(kvp => kvp.Key)
                                                   .Take(checkSum.Length)
                                                   .ToArray();

        return encryptedNameChecksum.SequenceEqual(checkSum) ? (int)sectorId : 0;
    }
}