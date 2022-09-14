namespace DayTen;

public class BalanceBots
{
    IEnumerable<string> _instructions;
    Dictionary<int, Bot> _bots = new Dictionary<int, Bot>();
    public Bot this[int botNumber] => _bots[botNumber];
    List<Tuple<int, int, int>> _book = new List<Tuple<int, int, int>>();

    public BalanceBots(IEnumerable<string> instructions)
    {
        _instructions = instructions;
    }

    public void InitializeBots()
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
        var lowTo = parts[5] == "bot" ? int.Parse(parts[6]) : int.MinValue;
        var highTo = parts[10] == "bot" ? int.Parse(parts[11]) : int.MinValue;
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
        return _bots.Values;
    }

    public void Activate()
    {
        var botsReady = _bots.Select(kvp => kvp.Value)
                             .Where(b => b.IsReady)
                             .ToList();
        foreach (var bot in botsReady)
        {
            // if bots are ready LowChip and HighChip porpeties are not null.
            if (bot.LowTo != int.MinValue)
            {
                _bots[bot.LowTo].ReceiveChip(bot.LowChip.Value);
            }
            if (bot.LowTo != int.MinValue)
            {
                _bots[bot.HighTo].ReceiveChip(bot.HighChip.Value);
            }
            _book.Add(new Tuple<int, int, int>(bot.BotNumber, bot.LowChip.Value, bot.HighChip.Value));
            bot.Reset();
        }
    }

    public int WhichBotCompared(int chip1, int chip2)
    {
        var lowChip = Math.Min(chip1, chip2);
        var highChip = Math.Max(chip1, chip2);
        return _book.Single(b=> b.Item2==lowChip && b.Item3==highChip).Item1;
    }
}
