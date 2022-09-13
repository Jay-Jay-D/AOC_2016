using System.Text.RegularExpressions;
using System;

namespace DayTen;

public class BalanceBots
{
    IEnumerable<string> _instructions;
    List<Bot> _bots = new List<Bot>();

    public BalanceBots(IEnumerable<string> instructions)
    {
        _instructions = instructions;
    }

    public void InitializeBots()
    {
        var botsInstructions = _instructions.Where(i => i.StartsWith("bot"));
        foreach (var botInstruction in botsInstructions)
        {
            _bots.Add(ParseBotFromInstructions(botInstruction));
        }

        var valueInstructions = _instructions.Where(i => i.StartsWith("value"));
        foreach (var valueInstruction in valueInstructions)
        {
            ParseValueFromInstructions(valueInstruction);
        }
    }

    private void ParseValueFromInstructions(string valueInstruction)
    {
        var parts = valueInstruction.Split(" ");
        var chip = int.Parse(parts[1]);
        var botNumber = int.Parse(parts[5]);
        var bot = _bots.Single(b => b.BotNumber == botNumber);
        bot.ReceiveChip(chip);
    }

    private static Bot ParseBotFromInstructions(string botInstruction)
    {
        var parts = botInstruction.Split(" ");
        var botNumber = int.Parse(parts[1]);
        var lowTo = parts[5] == "bot" ? int.Parse(parts[6]) : int.MinValue;
        var highTo = parts[10] == "bot" ? int.Parse(parts[11]) : int.MinValue;
        var bot = new Bot(botNumber, lowTo, highTo);
        return bot;
    }

    public int FindBotCompared(int v1, int v2)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bot> GetBots()
    {
        return _bots;
    }

    public void Activate()
    {
        foreach (var bot in _bots.Where(b=>b.IsReady))
        {
            _bots.Single(b=>b.BotNumber==bot.LowTo ).ReceiveChip(bot.LowChip.Value);
            _bots.Single(b=>b.BotNumber==bot.HighTo ).ReceiveChip(bot.HighChip.Value);
        }
    }
}

public class Bot
{
    public int BotNumber { get; private set; }
    public int LowTo { get; set; }
    public int HighTo { get; set; }
    public bool IsReady { get => LowChip.HasValue && HighChip.HasValue; }
    public Nullable<int> LowChip { get; private set; }
    public Nullable<int> HighChip { get; private set; }


    public Bot(int botNumber, int lowTo, int highTo)
    {
        BotNumber = botNumber;
        LowTo = lowTo;
        HighTo = highTo;
    }

    public Bot(int botNumber)
    {
        BotNumber = botNumber;
        LowTo = int.MinValue;
        HighTo = int.MinValue;
    }

    public void ReceiveChip(int v)
    {
        if (!LowChip.HasValue)
        {
            LowChip = v;
        }
        else if (LowChip > v)
        {
            HighChip = LowChip;
            LowChip = v;
        }
        else if (!HighChip.HasValue)
        {
            HighChip = v;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Bot bot &&
               BotNumber == bot.BotNumber &&
               LowTo == bot.LowTo &&
               HighTo == bot.HighTo &&
               IsReady == bot.IsReady;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BotNumber, LowTo, HighTo, IsReady);
    }
}