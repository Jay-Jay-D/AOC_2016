namespace DayEleven;

public class Elevator
{
    public int CurrentFloor { get; private set; }
    public int MaxFloor { get; private set; }

    public bool IsEmpty
    {
        get
        {
            return !Payload.Any();
        }
    }
    public List<RtgComponent> Payload { get; private set; } = new List<RtgComponent>(2);

    public Elevator(int currentFloor, int maxFloor)
    {
        CurrentFloor = currentFloor;
        MaxFloor = maxFloor;
    }

    public Elevator(int currentFloor) : this(currentFloor, 4)
    { }

    public int Move(int floors)
    {
        if (!IsEmpty)
        {
            CurrentFloor += floors;
        }
        return CurrentFloor;
    }

    public bool Load(RtgComponent load)
    {
        var loaded = !Payload.Any();
        if (!loaded && Payload.Count() == 1)
        {
            var combinedLoad = new[] { load, Payload.First() };
            loaded = Facility.IsSafe(combinedLoad);
        }
        if (loaded)
        {
            Payload.Add(load);
        }
        return loaded;
    }

    public void Unload(RtgComponent load)
    {
        Payload.Remove(load);
    }

    public bool Load(RtgComponent[] load)
    {
        if (load.Length + Payload.Count > 2)
        {
            return false;
        }
        if (Facility.IsSafe(load))
        {
            Payload.AddRange(load);
        }
        return !IsEmpty;
    }

    public void Unload(RtgComponent[] load)
    {
        foreach (var l in load)
        {
            Unload(l);
        }
    }
}