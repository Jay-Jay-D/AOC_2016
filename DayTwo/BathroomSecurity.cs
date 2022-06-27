using DayOne;
using System.Linq;
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

    public string DecodeLine(string instruction)
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
        int intCode = (StartingPoint.X + StartingPoint.Y - 1) + (2 - StartingPoint.Y) * 4;
        return intCode.ToString();
    }

    public Point StartingPoint { get; }

    public string GetCode(IEnumerable<string> instructions)
    {
        var code = instructions.Select(i => this.DecodeLine(i)).ToArray();
        return string.Join("", code);
    }
}