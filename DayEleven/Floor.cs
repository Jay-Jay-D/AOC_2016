namespace DayEleven;

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