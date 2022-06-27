using DayOne;
using System.Linq;
namespace DayTwo;

public class BathroomSecurity
{
    public Point CurrentPoint { get; }
    private readonly bool _extendedKeyboard = false;

    public BathroomSecurity()
    {
        CurrentPoint = new Point(1, 1);
    }

    public BathroomSecurity(bool extendedKeyboard)
    {
        _extendedKeyboard = extendedKeyboard;
        var xInit = extendedKeyboard ? -1 : 1;
        CurrentPoint = new Point(xInit, 1);
    }

    public BathroomSecurity(Point startingPoint)
    {
        CurrentPoint = startingPoint;
    }

    public BathroomSecurity(Point startingPoint, bool extendedKeyboard)
    {
        _extendedKeyboard = extendedKeyboard;
        CurrentPoint = startingPoint;
    }

    public string DecodeLine(string instruction)
    {
        int x, y = 0;
        foreach (var direction in instruction)
        {
            x = this.CurrentPoint.X;
            y = this.CurrentPoint.Y;
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
            this.CurrentPoint.X = x;
            this.CurrentPoint.Y = y;
        }
        var intCode = ((CurrentPoint.X + CurrentPoint.Y - 1) + (2 - CurrentPoint.Y) * 4).ToString();
        return intCode;
    }



    public string GetCode(IEnumerable<string> instructions)
    {
        var code = instructions.Select(i => this.DecodeLine(i)).ToArray();
        return string.Join("", code);
    }
}