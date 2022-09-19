using System;
namespace DayEleven;

public struct RtgComponent
{
    public string Type;
    public string Material;

    public RtgComponent(string type, string material)
    {
        Type = type;
        Material = material;
    }
}

public struct PossibleAction
{
    public RtgComponent[] Load;
    public int FloorMove;

    public PossibleAction(RtgComponent[] load, int floorMove)
    {
        Load = load;
        FloorMove = floorMove;
    }
}

public enum MoveElevator: int
{
    Up=1,
    Down=-1
}
