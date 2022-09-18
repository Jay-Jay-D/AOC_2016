namespace DayEleven;

public class Facility
{
    public Facility()
    {
        Floors = new List<object>(4);
        for (int i = 1; i < 5; i++)
        {
            Floors.Add(new { });
        }
        Elevator = new Elevator(1);
    }

    public List<object> Floors { get; private set; }

    public Elevator Elevator { get; set; }
}
