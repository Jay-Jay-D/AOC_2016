namespace DayEleven;

public class Facility
{
    public Facility()
    {
        Floors = new Dictionary<int, Floor>(4);
        for (int i = 1; i < 5; i++)
        {
            Floors[i]=new Floor();
        }
        Elevator = new Elevator(1);
    }

    public Dictionary<int, Floor> Floors { get; private set; }

    public Elevator Elevator { get; set; }

    public Floor this[int floor]=> Floors[floor];
}

public class Floor
{
    public int Count = 0;
}