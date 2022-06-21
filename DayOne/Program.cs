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
        throw new NotImplementedException();
    }
}

public class NoTimeForATaxicab
{
    List<Point> VisitedLocations = new List<Point> { new Point(0, 0) };
    public Point? EasterBunnyHQ { get; set; } = null;

    public int FollowInstructions(string instructions)
    {
        var directionSum = 0;
        var direction = 0;
        var x = 0;
        var y = 0;
        foreach (string instruction in instructions.Split(", "))
        {
            var turn = instruction[0];
            var steps = int.Parse(instruction[1..]);
            if (turn == 'R')
            {
                directionSum++;
            }
            else
            {
                directionSum--;
            }
            direction = directionSum % 4;

            var step = 0;
            while (step < steps)
            {
                switch (direction)
                {
                    case 0:
                        y++;
                        break;
                    case 1:
                    case -3:
                        x++;
                        break;
                    case 2:
                    case -2:
                        y--;
                        break;
                    case 3:
                    case -1:
                        x--;
                        break;
                }
                step++;
                var coords = new Point(x, y);
                if (EasterBunnyHQ == null && VisitedLocations.Contains(coords))
                {
                    EasterBunnyHQ = coords;
                }
                VisitedLocations.Add(coords);
            }
        }
        return VisitedLocations.Last().GetDistanceToOrigin();
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var instructions = System.IO.File.ReadAllText("./Input/DayOne.txt").Trim();
        var noTimeForATaxicab = new NoTimeForATaxicab();
        var distance = noTimeForATaxicab.FollowInstructions(instructions);
        Console.WriteLine($"Part 1: distance is {distance}");
        var locationVisitedTwice = noTimeForATaxicab.EasterBunnyHQ.GetDistanceToOrigin();
        Console.WriteLine($"Part 2: distance to the first location you visit twice is {locationVisitedTwice}");
    }
}

