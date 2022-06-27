using DayOne;
using System.Linq;
namespace DayTwo;

public class BathroomSecurity
{
    public Point CurrentPoint { get; }
    private readonly bool _extendedKeyboard = false;

    private readonly Dictionary<Point, string> _extendedKeyboardLayout = new Dictionary<Point, string>{
        {new Point(1,3),"1"},
        {new Point(0,2),"2"},
        {new Point(1,2),"3"},
        {new Point(2,2),"4"},
        {new Point(-1,1),"5"},
        {new Point(0,1),"6"},
        {new Point(1,1),"7"},
        {new Point(2,1),"8"},
        {new Point(3,1),"9"},
        {new Point(0,0),"A"},
        {new Point(1,0),"B"},
        {new Point(2,0),"C"},
        {new Point(1,-1),"D"},
    };


    private readonly Dictionary<Point, string> _basicKeyboardLayout = new Dictionary<Point, string>
    {
        {new Point(0,2),"1"},
        {new Point(1,2),"2"},
        {new Point(2,2),"3"},
        {new Point(0,1),"4"},
        {new Point(1,1),"5"},
        {new Point(2,1),"6"},
        {new Point(0,0),"7"},
        {new Point(1,0),"8"},
        {new Point(2,0),"9"},
    };

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
        var x = 0;
        var y = 0;
        var minX = 0;
        var minY = 0;
        var maxX = 2;
        var maxY = 2;
        var poitToCode = _extendedKeyboard ? _extendedKeyboardLayout : _basicKeyboardLayout;
        foreach (var direction in instruction)
        {
            x = CurrentPoint.X;
            y = CurrentPoint.Y;
            maxY = _extendedKeyboard && x == 1 ? 3 : 2;
            maxX = _extendedKeyboard && y == 1 ? 3 : 2;
            minY = _extendedKeyboard && x == 1 ? -1 : 0;
            minX = _extendedKeyboard && y == 1 ? -1 : 0;
            switch (direction)
            {
                case 'U':
                    y = Math.Min(maxY, y + 1);
                    break;
                case 'D':
                    y = Math.Max(minY, y - 1);
                    break;
                case 'R':
                    x = Math.Min(maxX, x + 1);
                    break;
                case 'L':
                    x = Math.Max(minX, x - 1);
                    break;
                default:
                    break;
            }
            CurrentPoint.X = x;
            CurrentPoint.Y = y;
        }
        var intCode = poitToCode[CurrentPoint];
        return intCode;
    }



    public string GetCode(IEnumerable<string> instructions)
    {
        var code = instructions.Select(i => this.DecodeLine(i)).ToArray();
        return string.Join("", code);
    }
}