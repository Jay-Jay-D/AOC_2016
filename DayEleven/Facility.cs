namespace DayEleven;

public class Facility
{
    public Facility() : this(Enumerable.Range(1, 4).ToDictionary(k => k, v => new Floor()))
    { }

    public Facility(Dictionary<int, Floor> floors)
    {
        Floors = floors;
        Elevator = new Elevator(1);
    }

    public Dictionary<int, Floor> Floors { get; private set; }

    public Elevator Elevator { get; set; }

    public Floor this[int floor] => Floors[floor];

    public bool LoadElevator(RtgComponent component)
    {
        return LoadElevator(new[] { component });
    }

    public bool LoadElevator(RtgComponent[] components)
    {
        var floor = Elevator.CurrentFloor;
        var componentsInFloor = components.All(c => Floors[floor].Payload.Contains(c));
        if (!componentsInFloor) return false;


        throw new NotImplementedException();
    }
}

public class Floor
{
    List<RtgComponent> _floorLoad;
    public int Count => _floorLoad.Count;

    public Floor()
    {
        _floorLoad = new List<RtgComponent>();
    }

    public Floor(IEnumerable<RtgComponent> RTGMs)
    {
        _floorLoad = RTGMs.ToList();
    }

    public bool IsEmpty => !_floorLoad.Any();

    public List<RtgComponent> Payload => _floorLoad;
}