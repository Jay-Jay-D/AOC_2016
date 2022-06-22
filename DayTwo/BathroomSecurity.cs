using DayOne;
namespace DayTwo;


public class BathroomSecurity
{
    public BathroomSecurity()
    {
        StartingPoint = new Point(1, 1);
    }

    public BathroomSecurity(Point startingPoint)
    {
        StartingPoint = startingPoint;
    }

    public int DecodeLine(string instruction)
    {
        int x, y = 0;
        foreach (var direction in instruction)
        {
            x = this.StartingPoint.X;
            y = this.StartingPoint.Y;
            switch (direction)
            {
                case 'U':
                    y = Math.Min(2, y + 1);
                    break;
                case 'D':
                    y = Math.Max(0, y - 1);
                    break;
                case 'R':
                    x = Math.Min(2, x + 1);
                    break;
                case 'L':
                    x = Math.Max(0, x - 1);
                    break;
                default:
                    break;
            }
            this.StartingPoint.X = x;
            this.StartingPoint.Y = y;
        }
        return (StartingPoint.X + StartingPoint.Y - 1) + (2 - StartingPoint.Y) * 4;
    }

    public Point StartingPoint { get; }

    public int GetCode(IEnumerable<string> instructions)
    {
        var code = 0;
        foreach (var instruction in instructions)
        {
            code = (code * 10) + this.DecodeLine(instruction);
        }
        return code;
    }
}