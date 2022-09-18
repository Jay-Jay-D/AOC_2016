using System.Linq;
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
    public List<(string Type, string Material)> Payload { get; private set; } = new List<(string Type, string Material)>(2);

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

    public bool Load((string Type, string Material) load)
    {
        var loaded = !Payload.Any();
        if (!loaded && Payload.Count() == 1)
        {
            var combinedLoad = new[] { load, Payload.First() };
            loaded = IsLoadCompatible(combinedLoad);
        }
        if (loaded)
        {
            Payload.Add(load);
        }
        return loaded;
    }

    public void Unload((string Type, string Material) load)
    {
        Payload.Remove(load);
    }

    public bool Load((string Type, string Material)[] load)
    {
        if (load.Length > 2)
        {
            return false;
        }
        if (IsLoadCompatible(load))
        {
            Payload.AddRange(load);
        }
        return !IsEmpty;
    }

    public void Unload((string Type, string Material)[] load)
    {
        foreach (var l in load)
        {
            Unload(l);
        }
    }

    bool IsLoadCompatible((string Type, string Material)[] load)
    {
        if (load.Length == 1) return true;
        if (load[0].Material == load[1].Material) return true;
        if (load[0].Type == load[1].Type) return true;
        return false;
    }
}