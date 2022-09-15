namespace DayTen;

public class BalanceBots
{
    IEnumerable<string> _instructions;
    Dictionary<int, Bot> _bots = new Dictionary<int, Bot>();
    public Bot this[int botNumber] => _bots[botNumber];
    List<Tuple<int, int, int>> _book = new List<Tuple<int, int, int>>();
    Dictionary<int, int> _outputBins = new Dictionary<int, int>();

    public BalanceBots(IEnumerable<string> instructions)
    {
        _instructions = instructions;
        InitializeBots();
    }

    private void InitializeBots()
    {
        var botsInstructions = _instructions.Where(i => i.StartsWith("bot"));
        foreach (var botInstruction in botsInstructions)
        {
            ParseBotFromInstructions(botInstruction);
        }

        var valueInstructions = _instructions.Where(i => i.StartsWith("value"));
        foreach (var valueInstruction in valueInstructions)
        {
            ParseValueFromInstructions(valueInstruction);
        }
    }

    private void ParseBotFromInstructions(string botInstruction)
    {
        var parts = botInstruction.Split(" ");
        var botNumber = int.Parse(parts[1]);
        var lowTo = new Tuple<string, int>(parts[5], int.Parse(parts[6]));
        var highTo = new Tuple<string, int>(parts[10], int.Parse(parts[11]));
        _bots[botNumber] = new Bot(botNumber, lowTo, highTo);
    }


    private void ParseValueFromInstructions(string valueInstruction)
    {
        var parts = valueInstruction.Split(" ");
        var chip = int.Parse(parts[1]);
        var botNumber = int.Parse(parts[5]);
        _bots[botNumber].ReceiveChip(chip);
    }

    public IEnumerable<Bot> GetBots()
    {
        return _bots.Values.OrderBy(b => b.BotNumber);
    }

    public bool Activate()
    {
        var botsReady = _bots.Select(kvp => kvp.Value)
                             .Where(b => b.IsReady)
                             .ToList();
        foreach (var bot in botsReady)
        {
            // if bots are ready LowChip and HighChip propeties are not null.
#pragma warning disable 8629
            if (bot.LowTo.Item1 == "bot")
            {
                _bots[bot.LowTo.Item2].ReceiveChip(bot.LowChip.Value);
            }
            else
            {
                _outputBins[bot.LowTo.Item2] = bot.LowChip.Value;
            }

            if (bot.HighTo.Item1 == "bot")
            {
                _bots[bot.HighTo.Item2].ReceiveChip(bot.HighChip.Value);
            }
            else
            {
                _outputBins[bot.HighTo.Item2] = bot.HighChip.Value;
            }
#pragma warning restore 8629
            _book.Add(new Tuple<int, int, int>(bot.BotNumber, bot.LowChip.Value, bot.HighChip.Value));
            bot.Reset();
        }
        return botsReady.Any();
    }

    public int WhichBotCompared(int chip1, int chip2)
    {
        var lowChip = Math.Min(chip1, chip2);
        var highChip = Math.Max(chip1, chip2);
        return _book.Single(b => b.Item2 == lowChip && b.Item3 == highChip).Item1;
    }

    public void Run()
    {
        while (Activate()) { }
    }

    public int GetChipFromOutputBin(int outputBin)
    {
        return _outputBins[outputBin];
    }
}
