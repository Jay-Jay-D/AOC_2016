namespace DayTen;

public class Bot
{
    public int BotNumber { get; private set; }
    public Tuple<string, int> LowTo { get; private set; }
    public Tuple<string, int> HighTo { get; private set; }
    public bool IsReady { get => LowChip.HasValue && HighChip.HasValue; }
    public int? LowChip { get; private set; }
    public int? HighChip { get; private set; }


    public Bot(int botNumber, Tuple<string, int> lowTo, Tuple<string, int> highTo)
    {
        BotNumber = botNumber;
        LowTo = lowTo;
        HighTo = highTo;
    }

    public Bot(int botNumber)
    {
        BotNumber = botNumber;
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

    internal void Reset()
    {
        LowChip = null;
        HighChip = null;
    }
}