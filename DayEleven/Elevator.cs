namespace DayEleven;

public class Elevator
{
    public int Floor { get; private set; }
    public bool IsEmpty { get; set; } = true;

    public Elevator(int floor)
    {
        Floor = floor;
    }

    public bool Move(int v)
    {
        return !IsEmpty;
    }

    public bool Load((string Type, string Material) load)
    {
        IsEmpty = false;
        return false;
    }

    public void Unload((string Type, string Material) load)
    {
        IsEmpty = true;
    }

    public bool Load((string Type, string Material)[] load)
    {
        if (load.Length > 2)
        {
            return false;
        }
        IsEmpty = !IsLoadCompatible(load);
        return !IsEmpty;
    }

    public void Unload((string Type, string Material)[] load)
    {
        IsEmpty = true;
    }

    bool IsLoadCompatible((string Type, string Material)[] load)
    {
        if (load.Length==1) return true;
        if (load[0].Material == load[1].Material) return true;
        if (load[0].Type == load[1].Type) return true;
        return false;
    }
}