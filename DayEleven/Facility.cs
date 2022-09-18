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
}

public class Floor
{
    List<RTGM> _floorLoad;
    public int Count => _floorLoad.Count;

    public Floor()
    {
        _floorLoad = new List<RTGM>();
    }

    public Floor(IEnumerable<RTGM> RTGMs)
    {
        _floorLoad = RTGMs.ToList();
    }

    public bool IsEmpty => !_floorLoad.Any();

    public List<RTGM> Load => _floorLoad;
}