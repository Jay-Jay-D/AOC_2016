namespace DayOne;

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

