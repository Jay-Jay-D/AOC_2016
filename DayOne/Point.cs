namespace DayOne;

public class Point
{
    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public int X { get; set; } = 0;

    public int Y { get; set; } = 0;

    public override bool Equals(object obj)
    {
        return obj is Point point &&
               X == point.X &&
               Y == point.Y;
    }

    public int GetDistanceToOrigin()
    {
        return Math.Abs(this.X) + Math.Abs(this.Y);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

