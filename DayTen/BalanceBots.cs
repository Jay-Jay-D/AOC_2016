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
            var parts = botInstruction.Split(" ");
            var botNumber = int.Parse(parts[1]);
            var lowTo = parts[5] == "bot" ? int.Parse(parts[6]) : int.MinValue;
            var highTo = parts[10] == "bot" ? int.Parse(parts[11]) : int.MinValue;
            _bots.Add(new Bot(botNumber, lowTo, highTo));
        }
    }


    public int FindBotCompared(int v1, int v2)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bot> GetBots()
    {
        return _bots;
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