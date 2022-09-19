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
        var canLoad = Elevator.Load(components);
        if (!canLoad) return false;
        foreach (var component in components)
        {
            Floors[floor].Payload.Remove(component);
        }
        return true;
    }

    public int MoveElevator(int floorShift)
    {
        if (IsSafeToMoveElevator(floorShift))
        {
            return Elevator.Move(floorShift);
        }
        return Elevator.CurrentFloor;
    }

    private bool IsSafeToMoveElevator(int floorShift)
    {
        /*
         * Critical assumption: there are only two component for each material, 
         * a single generator and a single chip. 
         */
        var targetFloor = Elevator.CurrentFloor + floorShift;
        var securedMaterial = Floors[targetFloor].Payload.Concat(Elevator.Payload)
            .Select(c => c)
            .GroupBy(c => c.Material)
            .Where(gb => gb.Count() == 2)
            .Select(gb => gb.Key)
            .ToArray();

        var unsafeElevatorComponents = Elevator.Payload
            .Where(c => !Array.Exists(securedMaterial, scm => scm == c.Material));
        var unsafeFloorComponents = Floors[targetFloor].Payload
            .Where(c => !Array.Exists(securedMaterial, scm => scm == c.Material));

        foreach (var elevatorComponent in unsafeElevatorComponents)
        {
            foreach (var floorComponent in unsafeFloorComponents)
            {
                if (!Facility.IsSafe(new[] { elevatorComponent, floorComponent })) return false;
            }

        }
        return true;
    }

    public static bool IsSafe(RtgComponent[] load)
    {
        if (load.Length == 1) return true;
        if (load[0].Material == load[1].Material) return true;
        if (load[0].Type == load[1].Type) return true;
        return false;
    }
}
